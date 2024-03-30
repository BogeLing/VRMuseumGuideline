using UnityEngine;
using UnityEngine.InputSystem;

public class ShowObjectOnInput : MonoBehaviour
{
    public InputAction showAction; // ���ڴ�����ʾ������Input Action
    public GameObject objectToShow; // ��Inspector�����ָ����Ҫ��ʾ������

    private void OnEnable()
    {
        showAction.Enable();
        showAction.performed += ShowObject;
    }

    private void OnDisable()
    {
        showAction.Disable();
        showAction.performed -= ShowObject;
    }

    private void ShowObject(InputAction.CallbackContext context)
    {
        // ����Ƿ���ָ����Ҫ��ʾ������
        if (objectToShow != null)
        {
            // ��ʾָ��������
            objectToShow.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No object assigned to ShowObjectOnInput script on " + gameObject.name);
        }
    }
}