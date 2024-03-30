using UnityEngine;

public class LockTransform : MonoBehaviour
{
    private Vector3 initialPosition; // ���ڴ洢�����ʼλ�õı���
    private Quaternion initialRotation; // ���ڴ洢�����ʼ��ת�ı���

    void Start()
    {
        // ����Ϸ��ʼʱ��¼����ĳ�ʼλ�ú���ת
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // ��ÿһ֡�У��������λ�ú���ת����Ϊ���ʼֵ
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
