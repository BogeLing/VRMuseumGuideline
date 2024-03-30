using UnityEngine;

public class ContinuousMatchTransform : MonoBehaviour
{
    public Transform targetObject; // 目标物体
    public Transform positionObject; // 位置匹配物体
    public Transform rotationObject; // 旋转匹配物体

    void Update()
    {
        if (targetObject != null && positionObject != null && rotationObject != null)
        {
            // 实时更新目标物体的位置为位置匹配物体的位置
            targetObject.position = positionObject.position;

            // 实时更新目标物体的旋转为旋转匹配物体的旋转
            targetObject.rotation = rotationObject.rotation;
        }
    }
}