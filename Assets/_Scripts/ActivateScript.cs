using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateScript : MonoBehaviour
{
    public InputAction activateAction; // ���ڼ���ű���Input Action
    private GameObject canvasGameObject; // ���ڴ洢Canvas GameObject������

    private void Start()
    {
        // ����������ʱ�ҵ����洢Canvas GameObject������
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
            // ��ȡ������ResetPosition�ű�
            var resetScript = GetComponent<ResetPosition>();
            if (resetScript != null)
            {
                resetScript.enabled = true;
            }

            // ���Canvas GameObject�ѱ��ҵ���ȷ�����Ǽ����
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