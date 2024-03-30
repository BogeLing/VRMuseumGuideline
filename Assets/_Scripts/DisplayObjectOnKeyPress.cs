using UnityEngine;

public class ToggleExternalObjectVisibilityOnKeyPress : MonoBehaviour
{
    // ����������������Unity�༭���������ⲿ����
    public GameObject externalObject;
    public GameObject ball;
    // ��ʼʱ���ⲿ��������Ϊ���ɼ�
    void Start()
    {
        if (externalObject != null)
        {
            externalObject.SetActive(false);
            ball.SetActive(true);
        }
    }

    // ÿһ֡����Ƿ�����A��
    void Update()
    {
        // ����Ƿ�����A��
        if (Input.GetKeyDown(KeyCode.A))
        {
            // ����Ѿ��������ⲿ����
            if (externalObject != null)
            {
                // �л��ⲿ�������ʾ/����״̬
                externalObject.SetActive(!externalObject.activeSelf);
                ball.SetActive(!externalObject.activeSelf);
            }
        }
    }
}