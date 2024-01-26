using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    //public Canvas wristUICanvas;
    public InputAction gripAction; // 新的InputAction变量，用于grip

    private void OnEnable()
    {
        gripAction.Enable(); // 启用grip动作
        gripAction.performed += OnGripPerformed; // 当grip动作执行时调用OnGripPerformed
    }

    private void OnDisable()
    {
        gripAction.Disable(); // 禁用grip动作
        gripAction.performed -= OnGripPerformed; // 取消订阅
    }

    private void OnGripPerformed(InputAction.CallbackContext context)
    {
        // 读取grip的模拟值
        float gripValue = context.ReadValue<float>();
        // 如果grip的模拟值大于0.5，打印到控制台
        if (gripValue > 0.5f)
        {
            Debug.Log(gripValue);
        }
    }
}
