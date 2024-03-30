using UnityEngine;

public class RaycastFromCube : MonoBehaviour
{
    public float rayLength = 100.0f;
    private LineRenderer lineRenderer;
    public LayerMask layerMask; // 添加一个公共变量来设置LayerMask

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * rayLength;

        // 在Raycast函数中使用layerMask来限制检测的层
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, layerMask))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            Debug.Log("Ray hit: " + hit.collider.gameObject.name + " at " + hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * rayLength);
        }
    }
}
