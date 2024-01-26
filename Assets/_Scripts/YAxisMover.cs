using UnityEngine;
using UnityEngine.InputSystem;

public class YAxisMover : MonoBehaviour
{
    public InputAction actionIncreaseY;
    public InputAction actionDecreaseY;

    public float moveSpeed = 1.0f; // ÿ��Y���ƶ��ĵ�λ��

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
            // ����Y����
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        else if (actionDecreaseY.ReadValue<float>() == 1)
        {
            // ��СY����
            transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
        }
    }
}
