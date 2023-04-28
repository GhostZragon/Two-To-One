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
        if (GUILayout.Button("Reset Timer"))
        {
            sliderLerping.ResetTimer();
        }
    }
}
public class SliderLerping : QuangLibrary
{
    [SerializeField] protected Color[] colors;
    [SerializeField] protected Image image;
    [SerializeField] bool isTimer = false;
    [SerializeField] protected ScoreManager scoreManager;
    [SerializeField] protected Color currentColor;
    [SerializeField] protected Slider slider;
    [Min(-1)] public int stateIndex = -1;

    [Min(0)] protected float time = 10;
    [SerializeField][Min(0)] protected int timePerTurn = 10;
    [Min(0)] protected float temp;
    [Range(0, 2)] public float value;
    [SerializeField] float maxTimeFadeColor = 2;
    private bool lerpDone = false;

    public Text timeText;
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
        time = timePerTurn;
        slider.maxValue = time;
        isTimer = true;
        lerpDone = true;
    }
    public void ResetTimer()
    {
        temp = colors.Length ;
        stateIndex = -1;
        value = 2;
        lerpDone = false;
        time = timePerTurn;
        slider.maxValue = time;
    }
    private void Update()
    {
        if (!isTimer) return;
        GetTime();
        timeText.text = Mathf.Round(time).ToString();
    }
    [Range(0, 2)] float _A;
    public void GetTime()
    {
        if (time < 0) return;
        time -= Time.deltaTime;
        slider.value = time;
        //Logic khi thoi gian cham moc thoi gian de chuyen mau
        if (time <= (timePerTurn * (temp / colors.Length)) && time > 0)
        {
            Debug.Log(timePerTurn * (temp / colors.Length));
            //reset value for lerp color
            lerpDone = false;
            value = 2;
            temp--;
            stateIndex++;
            currentColor = colors[stateIndex];

        }
        
        //logic for current image color
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
        else if (stateIndex < colors.Length - 1 && stateIndex > -1)
        {
            image.color = colors[stateIndex + 1];
        }
        else if (stateIndex < colors.Length )
        {
            image.color = colors[stateIndex];
        }
    }

    private void SetColor()
    {

    }

}

