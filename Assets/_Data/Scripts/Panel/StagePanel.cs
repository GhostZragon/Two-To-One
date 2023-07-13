using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class StagePanel : QuangLibrary, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Text")]
    [SerializeField] protected TextMeshProUGUI minMaxValue;
    [SerializeField] protected TextMeshProUGUI mathOperation;
    [SerializeField] protected TextMeshProUGUI bestScore;
    [Header("Transform And Component")]
    [SerializeField] protected Transform _transformHolder;
    [SerializeField] protected Image image;
    [SerializeField] protected bool MoveYPosFinished = false;
    [SerializeField] public int index;
    [SerializeField] protected StageManager stageManager;
    [SerializeField] protected LoadStageInMenuPanel loadStageInMenuPanel;
    public Color color = new Color(.6f, .6f, .6f, 1f);
    public float time = 0.3f;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadStageManager();
        this.LoadTransformHolder();
        this.LoadText(out minMaxValue, "MinMaxValue");
        this.LoadText(out mathOperation, "MathOperation");
        this.LoadText(out bestScore, "BestScore");
        this.LoadSpriteRenderer();
        this.LoadStageMenuInPanel();
    }
    protected virtual void LoadStageMenuInPanel()
    {
        if (this.loadStageInMenuPanel != null) return;
        this.loadStageInMenuPanel = GetComponentInParent<LoadStageInMenuPanel>();
    }
    protected virtual void LoadSpriteRenderer()
    {
        if (image != null) return;
        image = _transformHolder.Find("Background").GetComponent<Image>();
    }
    protected virtual void LoadText(out TextMeshProUGUI _text, string _name)
    {
        _text = _transformHolder.Find(_name).GetComponent<TextMeshProUGUI>();

    }

    protected virtual void LoadTransformHolder()
    {
        if (this._transformHolder != null) return;
        this._transformHolder = transform.Find("Panel");
    }
    protected virtual void LoadStageManager()
    {
        if (stageManager != null) return;
        stageManager = GetComponentInParent<StageManager>();
    }
    public void LoadStage(Stage _stage)
    {
        string str = $"Min: {_stage.minValue}" +
            $"\nMax: {_stage.maxValue}";
        string bestscore = _stage.GetBestScore().ToString();

        bestScore.text = bestscore;
        //minMaxValue.valueText = _stage.minValue + " - " + _stage.maxValue;
        minMaxValue.text = str;
        mathOperation.text = MathState.GetStringMathOperation(_stage.operation);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("My panel index: " + index);
        stageManager.SetCurrentStage(index);
        loadStageInMenuPanel.CheckStageIndex(index);
        AudioManager.PlaySound(AudioManager.AudioName.ClickUI, "play");
    }
    public void SetIndexPanel(int _index)
    {
        this.index = _index;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ColorChangeTrigger(color);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ColorChangeTrigger(Color.white);
    }

    private void ColorChangeTrigger(Color color)
    {
        LeanTween.color(image.transform.gameObject, color, time);
    }
    public void MoveUp()
    {
        _transformHolder.LeanMoveLocalY(15, time);
    }
    public void MoveDown()
    {
        _transformHolder.LeanMoveLocalY(0, time);
    }
}
