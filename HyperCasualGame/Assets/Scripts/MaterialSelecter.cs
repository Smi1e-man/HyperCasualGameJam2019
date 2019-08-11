using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSelecter : MonoBehaviour
{
    private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = ThemeManager.Instance.Material;
    }
}
