using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerPerTurn : QuangLibrary
{
    [SerializeField] protected Slider slider;
    [SerializeField] protected TextMeshProUGUI timerText;
    [SerializeField] protected SliderLerping sliderLerping;
    [SerializeField] protected float timeToEndTurn = 10f;
    [SerializeField] protected float timer = 10;
    [SerializeField] protected float scaleSpeed = 1;
    [SerializeField] protected int speed = 1;
    [SerializeField] protected float currentVelocity = 0f;
    [SerializeField] protected bool isTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        ShowTimerUI();
    }

    public void ShowTimerUI()
    {
        slider.transform.localScale = Vector3.zero;
        slider.transform.LeanScale(Vector3.one, scaleSpeed).setEaseOutBack();
        //slider.transform.LeanScale(Vector3.one, scaleSpeed).setEaseInOutQuint();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTimerText();
        this.LoadSlider();
        LoadSliderLerping();
    }
    protected virtual void LoadSliderLerping()
    {
        if (sliderLerping != null) return;
        sliderLerping = GetComponent<SliderLerping>();
    }
    protected virtual void LoadTimerText()
    {
        if (timerText != null) return;
        timerText = GetComponentInChildren<TextMeshProUGUI>();
    }
    protected virtual void LoadSlider()
    {
        if (slider != null) return;
        slider = GetComponentInChildren<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        TimerToText();

        LoadColorSlider();
    }
    private void TimerToText()
    {
        if (!isTimer) return;
        if (timer > 0)
        {

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                timerText.text = "00 : 00";
                slider.value = 0;
            }
            //Mathf.Round(timer);
            //slider.value = Mathf.Lerp(slider.value, timer / 10, speed);
            float currentTimer = Mathf.SmoothDamp(slider.value, timer / timeToEndTurn, ref currentVelocity, speed*Time.deltaTime);
            timerText.text = $"{Mathf.FloorToInt(timer / 60).ToString("00")} : {Mathf.FloorToInt(timer % 60).ToString("00")}";
            slider.value = currentTimer;

        }
    }
    public void LoadColorSlider()
    {
        sliderLerping.LerpColor(slider.value);
    }

    public void TimerPlus()
    {
        timer += 1;
    }
    public void ResetTimer()
    {
        timer = 10f;
    }

    public void ResumeTime()
    {
        isTimer = true;
    }

    public void StopTime()
    {
        isTimer = false;
    }
}
