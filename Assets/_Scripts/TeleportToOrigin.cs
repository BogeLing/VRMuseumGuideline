using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportToOrigin : MonoBehaviour
{
    public InputAction teleportAction;
    public Transform cameraOffsetTransform; // 公开的 Camera Offset Transform 变量
    public Transform table; // 公开的 Table Transform 变量

    private Vector3 originalPosition;
    private Vector3 originalTablePosition; // 用于保存 Table 的初始位置

    void Start()
    {
        // 保存初始位置
        originalPosition = transform.position;

        // 检查并保存 Table 的初始位置
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
        // 当动作触发时，将物体传送回原始位置并重置 Camera Offset 的旋转
        if (context.ReadValue<float>() > 0)
        {
            transform.position = originalPosition;

            // 检查是否设置了 Camera Offset Transform
            if (cameraOffsetTransform != null)
            {
                cameraOffsetTransform.rotation = Quaternion.identity; // 将旋转重置为初始状态
            }
            else
            {
                Debug.LogWarning("Camera Offset Transform is not set.");
            }

            // 同时重置 Table 的位置
            if (table != null)
            {
                table.position = originalTablePosition;
            }
        }
    }
}
