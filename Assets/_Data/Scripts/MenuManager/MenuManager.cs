using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//[CustomEditor(typeof(MenuManager))]
//public class MenuManagerCustom : Editor
//{
//    private MenuManager menuManager;
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        MenuManager menuManager = (MenuManager)target;
//        if (GUILayout.Button("Pop Out"))
//        {
//            menuManager.PopUp();
//        }
//        if (GUILayout.Button("Pop In"))
//        {
//            menuManager.PopIn();
//        }
//    }
//}
public class MenuManager : QuangLibrary
{
    [SerializeField] protected GameObject DisplayHolder;
    [SerializeField] protected GameObject MenuPanel;
    [SerializeField] protected GameObject EndStageMenu;
    public float time = 0.3f;
    public List<CanvasTranstionActive> PanelList;
    public static MenuManager Instance;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        this.LoadPanelList();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadDisplayHolder();
        this.LoadMenuPanel();
        this.LoadEndStagePanel();
        this.LoadPanelList();
    }
    protected virtual void LoadEndStagePanel()
    {
        if(this.EndStageMenu != null ) return;
        this.EndStageMenu = GameObject.Find("EndStagePanel");
    }
    protected virtual void LoadPanelList()
    {
        PanelList = new List<CanvasTranstionActive>
        {
            DisplayHolder.GetComponent<CanvasTranstionActive>(),
            MenuPanel.GetComponent<CanvasTranstionActive>(),
            EndStageMenu.GetComponent<CanvasTranstionActive>()
        };
    }
    protected virtual void LoadDisplayHolder()
    {
        if (DisplayHolder != null) return;
        DisplayHolder = GameObject.Find("_DisplayHolder");
    }
    protected virtual void LoadMenuPanel()
    {
        if (MenuPanel != null) return;
        MenuPanel = GameObject.Find("MenuPanel");
    }
    void ShowObject(GameObject _obj)
    {
        foreach (var panel in PanelList)
        {
            if (panel.inScreen)
            {
                StartCoroutine(panel.PopIn(0f));
            }
        }
        foreach(var panel in PanelList)
        {
            if (panel.gameObject == _obj)
            {
                StartCoroutine(panel.PopOut(0.6f));
            }
        }
    }
    public void ShowEndStageMenu()
    {
        ShowObject(EndStageMenu);
    }
    public void ShowChooseStageMenu()
    {
        ShowObject(MenuPanel);
    }
    public void ShowPlayGameMenu()
    {
        ShowObject(DisplayHolder);
    }


    //public void ChangeGameObjectActive(object canvas, bool state)
    //{
    //    (canvas as GameObject)?.SetActive(state);
    //}
    //public void PopUp()
    //{
    //    float a = 0.56f;
    //    Vector3 vector = new Vector3(a, a, a);
    //    MenuPanel.transform.localScale = Vector3.zero;
    //    MenuPanel.LeanScale(vector, time).setEaseOutBack();
    //}
    //public void PopIn()
    //{
    //    MenuPanel.LeanScale(Vector3.zero, time).setEaseInBack();
    //}
}
