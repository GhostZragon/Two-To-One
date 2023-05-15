using UnityEngine;
using UnityEngine.UI;

public class Cell : QuangLibrary
{

    public Infor infor;
    public Text valueText;
    public Button btn;
    bool isScale = false;
    public Image image;
    [HideInInspector] public float scaleSpeed = 0.1f;
    [HideInInspector] public float rorateUp = -720;
    [HideInInspector] public float rorateDown = 720;
    [HideInInspector] public float time = 0.3f;
    public SpriteCellSO spriteCellSO;
    protected override void OnEnable()
    {
        base.OnEnable();
        CreateSprite();
    }
    private void Start()
    {
        this.UpdateValue(infor.value);
        btn.onClick.AddListener(ClickDown);

    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadText();
        LoadButton();
    }
    protected virtual void LoadText()
    {
        if (this.valueText != null)
        {
            valueText = GetComponentInChildren<Text>();
        }
    }
    protected virtual void LoadButton()
    {
        if (this.btn != null)
        {
            btn = GetComponent<Button>();
        }
    }
    private void CreateSprite()
    {
        image.sprite = spriteCellSO.GetRandomSprite();
    }

    public void UpdateValue(int newValue)
    {
        infor.value = newValue;
        valueText.text = infor.value.ToString();
    }
    public void ClickDown()
    {
        if (!SelectionManager.Instance.CanBeClicked()) return;
        if (isScale == false)
        {
            ScaleUp();
            isScale = true;
        }
        //Debug.Log(transform.name+" Value: "+infor.value);
        SelectionManager.Instance.OnCellClick(transform);
        //MultiSelection.Instance.OnCellClick(transform);

    }
    public void TriggerScorePopUp()
    {
        // Active event score pop up
        ScoreManager.Instance.DisplayScorePopUp(transform.position);
        btn.interactable = false;
    }
    private void Rotating(float rorate)
    {
        Vector3 next = new Vector3(0, 0, rorate);
        LeanTween.rotateLocal(transform.gameObject, next, time).setEaseInOutBack();
    }
    public void RandomValue(int min, int max)
    {
        max += 1;
        int _newValue = Random.Range(min, max);
        while (_newValue == 0)
        {
            _newValue = Random.Range(min, max);
        }
        UpdateValue(_newValue);
    }

    private void ScaleUp()
    {
        //Debug.Log("On mouse enter");
        Vector3 vector = new Vector3(1.2f, 1.2f, 1.2f);
        transform.LeanScale(vector, scaleSpeed);
        Rotating(rorateUp);
    }

    public void ScaleDown()
    {
        transform.LeanScale(Vector3.one, scaleSpeed);
        isScale = false;
        Rotating(rorateDown);
    }



}
[System.Serializable]
public class Infor
{
    public int x;
    public int y;
    public int value;
    public int position;
}
