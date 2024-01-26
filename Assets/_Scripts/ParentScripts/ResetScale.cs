using UnityEngine;
using UnityEngine.InputSystem;

public class ResetScale : MonoBehaviour
{
    public InputAction scaleAction; // �������ŵ����붯��

    private void OnEnable()
    {
        scaleAction.Enable(); // �������Ŷ���
        scaleAction.performed += OnScaleActionPerformed;
    }

    private void OnDisable()
    {
        scaleAction.Disable(); // �������Ŷ���
        scaleAction.performed -= OnScaleActionPerformed;
    }

    private void OnScaleActionPerformed(InputAction.CallbackContext context)
    {
        // ��������������Ƿ񶼴��ڼ���״̬
        bool allChildrenActive = true;
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                allChildrenActive = false;
                break;
            }
        }

        // ������������嶼��������ñ������scale
        if (allChildrenActive)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
