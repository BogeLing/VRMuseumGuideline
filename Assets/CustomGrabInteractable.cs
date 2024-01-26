using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabInteractable : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // ���浱ǰ������
        Transform originalParent = transform.parent;
        base.OnSelectEntered(args);
        // ���踸����
        transform.parent = originalParent;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // ���浱ǰ������
        Transform originalParent = transform.parent;
        base.OnSelectExited(args);
        // ���踸����
        transform.parent = originalParent;
    }
}
