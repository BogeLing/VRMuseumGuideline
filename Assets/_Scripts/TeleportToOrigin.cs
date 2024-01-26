using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportToOrigin : MonoBehaviour
{
    public InputAction teleportAction;
    public Transform cameraOffsetTransform; // ������ Camera Offset Transform ����
    public Transform table; // ������ Table Transform ����

    private Vector3 originalPosition;
    private Vector3 originalTablePosition; // ���ڱ��� Table �ĳ�ʼλ��

    void Start()
    {
        // �����ʼλ��
        originalPosition = transform.position;

        // ��鲢���� Table �ĳ�ʼλ��
        if (table != null)
        {
            originalTablePosition = table.position;
        }
        else
        {
            Debug.LogWarning("Table Transform is not set.");
        }
    }

    private void OnEnable()
    {
        teleportAction.Enable();
        teleportAction.performed += HandleTeleport;
    }

    private void OnDisable()
    {
        teleportAction.Disable();
        teleportAction.performed -= HandleTeleport;
    }

    private void HandleTeleport(InputAction.CallbackContext context)
    {
        // ����������ʱ�������崫�ͻ�ԭʼλ�ò����� Camera Offset ����ת
        if (context.ReadValue<float>() > 0)
        {
            transform.position = originalPosition;

            // ����Ƿ������� Camera Offset Transform
            if (cameraOffsetTransform != null)
            {
                cameraOffsetTransform.rotation = Quaternion.identity; // ����ת����Ϊ��ʼ״̬
            }
            else
            {
                Debug.LogWarning("Camera Offset Transform is not set.");
            }

            // ͬʱ���� Table ��λ��
            if (table != null)
            {
                table.position = originalTablePosition;
            }
        }
    }
}
