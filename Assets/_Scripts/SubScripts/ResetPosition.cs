using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetPosition : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private XRGrabInteractable grabInteractable; // ץȡ���

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        grabInteractable = GetComponent<XRGrabInteractable>(); // ��ȡXRGrabInteractable���

        // ÿ��0.5�����һ��CheckAndResetPosition����
        InvokeRepeating("CheckAndResetPosition", 0.5f, 0.5f);
    }

    private void CheckAndResetPosition()
    {
        // ��������Ƿ����ڱ�ץȡ
        if (grabInteractable != null && !grabInteractable.isSelected)
        {
            // �������δ��ץȡ��������λ�ú���ת
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}
