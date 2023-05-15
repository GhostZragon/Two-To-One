using UnityEngine;
using UnityEngine.UI;


public class ScorePopUp : QuangLibrary
{
    public float timeScale = 1f;
    public float timeMove = 1f;
    public float timeFade = 1f;
    public float localYPosition = 30f;
    public Color color = new Color(1, 1, 1, 0);
    public Text TextPrefab;
    public GameObject TextsHolder;

    protected override void Awake()
    {
        base.Awake();

    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadPrefab();
        LoadTextsHolder();
        LoadTimeValue();
    }
    protected virtual void LoadTimeValue()
    {
        timeScale = 0.5f;
        timeMove = 0.5f;
        timeFade = 0.5f;
        localYPosition = 30f;
    }
    protected virtual void LoadTextsHolder()
    {
        if (this.TextsHolder != null) return;
        this.TextsHolder = transform.Find("TextsHolder").gameObject;
    }
    protected virtual void LoadPrefab()
    {
        if (this.TextPrefab != null) return;
        TextPrefab = GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Spawn a Text with there effect : scale, move to Y, fade
    /// </summary>
    /// <returns>Return a Text GameObject </returns>
    public Text CreatePopUpText()
    {
        var go = Instantiate(TextPrefab, TextsHolder.transform);
        go.gameObject.SetActive(true);
        go.transform.localScale = Vector3.zero;
        go.gameObject.SetActive(true);
        ScaleUpText(go);
        return go;

    }
    public void ScaleUpText(Text go)
    {
        float scaleNumber = 1.3f;
        Vector3 nextScale = new Vector3(scaleNumber, scaleNumber, scaleNumber);

        go.transform.LeanScale(nextScale, this.timeScale).setEaseInOutBounce().setOnComplete(() =>
        {
            //Debug.Log("Text is scale");
            MoveTextUp(go);
        });
    }
    public void MoveTextUp(Text go)
    {
        float currentTextLocalPosition = go.rectTransform.localPosition.y;
        currentTextLocalPosition += localYPosition;
        go.transform.LeanMoveLocalY(currentTextLocalPosition, this.timeMove).setOnComplete(() =>
        {
            FadeText(go);
        });
    }
    public void FadeText(Text go)
    {
        LeanTween.colorText(go.rectTransform, this.color, timeFade).setOnComplete(() =>
        {
            Destroy(go.gameObject, 0.5f);
        });
        go.transform.LeanScale(Vector3.zero, this.timeFade).setEaseInBack();
    }
}
