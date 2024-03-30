using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSwitcher : MonoBehaviour
{
    public InputAction switchNextAction;
    public InputAction switchPreviousAction;

    public List<GameObject> objects = new List<GameObject>();
    private int currentIndex = -1;

    private void OnEnable()
    {
        switchNextAction.Enable();
        switchPreviousAction.Enable();

        switchNextAction.performed += _ => SwitchParentObject(1);
        switchPreviousAction.performed += _ => SwitchParentObject(-1);
    }

    private void OnDisable()
    {
        switchNextAction.Disable();
        switchPreviousAction.Disable();
    }

    private void Start()
    {
        // ��������a.b��ʽ���������岢��ӵ��б�
        for (int a = 1; a <= 10; a++)
        {
            for (int b = 1; b <= 20; b++)
            {
                string objectName = $"{a}.{b}";
                GameObject obj = GameObject.Find(objectName);
                if (obj != null)
                {
                    Debug.Log(obj.name);
                    objects.Add(obj);
                    // Ĭ�����ø�����Ϊ���أ�ȷ�����������
                    if (obj.transform.parent != null)
                    {
                        obj.transform.parent.gameObject.SetActive(false);
                    }
                }
            }
        }

        // �����б��е�һ������ĸ�����
        if (objects.Count > 0)
        {
            currentIndex = 0;
            if (objects[currentIndex].transform.parent != null)
            {
                objects[currentIndex].transform.parent.gameObject.SetActive(true);
            }
        }
    }

    private void SwitchParentObject(int direction)
    {
        if (objects.Count == 0)
            return;

        // �����µ�����ֵ
        int newIndex = currentIndex + direction;

        // ����������Ƿ񳬳���Χ������ֹѭ��
        if (newIndex >= objects.Count || newIndex < 0)
        {
            // ������Χʱ��ִ���κβ���
            return;
        }

        // ���ص�ǰ����ĸ�����
        if (objects[currentIndex].transform.parent != null)
        {
            objects[currentIndex].transform.parent.gameObject.SetActive(false);
        }

        // ��������
        currentIndex = newIndex;

        // ��ʾ�µ�����ĸ�����
        if (objects[currentIndex].transform.parent != null)
        {
            objects[currentIndex].transform.parent.gameObject.SetActive(true);
        }
    }
}
