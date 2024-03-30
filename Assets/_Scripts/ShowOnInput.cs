using UnityEngine;
using UnityEngine.InputSystem;

public class ShowObjectOnInput : MonoBehaviour
{
    public InputAction showAction; // 用于触发显示操作的Input Action
    public GameObject objectToShow; // 在Inspector面板中指定的要显示的物体

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
        // 检查是否已指定了要显示的物体
        if (objectToShow != null)
        {
            // 显示指定的物体
            objectToShow.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No object assigned to ShowObjectOnInput script on " + gameObject.name);
        }
    }
}