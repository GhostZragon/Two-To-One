using UnityEngine;
using UnityEngine.UI;


public class ScorePopUp : QuangLibrary
{
    [SerializeField] protected float timeScale = 1f;
    [SerializeField] protected float timeMove = 1f;
    [SerializeField] protected float timeFade = 1f;
    [SerializeField] protected float localYPosition = 30f;
    [SerializeField] protected Color color = new Color(1, 1, 1, 0);
    [SerializeField] protected Text TextPrefab;
    [SerializeField] protected GameObject TextsHolder;
    [SerializeField] protected GameObject PoolingHolders;

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
        //var go = Instantiate(TextPrefab, TextsHolder.transform);
        //go.gameObject.SetActive(true);
        var go = TextSpawner.Instance.Spawn(transform.position, transform.rotation);
        go.transform.localScale = Vector3.zero;
        Text text = go.GetComponent<Text>();
        ScaleUpText(text);
        return text;

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
            //Destroy(go.gameObject, 0.5f);
            DespawnByTrigger despawn = go.GetComponentInChildren<DespawnByTrigger>();
            if(despawn != null)
            {
                go.color = new Color(1, 1, 1, 1);
                despawn.SetCanDespawn();
            }
            else
            {
                Debug.LogError("Can't find Despawn component in " + go.gameObject.name);
            }
        });
        go.transform.LeanScale(Vector3.zero, this.timeFade).setEaseInBack();
    }
}
