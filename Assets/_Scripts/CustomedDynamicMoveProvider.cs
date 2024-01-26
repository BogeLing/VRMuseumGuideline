using Unity.XR.CoreUtils;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
    /// <summary>
    /// A version of action-based continuous movement that automatically controls the frame of reference that
    /// determines the forward direction of movement based on user preference for each hand.
    /// For example, can configure to use head relative movement for the left hand and controller relative movement for the right hand.
    /// </summary>
    public class CustomedDynamicMoveProvider : ActionBasedContinuousMoveProvider
    {
        /// <summary>
        /// Defines which transform the XR Origin's movement direction is relative to.
        /// </summary>
        /// <seealso cref="leftHandMovementDirection"/>
        /// <seealso cref="rightHandMovementDirection"/>
        /// 

        private bool allowXAxisMovement = false;
        private bool circleMovementEnabled = false;
        private float movementRadius;
        public Transform cameraOffsetTransform;

        public enum MovementDirection
        {
            /// <summary>
            /// Use the forward direction of the head (camera) as the forward direction of the XR Origin's movement.
            /// </summary>
            HeadRelative,

            /// <summary>
            /// Use the forward direction of the hand (controller) as the forward direction of the XR Origin's movement.
            /// </summary>
            HandRelative,
        }

        [Space, Header("Movement Direction")]
        [SerializeField]
        [Tooltip("Directs the XR Origin's movement when using the head-relative mode. If not set, will automatically find and use the XR Origin Camera.")]
        Transform m_HeadTransform;

        /// <summary>
        /// Directs the XR Origin's movement when using the head-relative mode. If not set, will automatically find and use the XR Origin Camera.
        /// </summary>
        public Transform headTransform
        {
            get => m_HeadTransform;
            set => m_HeadTransform = value;
        }

        [SerializeField]
        [Tooltip("Directs the XR Origin's movement when using the hand-relative mode with the left hand.")]
        Transform m_LeftControllerTransform;

        /// <summary>
        /// Directs the XR Origin's movement when using the hand-relative mode with the left hand.
        /// </summary>
        public Transform leftControllerTransform
        {
            get => m_LeftControllerTransform;
            set => m_LeftControllerTransform = value;
        }

        [SerializeField]
        [Tooltip("Directs the XR Origin's movement when using the hand-relative mode with the right hand.")]
        Transform m_RightControllerTransform;

        public Transform rightControllerTransform
        {
            get => m_RightControllerTransform;
            set => m_RightControllerTransform = value;
        }

        [SerializeField]
        [Tooltip("Whether to use the specified head transform or left controller transform to direct the XR Origin's movement for the left hand.")]
        MovementDirection m_LeftHandMovementDirection;

        /// <summary>
        /// Whether to use the specified head transform or controller transform to direct the XR Origin's movement for the left hand.
        /// </summary>
        /// <seealso cref="MovementDirection"/>
        public MovementDirection leftHandMovementDirection
        {
            get => m_LeftHandMovementDirection;
            set => m_LeftHandMovementDirection = value;
        }

        [SerializeField]
        [Tooltip("Whether to use the specified head transform or right controller transform to direct the XR Origin's movement for the right hand.")]
        MovementDirection m_RightHandMovementDirection;

        /// <summary>
        /// Whether to use the specified head transform or controller transform to direct the XR Origin's movement for the right hand.
        /// </summary>
        /// <seealso cref="MovementDirection"/>
        public MovementDirection rightHandMovementDirection
        {
            get => m_RightHandMovementDirection;
            set => m_RightHandMovementDirection = value;
        }

        Transform m_CombinedTransform;
        Pose m_LeftMovementPose = Pose.identity;
        Pose m_RightMovementPose = Pose.identity;

        /// <inheritdoc />
        protected override void Awake()
        {
            base.Awake();

            m_CombinedTransform = new GameObject("[Dynamic Move Provider] Combined Forward Source").transform;
            m_CombinedTransform.SetParent(transform, false);
            m_CombinedTransform.localPosition = Vector3.zero;
            m_CombinedTransform.localRotation = Quaternion.identity;

            forwardSource = m_CombinedTransform;
        }

        /// <inheritdoc />
        protected override Vector3 ComputeDesiredMove(Vector2 input)
        {
            // Don't need to do anything if the total input is zero.
            // This is the same check as the base method.
            if (input == Vector2.zero)
                return Vector3.zero;

            // Initialize the Head Transform if necessary, getting the Camera from XR Origin
            if (m_HeadTransform == null)
            {
                var xrOrigin = system.xrOrigin;
                if (xrOrigin != null)
                {
                    var xrCamera = xrOrigin.Camera;
                    if (xrCamera != null)
                        m_HeadTransform = xrCamera.transform;
                }
            }

            // Get the forward source for the left hand input
            switch (m_LeftHandMovementDirection)
            {
                case MovementDirection.HeadRelative:
                    if (m_HeadTransform != null)
                        m_LeftMovementPose = m_HeadTransform.GetWorldPose();

                    break;

                case MovementDirection.HandRelative:
                    if (m_LeftControllerTransform != null)
                        m_LeftMovementPose = m_LeftControllerTransform.GetWorldPose();

                    break;

                default:
                    Assert.IsTrue(false, $"Unhandled {nameof(MovementDirection)}={m_LeftHandMovementDirection}");
                    break;
            }

            // Get the forward source for the right hand input
            switch (m_RightHandMovementDirection)
            {
                case MovementDirection.HeadRelative:
                    if (m_HeadTransform != null)
                        m_RightMovementPose = m_HeadTransform.GetWorldPose();

                    break;

                case MovementDirection.HandRelative:
                    if (m_RightControllerTransform != null)
                        m_RightMovementPose = m_RightControllerTransform.GetWorldPose();

                    break;

                default:
                    Assert.IsTrue(false, $"Unhandled {nameof(MovementDirection)}={m_RightHandMovementDirection}");
                    break;
            }

            // Combine the two poses into the forward source based on the magnitude of input
            var leftHandValue = leftHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
            var rightHandValue = rightHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

            var totalSqrMagnitude = leftHandValue.sqrMagnitude + rightHandValue.sqrMagnitude;
            var leftHandBlend = 0.5f;
            if (totalSqrMagnitude > Mathf.Epsilon)
                leftHandBlend = leftHandValue.sqrMagnitude / totalSqrMagnitude;

            var combinedPosition = Vector3.Lerp(m_RightMovementPose.position, m_LeftMovementPose.position, leftHandBlend);
            var combinedRotation = Quaternion.Slerp(m_RightMovementPose.rotation, m_LeftMovementPose.rotation, leftHandBlend);
            m_CombinedTransform.SetPositionAndRotation(combinedPosition, combinedRotation);

            // �����ƶ���������
            Vector3 move = base.ComputeDesiredMove(input);

            // �����������X���ƶ�����X��������Ϊ0
            if (!allowXAxisMovement)
            {
                move.x = 0;
            }

            // ���Բ���˶�����
            if (circleMovementEnabled)
            {
                Vector3 newPosition = transform.position + move;
                float distance = Vector2.Distance(new Vector2(newPosition.x, newPosition.z), Vector2.zero);

                // �����λ�ó���Բ��·��������λ���Ա�����Բ��·����
                if (Mathf.Abs(distance - movementRadius) > Mathf.Epsilon)
                {
                    Vector2 direction = new Vector2(newPosition.x, newPosition.z).normalized;
                    newPosition.x = direction.x * movementRadius;
                    newPosition.z = direction.y * movementRadius;

                    // ����move�Է�ӳ�µ�λ�ã�ͬʱ����y���ֵ
                    move.x = newPosition.x - transform.position.x;
                    move.z = newPosition.z - transform.position.z;

                    UpdateCameraOffsetRotation(newPosition);
                }

            }

            // �������յ��ƶ�����
            return move;
        }

        private void UpdateCameraOffsetRotation(Vector3 newPosition)
        {
            if (cameraOffsetTransform == null)
            {
                Debug.LogError("Camera Offset Transform is not set.");
                return;
            }

            // ���㵱ǰλ����ԭ��֮�������
            Vector3 toOrigin = new Vector3(0, 0, 0) - newPosition;

            // ��������������Z��ļн�
            float angle = - Vector3.SignedAngle(toOrigin, Vector3.forward, Vector3.up);

            // ���� Camera Offset Transform ����ת
            cameraOffsetTransform.rotation = Quaternion.Euler(0, angle, 0);
        }

        [SerializeField]
        private InputAction enableXAxisMovementAction; // ��������X���ƶ��Ķ���

        [SerializeField]
        private InputAction disableXAxisMovementAction; // ���ڽ���X���ƶ��Ķ���
        private void OnEnable()
        {
            enableXAxisMovementAction.Enable();
            enableXAxisMovementAction.performed += EnableXAxisMovement;
            enableXAxisMovementAction.performed += EnableCircleMovement;

            disableXAxisMovementAction.Enable();
            disableXAxisMovementAction.performed += DisableXAxisMovement;
        }

        private void OnDisable()
        {
            enableXAxisMovementAction.Disable();
            enableXAxisMovementAction.performed -= EnableXAxisMovement;
            enableXAxisMovementAction.performed -= EnableCircleMovement;

            disableXAxisMovementAction.Disable();
            disableXAxisMovementAction.performed -= DisableXAxisMovement;
        }

        private void EnableCircleMovement(InputAction.CallbackContext context)
        {
            if (context.ReadValue<float>() > 0)
            {
                circleMovementEnabled = true;
                // ����Բ���˶�ʱ�����㵱ǰ��ԭ��ľ���
                movementRadius = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), Vector2.zero);
            }
        }

        private void EnableXAxisMovement(InputAction.CallbackContext context)
        {
            if (context.ReadValue<float>() > 0)
            {
                allowXAxisMovement = true; // ����X���ƶ���״̬
            }
        }

        private void DisableXAxisMovement(InputAction.CallbackContext context)
        {
            if (context.ReadValue<float>() > 0)
            {
                allowXAxisMovement = false; // ����X���ƶ���״̬
                circleMovementEnabled = false; // ͬʱ����Բ���˶�
            }
        }

    }
}
