using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Bounds bounds = meshRenderer.bounds;
            Debug.Log("����ı߽���С: " + bounds.size);
        }
        else
        {
            Debug.Log("δ�ҵ�MeshRenderer���");
        }
    }
}
