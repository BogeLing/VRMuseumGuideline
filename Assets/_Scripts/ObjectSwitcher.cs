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
        // 查找所有a.b格式命名的物体并添加到列表
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
                    // 默认设置父物体为隐藏，确保父物体存在
                    if (obj.transform.parent != null)
                    {
                        obj.transform.parent.gameObject.SetActive(false);
                    }
                }
            }
        }

        // 激活列表中第一个物体的父物体
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

        // 计算新的索引值
        int newIndex = currentIndex + direction;

        // 检查新索引是否超出范围，并阻止循环
        if (newIndex >= objects.Count || newIndex < 0)
        {
            // 超出范围时不执行任何操作
            return;
        }

        // 隐藏当前物体的父物体
        if (objects[currentIndex].transform.parent != null)
        {
            objects[currentIndex].transform.parent.gameObject.SetActive(false);
        }

        // 更新索引
        currentIndex = newIndex;

        // 显示新的物体的父物体
        if (objects[currentIndex].transform.parent != null)
        {
            objects[currentIndex].transform.parent.gameObject.SetActive(true);
        }
    }
}
