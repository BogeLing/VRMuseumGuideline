using UnityEngine;
using UnityEngine.InputSystem;

public class ResetScale : MonoBehaviour
{
    public InputAction scaleAction; // 用于缩放的输入动作

    private void OnEnable()
    {
        scaleAction.Enable(); // 启用缩放动作
        scaleAction.performed += OnScaleActionPerformed;
    }

    private void OnDisable()
    {
        scaleAction.Disable(); // 禁用缩放动作
        scaleAction.performed -= OnScaleActionPerformed;
    }

    private void OnScaleActionPerformed(InputAction.CallbackContext context)
    {
        // 检查所有子物体是否都处于激活状态
        bool allChildrenActive = true;
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                allChildrenActive = false;
                break;
            }
        }

        // 如果所有子物体都激活，则重置本物体的scale
        if (allChildrenActive)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
