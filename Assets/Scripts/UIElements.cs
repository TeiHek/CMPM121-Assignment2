using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIElements : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;
    public static UIElements Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public float GetSliderValue()
    {
        return slider.value;
    }

    private void Update()
    {
        text.text = "Lights: " + Mathf.Round(GetSliderValue() * 100) + "%";
    }
}
