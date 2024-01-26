using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResetPosition : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private XRGrabInteractable grabInteractable; // 抓取组件

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        grabInteractable = GetComponent<XRGrabInteractable>(); // 获取XRGrabInteractable组件

        // 每隔0.5秒调用一次CheckAndResetPosition方法
        InvokeRepeating("CheckAndResetPosition", 0.5f, 0.5f);
    }

    private void CheckAndResetPosition()
    {
        // 检查物体是否正在被抓取
        if (grabInteractable != null && !grabInteractable.isSelected)
        {
            // 如果物体未被抓取，重置其位置和旋转
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}
