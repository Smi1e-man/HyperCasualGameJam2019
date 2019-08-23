using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSegments : MonoBehaviour
{
    [ExecuteInEditMode]
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(transform.position.x);
        pos.z = Mathf.Round(transform.position.z);
        transform.position = pos;
    }
}
