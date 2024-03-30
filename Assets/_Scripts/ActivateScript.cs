using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateScript : MonoBehaviour
{
    public InputAction activateAction; // 用于激活脚本的Input Action
    private GameObject canvasGameObject; // 用于存储Canvas GameObject的引用

    private void Start()
    {
        // 尝试在启动时找到并存储Canvas GameObject的引用
        Transform canvasTransform = transform.parent.parent.Find("Canvas");
        if (canvasTransform != null)
        {
            canvasGameObject = canvasTransform.gameObject;
        }
        else
        {
            Debug.Log("Canvas GameObject not found.");
        }
    }

    private void OnEnable()
    {
        activateAction.Enable();
        activateAction.performed += OnActivatePerformed;
    }

    private void OnDisable()
    {
        activateAction.Disable();
        activateAction.performed -= OnActivatePerformed;
    }


    private void OnActivatePerformed(InputAction.CallbackContext context)
    {
        if (gameObject.activeSelf)
        {
            // 获取并启用ResetPosition脚本
            var resetScript = GetComponent<ResetPosition>();
            if (resetScript != null)
            {
                resetScript.enabled = true;
            }

            // 如果Canvas GameObject已被找到，确保它是激活的
            if (canvasGameObject != null)
            {
                canvasGameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("GameObject is not active. ResetPosition script will not be enabled.");
        }
    }
}