using UnityEngine;
using UnityEngine.InputSystem;

public class WristUI : MonoBehaviour
{
    //public Canvas wristUICanvas;
    public InputAction gripAction; // �µ�InputAction����������grip

    private void OnEnable()
    {
        gripAction.Enable(); // ����grip����
        gripAction.performed += OnGripPerformed; // ��grip����ִ��ʱ����OnGripPerformed
    }

    private void OnDisable()
    {
        gripAction.Disable(); // ����grip����
        gripAction.performed -= OnGripPerformed; // ȡ������
    }

    private void OnGripPerformed(InputAction.CallbackContext context)
    {
        // ��ȡgrip��ģ��ֵ
        float gripValue = context.ReadValue<float>();
        // ���grip��ģ��ֵ����0.5����ӡ������̨
        if (gripValue > 0.5f)
        {
            Debug.Log(gripValue);
        }
    }
}
