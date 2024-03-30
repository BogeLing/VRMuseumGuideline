using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null) 
        {
            Bounds bounds = meshRenderer.bounds;
            Debug.Log("物体的边界框大小：" + bounds.size);
        }
        else
        {
            Debug.Log("未找到组件");
        }
    }

   
}
