using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Cell : QuangLibrary
{

    public Infor infor;
    //public Text valueText;
    [SerializeField] protected TextMeshProUGUI valueText;
    [SerializeField] protected Button btn;
    bool isScale = false;
    public Image image;
    [HideInInspector] public float scaleSpeed = 0.1f;
    [HideInInspector] public float rorateUp = -720;
    [HideInInspector] public float rorateDown = 720;
    [HideInInspector] public float time = 0.3f;
    public int value = 0;
    public SpriteCellSO spriteCellSO;
    protected override void OnEnable()
    {
        base.OnEnable();
        CreateSprite();
    }
    private void Start()
    {
        this.UpdateValue(value);
        btn.onClick.AddListener(ClickDown);

    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadText();
        this.LoadButton();
        this.LoadImage();
        this.LoadSpriteCellSO();
    }
    protected virtual void LoadSpriteCellSO()
    {
        if (spriteCellSO != null) return;
        spriteCellSO = Resources.Load<SpriteCellSO>("SpriteCellSO/SpritesCell");
    }
    protected virtual void LoadImage()
    {
        if (this.image != null) return;
        image = GetComponent<Image>();
    }
    protected virtual void LoadText()
    {
        if (this.valueText != null) return;
        valueText = GetComponentInChildren<TextMeshProUGUI>();

    }
    protected virtual void LoadButton()
    {
        if (this.btn != null) return;
        btn = GetComponent<Button>();
    }
    private void CreateSprite()
    {
        image.sprite = spriteCellSO.GetRandomSprite();
    }

    public void UpdateValue(int newValue)
    {
        value = newValue;
        
        valueText.text = value.ToString();

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
    public int value;
}
