using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SliderLerping : MonoBehaviour
{
    [SerializeField] protected Color[] colors;
    [SerializeField] protected Image image;
    [SerializeField] protected int colorIndex;
    public void LerpColor(float newValue)
    {
        float value = newValue;
        float time = 0;
        switch (value)
        {
            case >= (float)2 / 3:
                colorIndex = 0;
                time = Mathf.InverseLerp((float)2 / 3, 1, value);
                //time = (value - 0.7f) / (1 - 0.7f);
                break;
            case >= (float)1 / 3:
                colorIndex = 1;
                time = Mathf.InverseLerp((float)1 / 3, (float)2 / 3, value);
                //time = (value - 0.25f) / (0.5f - 0.25f);
                break;
            case >= 0:
                colorIndex = 2;
                time = Mathf.InverseLerp(0, (float)1 / 3, value);
                //time = (value - 0.0f) / (0.25f - 0);
                break;
            default:
                break;
        }

        image.color = Color.Lerp(colors[colorIndex + 1], colors[colorIndex], time);
    }
    


}

