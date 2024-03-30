using UnityEngine;

public class LockTransform : MonoBehaviour
{
    private Vector3 initialPosition; // 用于存储物体初始位置的变量
    private Quaternion initialRotation; // 用于存储物体初始旋转的变量

    void Start()
    {
        // 在游戏开始时记录物体的初始位置和旋转
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // 在每一帧中，将物体的位置和旋转重置为其初始值
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
