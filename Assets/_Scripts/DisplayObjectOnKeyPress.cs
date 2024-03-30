using UnityEngine;

public class ToggleExternalObjectVisibilityOnKeyPress : MonoBehaviour
{
    // 公开变量，用于在Unity编辑器中引用外部物体
    public GameObject externalObject;
    public GameObject ball;
    // 开始时将外部物体设置为不可见
    void Start()
    {
        if (externalObject != null)
        {
            externalObject.SetActive(false);
            ball.SetActive(true);
        }
    }

    // 每一帧检查是否按下了A键
    void Update()
    {
        // 检查是否按下了A键
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 如果已经设置了外部物体
            if (externalObject != null)
            {
                // 切换外部物体的显示/隐藏状态
                externalObject.SetActive(!externalObject.activeSelf);
                ball.SetActive(!externalObject.activeSelf);
            }
        }
    }
}