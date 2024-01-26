using UnityEngine;

public class MaintainParent : MonoBehaviour
{
    private Transform originalParent; // ������ԭʼ�ĸ�����

    private void Start()
    {
        // �洢��Ϸ��ʼʱ�ĸ�����
        originalParent = transform.parent;

        // ÿ��һ��ʱ���ظ�����ָ��������ȷ�������屣����ԭʼ��������
        InvokeRepeating("CheckAndMaintainOriginalParent", 1f, 1f);
    }

    private void CheckAndMaintainOriginalParent()
    {
        // ��������Ƿ���Ȼ��ԭʼ�ĸ�������
        if (transform.parent != originalParent)
        {
            // ������ǣ����������·Ż�ԭʼ��������
            transform.SetParent(originalParent, true); // �ڶ�������Ϊtrue�����������겻��
        }
    }
}
