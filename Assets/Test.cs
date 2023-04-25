using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Color[] colors;
    public Color currentColor;
    public int stateIndex = -1;
    public float time = 10;
    public float temp;
    public Slider slider;
    public Image image;
    private void Start()
    {
        temp = colors.Length;
        slider.maxValue = time;
    }
    private void Update()
    {
        JustTest();

    }
    void JustTest()
    {
        if (time < 0) return;
        time -= Time.deltaTime;
        slider.value = time;

        if (time <= (10 * (temp / colors.Length)) && time >= 0)
        {
            temp--;
            stateIndex++;
            currentColor = colors[stateIndex];
            
            image.color = currentColor;
        }
    }
}
