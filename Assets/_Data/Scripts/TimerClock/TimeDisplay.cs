using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeDisplay : QuangLibrary
{
    public Image image;
    [SerializeField] protected TimerManager timerManager;
    public Transform displayHolder;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTimerManager();
        this.LoadDisplayHolder();
        this.LoadImage();
    }
    private void LoadDisplayHolder()
    {
        if (displayHolder != null) return;
        displayHolder = GameObject.Find("_DisplayHolder").transform;
    }
    private void LoadImage()
    {
        if (image != null) return;
        image = displayHolder.Find("FillImage").GetComponent<Image>();
    }



    private void LoadTimerManager()
    {
        if (timerManager != null) return;
        timerManager = GetComponent<TimerManager>();
    }

    public void Start()
    {
        image.fillAmount = 1;
    }
    // Update is called once per frame
    void Update()
    {
        image.fillAmount = Mathf.InverseLerp(0, timerManager._defaultPlayTime, timerManager._currentPlayTime);
    }

}
