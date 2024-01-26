using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectScaler : MonoBehaviour
{
    private GameObject parentObject; // 父物体，现在指向脚本附加的物体
    private GameObject childObject; // 父物体的子物体
    public GameObject Lefthand; // 左手控制器
    public GameObject Righthand; // 右手控制器

    private Vector3 initialHandDistance; // 初始时两手之间的距离
    private Vector3 initialScale; // 父物体的初始缩放值
    private bool isScaling = false; // 是否正在缩放的标志

    public InputActionProperty leftGrip; // 左手触发器的输入动作

    private XRGrabInteractable grabInteractable; // 子物体的XRGrabInteractable组件

    private void OnEnable()
    {
        parentObject = gameObject;

        if (parentObject.transform.childCount > 0)
        {
            childObject = parentObject.transform.GetChild(0).gameObject;
            grabInteractable = childObject.GetComponent<XRGrabInteractable>();
        }

        leftGrip.action.Enable(); // 启用左手触发器动作
        leftGrip.action.performed += OnTriggerChanged; // 订阅触发器变化事件
        leftGrip.action.canceled += OnTriggerChanged; // 订阅触发器取消事件
    }

    private void OnDisable()
    {
        leftGrip.action.Disable(); // 禁用左手触发器动作
        leftGrip.action.performed -= OnTriggerChanged; // 取消订阅触发器变化事件
        leftGrip.action.canceled -= OnTriggerChanged; // 取消订阅触发器取消事件
    }

    private void Start()
    {
        initialHandDistance = Righthand.transform.position - Lefthand.transform.position;
        initialScale = parentObject.transform.localScale;
    }

    private void Update()
    {
        if (isScaling && grabInteractable != null && grabInteractable.isSelected)
        {
            Vector3 currentHandDistance = Righthand.transform.position - Lefthand.transform.position;
            float distanceRatio = currentHandDistance.magnitude / initialHandDistance.magnitude;
            parentObject.transform.localScale = initialScale * distanceRatio;
        }
    }

    private void OnTriggerChanged(InputAction.CallbackContext context)
    {
        isScaling = context.ReadValue<float>() > 0.1f;
        if (isScaling)
        {
            initialHandDistance = Righthand.transform.position - Lefthand.transform.position;
            initialScale = parentObject.transform.localScale;
        }
    }
}
