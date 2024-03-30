using UnityEngine;
using UnityEngine.UI; // 对于普通的Text组件
using TMPro; // 对于TextMeshPro组件

public class SliderValueToText : MonoBehaviour
{
    public Slider Shape;
    public Slider Texture; // 引用滑块
    public TextMeshProUGUI ShapeText;
    public TextMeshProUGUI TextureText;// 对于TextMeshPro的文本

    void Start()
    {
        if (Shape != null)
        {
            // 注册滑块的值变化事件
            Shape.onValueChanged.AddListener(delegate { UpdateShapeTextDisplay(); });
            UpdateShapeTextDisplay(); // 初始时也更新文本
        }

        if (Texture != null)
        {
            // 注册滑块的值变化事件
            Texture.onValueChanged.AddListener(delegate { UpdateTextureTextDisplay(); });
            UpdateTextureTextDisplay(); // 初始时也更新文本
        }
    }

    // 更新文本显示的方法
    public void UpdateShapeTextDisplay()
    {
        if (ShapeText != null)
        {
            ShapeText.text = Shape.value.ToString(); // 显示两位小数的滑块值
        }
    }

    public void UpdateTextureTextDisplay()
    {
        if (TextureText != null)
        {

            TextureText.text = Texture.value.ToString(); // 显示两位小数的滑块值
        }
    }
}
