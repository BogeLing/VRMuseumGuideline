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
            Debug.Log("����ı߽���С��" + bounds.size);
        }
        else
        {
            Debug.Log("δ�ҵ����");
        }
    }

   
}
