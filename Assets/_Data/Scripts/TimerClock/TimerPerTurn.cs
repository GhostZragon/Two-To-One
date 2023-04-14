using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerPerTurn : QuangLibrary
{
    public Slider slider;
    public TextMeshProUGUI timerText;
    public float timeToEndTurn = 10f;
    public float timer = 10;
    public float scaleSpeed = 1;
    public int speed = 1;
    public float currentVelocity = 0f;
    public bool isTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
            float currentTimer = Mathf.SmoothDamp(slider.value, timer / timeToEndTurn, ref currentVelocity, Time.deltaTime);
            timerText.text = $"{Mathf.FloorToInt(timer / 60).ToString("00")} : {Mathf.FloorToInt(timer % 60).ToString("00")}";
            slider.value = currentTimer;
        }
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
