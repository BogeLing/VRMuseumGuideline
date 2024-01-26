using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrabInteractable : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // 保存当前父物体
        Transform originalParent = transform.parent;
        base.OnSelectEntered(args);
        // 重设父物体
        transform.parent = originalParent;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // 保存当前父物体
        Transform originalParent = transform.parent;
        base.OnSelectExited(args);
        // 重设父物体
        transform.parent = originalParent;
    }
}
