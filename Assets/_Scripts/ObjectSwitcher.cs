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

        switchNextAction.performed += _ => SwitchObject(1);
        switchPreviousAction.performed += _ => SwitchObject(-1);
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
                    obj.SetActive(false); // Ĭ������Ϊ����
                }
            }
        }


        // �����б��еĵ�һ������
        if (objects.Count > 0)
        {
            currentIndex = 0;
            objects[currentIndex].SetActive(true);
        }
    }

    private void SwitchObject(int direction)
    {
        if (objects.Count == 0)
            return;

        // ���ص�ǰ����
        objects[currentIndex].SetActive(false);

        // �����µ�����ֵ
        currentIndex += direction;
        if (currentIndex >= objects.Count)
            currentIndex = 0;
        else if (currentIndex < 0)
            currentIndex = objects.Count - 1;

        // ��ʾ�µ�����
        objects[currentIndex].SetActive(true);
    }
}
