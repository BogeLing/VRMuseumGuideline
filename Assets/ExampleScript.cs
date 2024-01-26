using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Bounds bounds = meshRenderer.bounds;
            Debug.Log("物体的边界框大小: " + bounds.size);
        }
        else
        {
            Debug.Log("未找到MeshRenderer组件");
        }
    }
}
