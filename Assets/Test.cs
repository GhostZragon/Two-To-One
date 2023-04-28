using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : QuangLibrary
{
    public Image image;
    public TextMeshProUGUI text;
    [Range(0,1)]public float value;
    public int textValue;
    public float time = 10;
    private void Start()
    {
        value = 1;
        textValue = 600;
    }
    private void Update()
    {
        time -= Time.deltaTime;
        if (time < 0) return;
        image.fillAmount = ReturnLerpValue(1, 10, time);

        text.text = $"{textValue.ToString()}/600";
    }
}
