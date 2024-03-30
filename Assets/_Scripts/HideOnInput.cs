using UnityEngine;
using UnityEngine.InputSystem;

public class HideOnInput : MonoBehaviour
{
    public InputAction hideAction1; // ��һ���������������Input Action
    public InputAction hideAction2; // �ڶ����������������Input Action

    private void OnEnable()
    {
        // ����InputAction
        hideAction1.Enable();
        hideAction2.Enable();
        // Ϊ����InputActionע����ͬ���¼�������
        hideAction1.performed += HideObject;
        hideAction2.performed += HideObject;
    }

    private void OnDisable()
    {
        // ����InputAction
        hideAction1.Disable();
        hideAction2.Disable();
        // ע���¼�������
        hideAction1.performed -= HideObject;
        hideAction2.performed -= HideObject;
    }

    private void HideObject(InputAction.CallbackContext context)
    {
        // ��������
        gameObject.SetActive(false);
    }
}