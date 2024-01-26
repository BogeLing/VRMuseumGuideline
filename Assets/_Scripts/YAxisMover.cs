using UnityEngine;
using UnityEngine.InputSystem;

public class YAxisMover : MonoBehaviour
{
    public InputAction actionIncreaseY;
    public InputAction actionDecreaseY;

    public float moveSpeed = 1.0f; // 每秒Y轴移动的单位数

    private void OnEnable()
    {
        actionIncreaseY.Enable();
        actionDecreaseY.Enable();
    }

    private void OnDisable()
    {
        actionIncreaseY.Disable();
        actionDecreaseY.Disable();
    }

    void Update()
    {
        if (actionIncreaseY.ReadValue<float>() == 1)
        {
            // 增加Y坐标
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        else if (actionDecreaseY.ReadValue<float>() == 1)
        {
            // 减小Y坐标
            transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
        }
    }
}
