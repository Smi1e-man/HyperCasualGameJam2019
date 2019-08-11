using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    static private ThemeManager instance;
    static public ThemeManager Instance { get { return instance; } }

    [SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();

    private int themeID;

    private void Awake()
    {
        instance = this;
        themeID = Random.Range(0,materials.Count);
    }

    public Material Material { get { return materials[themeID]; } }
    public Sprite Sprite { get { return sprites[themeID]; } }
}
