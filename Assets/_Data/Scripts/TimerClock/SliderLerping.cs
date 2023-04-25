using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[CustomEditor(typeof(SliderLerping))]
public class CustomSliderLerping : Editor
{
    private SliderLerping sliderLerping;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SliderLerping sliderLerping = (SliderLerping)target;
        if(GUILayout.Button("Reset Timer"))
        {
            sliderLerping.ResetTimer();
        }
    }
}
public class SliderLerping : QuangLibrary
{
    [SerializeField] protected Color[] colors;
    [SerializeField] protected Image image;
    [SerializeField] protected int colorIndex;
    [SerializeField] protected ScoreManager scoreManager;
    public Color currentColor;
    public Slider slider;
    [Min(-1)]
    public int stateIndex = -1;
    [Min(0)]
    public float time = 10;
    public float temp;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadScoreManager();
    }
    protected virtual void LoadScoreManager()
    {
        if (this.scoreManager != null) return;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }
    private void Start()
    {
        temp = colors.Length;
        slider.maxValue = time;
    }
    public void ResetTimer()
    {
        temp = colors.Length;
        stateIndex = -1;
        value = 2;
        lerpDone = false;
        time = 10;

    }
    private void Update()
    {
        GetTime();
        //Test();
    }
    [SerializeField][Range(0, 2)] float value;
    bool lerpDone = false;

    public float _A;
    public void GetTime()
    {
        if (time < 0) return;
        time -= Time.deltaTime;
        slider.value = time;

        if (time <= (10 * (temp / colors.Length)) && time >= 0)
        {
            lerpDone = false;
            value = 2;
            //LeanTween.value(gameObject, colors[stateIndex], colors[stateIndex + 1], 1f);
            temp--;
            stateIndex++;
            currentColor = colors[stateIndex];
            
        }
        if (lerpDone == false && stateIndex < colors.Length - 1 && stateIndex > -1)
        {
            value -= Time.deltaTime;
            _A = ReturnLerpValue(0, 2, value);
            image.color = Color.Lerp(colors[stateIndex + 1], colors[stateIndex], _A);
            if (value < 0)
            {
                lerpDone = true;
            }
        }
        else if(stateIndex < colors.Length - 1 && stateIndex > -1)
        {
            image.color = colors[stateIndex + 1];
        }
        else
        {
            image.color = colors[stateIndex];
        }
    }

    public void LerpColor(float newValue)
    {
        float value = newValue;
        float time = 0;
        switch (value)
        {
            case >= (float)2 / 3:
                time = Mathf.InverseLerp((float)2 / 3, 1, value);
                break;
            case >= (float)1 / 3:
                time = Mathf.InverseLerp((float)1 / 3, (float)2 / 3, value);
                break;
            case >= 0:
                time = Mathf.InverseLerp(0, (float)1 / 3, value);
                break;
            default:
                break;
        }
        image.color = Color.Lerp(colors[colorIndex + 1], colors[colorIndex], time);
    }
}

