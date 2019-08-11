using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSelecter : MonoBehaviour
{
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = ThemeManager.Instance.Sprite;
    }

}
