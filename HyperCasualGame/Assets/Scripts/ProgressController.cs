using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressController : MonoBehaviour
{
    static ProgressController instance;
    public static ProgressController Instance { get { return instance; } }

    [SerializeField] Image _image;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeValue(float value)
    {
        _image.fillAmount = value;
    }
}