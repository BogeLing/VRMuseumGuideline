using UnityEngine;

public class ContinuousMatchTransform : MonoBehaviour
{
    public Transform targetObject; // Ŀ������
    public Transform positionObject; // λ��ƥ������
    public Transform rotationObject; // ��תƥ������

    void Update()
    {
        if (targetObject != null && positionObject != null && rotationObject != null)
        {
            // ʵʱ����Ŀ�������λ��Ϊλ��ƥ�������λ��
            targetObject.position = positionObject.position;

            // ʵʱ����Ŀ���������תΪ��תƥ���������ת
            targetObject.rotation = rotationObject.rotation;
        }
    }
}