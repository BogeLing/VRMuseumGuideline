using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public class ActionUIBinding
    {
        public InputAction action; // 直接使用InputAction
        public GameObject uiElement; // 对应的UI元素
    }

    public List<ActionUIBinding> actionBindings; // 动作和UI元素的绑定列表

    private void OnEnable()
    {
        // 注册所有动作的触发事件
        foreach (var binding in actionBindings)
        {
            binding.action.Enable();
            binding.action.performed += context => OnActionPerformed(binding);
        }
    }

    private void OnDisable()
    {
        // 注销所有动作的触发事件
        foreach (var binding in actionBindings)
        {
            binding.action.Disable();
            binding.action.performed -= context => OnActionPerformed(binding);
        }
    }

    private void OnActionPerformed(ActionUIBinding binding)
    {
        // 隐藏所有UI元素
        HideAllUIElements();

        // 显示触发动作对应的UI元素
        binding.uiElement.SetActive(true);
    }

    private void HideAllUIElements()
    {
        // 遍历列表，隐藏每个UI元素
        foreach (var binding in actionBindings)
        {
            binding.uiElement.SetActive(false);
        }
    }
}
