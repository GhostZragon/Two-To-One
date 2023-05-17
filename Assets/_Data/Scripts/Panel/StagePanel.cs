using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StagePanel : QuangLibrary, IPointerClickHandler
{
    [SerializeField]protected TextMeshProUGUI minMaxValue;
    [SerializeField]protected TextMeshProUGUI stageOperation;
    [SerializeField] protected TextMeshProUGUI bestScore;
    [SerializeField]protected Stage stage;
    [SerializeField] protected int index;
    [SerializeField] protected StageManager stageManager;

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
    public void LoadStage(Stage stage)
    {
        this.stage = stage;
        string str = $"Min: {stage.minValue}" +
            $"\nMax: {stage.maxValue}";
        if(stage.bestScore >= stage.MaxScore())
        {
            bestScore.text = "Maximum";
        }
        else
        {
            bestScore.text = stage.bestScore.ToString();
        }
        //minMaxValue.text = stage.minValue + " - " + stage.maxValue;
        minMaxValue.text = str;
        stageOperation.text = MathState.GetStringMathOperation(stage.operation);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("My panel index: "+index);
        stageManager.SetCurrentStage(index);
    }
    public void SetIndexPanel(int _index)
    {
        this.index = _index;
    }
}
