using UnityEngine;
using UnityEngine.UI; // ������ͨ��Text���
using TMPro; // ����TextMeshPro���

public class SliderValueToText : MonoBehaviour
{
    public Slider Shape;
    public Slider Texture; // ���û���
    public TextMeshProUGUI ShapeText;
    public TextMeshProUGUI TextureText;// ����TextMeshPro���ı�

    void Start()
    {
        if (Shape != null)
        {
            // ע�Ử���ֵ�仯�¼�
            Shape.onValueChanged.AddListener(delegate { UpdateShapeTextDisplay(); });
            UpdateShapeTextDisplay(); // ��ʼʱҲ�����ı�
        }

        if (Texture != null)
        {
            // ע�Ử���ֵ�仯�¼�
            Texture.onValueChanged.AddListener(delegate { UpdateTextureTextDisplay(); });
            UpdateTextureTextDisplay(); // ��ʼʱҲ�����ı�
        }
    }

    // �����ı���ʾ�ķ���
    public void UpdateShapeTextDisplay()
    {
        if (ShapeText != null)
        {
            ShapeText.text = Shape.value.ToString(); // ��ʾ��λС���Ļ���ֵ
        }
    }

    public void UpdateTextureTextDisplay()
    {
        if (TextureText != null)
        {

            TextureText.text = Texture.value.ToString(); // ��ʾ��λС���Ļ���ֵ
        }
    }
}
