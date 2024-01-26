using UnityEngine;

public class MaintainParent : MonoBehaviour
{
    private Transform originalParent; // 子物体原始的父物体

    private void Start()
    {
        // 存储游戏开始时的父物体
        originalParent = transform.parent;

        // 每隔一定时间重复调用指定方法，确保子物体保持在原始父物体下
        InvokeRepeating("CheckAndMaintainOriginalParent", 1f, 1f);
    }

    private void CheckAndMaintainOriginalParent()
    {
        // 检查物体是否仍然在原始的父物体下
        if (transform.parent != originalParent)
        {
            // 如果不是，将物体重新放回原始父物体下
            transform.SetParent(originalParent, true); // 第二个参数为true保持世界坐标不变
        }
    }
}
