using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectScaler : MonoBehaviour
{
    private GameObject parentObject; // �����壬����ָ��ű����ӵ�����
    private GameObject childObject; // �������������
    public GameObject Lefthand; // ���ֿ�����
    public GameObject Righthand; // ���ֿ�����

    private Vector3 initialHandDistance; // ��ʼʱ����֮��ľ���
    private Vector3 initialScale; // ������ĳ�ʼ����ֵ
    private bool isScaling = false; // �Ƿ��������ŵı�־

    public InputActionProperty leftGrip; // ���ִ����������붯��

    private XRGrabInteractable grabInteractable; // �������XRGrabInteractable���

    private void OnEnable()
    {
        parentObject = gameObject;

        if (parentObject.transform.childCount > 0)
        {
            childObject = parentObject.transform.GetChild(0).gameObject;
            grabInteractable = childObject.GetComponent<XRGrabInteractable>();
        }

        leftGrip.action.Enable(); // �������ִ���������
        leftGrip.action.performed += OnTriggerChanged; // ���Ĵ������仯�¼�
        leftGrip.action.canceled += OnTriggerChanged; // ���Ĵ�����ȡ���¼�
    }

    private void OnDisable()
    {
        leftGrip.action.Disable(); // �������ִ���������
        leftGrip.action.performed -= OnTriggerChanged; // ȡ�����Ĵ������仯�¼�
        leftGrip.action.canceled -= OnTriggerChanged; // ȡ�����Ĵ�����ȡ���¼�
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
