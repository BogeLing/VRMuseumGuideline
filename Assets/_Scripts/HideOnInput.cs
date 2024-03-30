using UnityEngine;
using UnityEngine.InputSystem;

public class HideOnInput : MonoBehaviour
{
    public InputAction hideAction1; // 第一个用于隐藏物体的Input Action
    public InputAction hideAction2; // 第二个用于隐藏物体的Input Action

    private void OnEnable()
    {
        // 启用InputAction
        hideAction1.Enable();
        hideAction2.Enable();
        // 为两个InputAction注册相同的事件处理函数
        hideAction1.performed += HideObject;
        hideAction2.performed += HideObject;
    }

    private void OnDisable()
    {
        // 禁用InputAction
        hideAction1.Disable();
        hideAction2.Disable();
        // 注销事件处理函数
        hideAction1.performed -= HideObject;
        hideAction2.performed -= HideObject;
    }

    private void HideObject(InputAction.CallbackContext context)
    {
        // 隐藏物体
        gameObject.SetActive(false);
    }
}