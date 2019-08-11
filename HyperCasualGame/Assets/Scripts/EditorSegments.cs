using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSegments : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    [ExecuteInEditMode]
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(transform.position.x);
        pos.z = Mathf.Round(transform.position.z);
        transform.position = pos;
    }
}
