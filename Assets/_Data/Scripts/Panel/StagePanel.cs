using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class StagePanel : QuangLibrary, IPointerClickHandler
{
    [SerializeField] protected TextMeshProUGUI minMaxValue;
    [SerializeField] protected TextMeshProUGUI stageOperation;
    [SerializeField] protected TextMeshProUGUI bestScore;
    [SerializeField] protected Stage stage;
    [SerializeField] public int index;
    [SerializeField] protected StageManager stageManager;
    void Start()
    {

    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadStageManager();
    }
    protected virtual void LoadStageManager()
    {
        if (stageManager != null) return;
        stageManager = GetComponentInParent<StageManager>();
    }
    public void LoadStage(Stage _stage)
    {
        this.stage = _stage;
        string str = $"Min: {_stage.minValue}" +
            $"\nMax: {_stage.maxValue}";
        string bestscore = _stage.GetBestScore().ToString();

        bestScore.text = bestscore;
        //minMaxValue.valueText = _stage.minValue + " - " + _stage.maxValue;
        minMaxValue.text = str;
        stageOperation.text = MathState.GetStringMathOperation(_stage.operation);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("My panel index: " + index);
        stageManager.SetCurrentStage(index);
    }
    public void SetIndexPanel(int _index)
    {
        this.index = _index;
    }
}
