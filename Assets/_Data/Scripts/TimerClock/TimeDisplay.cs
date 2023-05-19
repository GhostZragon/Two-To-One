using System.Collections;
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
        if (!GameManager.Instance.IsCounting) return;
        UpdateFillAmount(0, timerManager._defaultPlayTime, timerManager._currentPlayTime);
    }
    public void UpdateFillAmount(float minValue, float maxValue, float currentValue)
    {
        image.fillAmount = Mathf.InverseLerp(minValue, maxValue, currentValue);
    }
    public void SetFillAmount(float value)
    {
        image.fillAmount = value;
    }
    public IEnumerator FillRefreshTime()
    {
        float time = 1;
        image.fillAmount = 1;
        while (time >= 0)
        {
            image.fillAmount = time;
            time -= Time.deltaTime;
            yield return null;
        }
    }

}
