using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public class ActionUIBinding
    {
        public InputAction action; // ֱ��ʹ��InputAction
        public GameObject uiElement; // ��Ӧ��UIԪ��
    }

    public List<ActionUIBinding> actionBindings; // ������UIԪ�صİ��б�

    private void OnEnable()
    {
        // ע�����ж����Ĵ����¼�
        foreach (var binding in actionBindings)
        {
            binding.action.Enable();
            binding.action.performed += context => OnActionPerformed(binding);
        }
    }

    private void OnDisable()
    {
        // ע�����ж����Ĵ����¼�
        foreach (var binding in actionBindings)
        {
            binding.action.Disable();
            binding.action.performed -= context => OnActionPerformed(binding);
        }
    }

    private void OnActionPerformed(ActionUIBinding binding)
    {
        // ��������UIԪ��
        HideAllUIElements();

        // ��ʾ����������Ӧ��UIԪ��
        binding.uiElement.SetActive(true);
    }

    private void HideAllUIElements()
    {
        // �����б�����ÿ��UIԪ��
        foreach (var binding in actionBindings)
        {
            binding.uiElement.SetActive(false);
        }
    }
}
