using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadStageInMenuPanel : QuangLibrary
{
    public List<StagePanel> stagePanelsShowOnScreen;
    public GameObject stagePanelPrefab;
    public GameObject holder;
    public StageManager stageManager;
    private GridLayoutGroup gridLayoutGroup;
    public static Action OnResetStageValue;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadStageManager();
        this.LoadPrefab();
        this.LoadHolder();
    }
    protected virtual void LoadHolder()
    {
        if (holder != null) return;
        holder = transform.Find("Holder").gameObject;
    }
    protected virtual void LoadPrefab()
    {
        Transform obj = transform.Find("Prefabs");
        if (stagePanelPrefab != null) return;
        stagePanelPrefab = obj.Find("Panel").gameObject;
    }
    protected virtual void LoadStageManager()
    {
        if (stageManager != null) return;
        stageManager = GetComponentInParent<StageManager>();
    }
    private void Start()
    {
        gridLayoutGroup = holder.GetComponent<GridLayoutGroup>();
        SpawnStagePanel();
        OnResetStageValue += ResetStageDisplay;
        CheckStageIndex();
    }
    private void SpawnStagePanel()
    {
        if(holder.transform.childCount > 0)
        {
            for(int i = 0; i < holder.transform.childCount; i++)
            {
                Destroy(holder.transform.GetChild(i).gameObject);
            }
        }
        for(int i = 0; i < stageManager.GetStageCount(); i++)
        {
            var go = Instantiate(stagePanelPrefab, holder.transform).GetComponent<StagePanel>();
            go.gameObject.SetActive(true);
            go.LoadStage(stageManager.stageSO.stages[i]);
            go.SetIndexPanel(i);
            stagePanelsShowOnScreen.Add(go);
        }
        //gridLayoutGroup.enabled = false;
    }
    public void ResetStageDisplay()
    {
        if (stagePanelsShowOnScreen.Count == 0) return;
        foreach(var item in stagePanelsShowOnScreen)
        {
            item.LoadStage(stageManager.stageSO.stages[item.index]);
        }
    }
    public void CheckStageIndex()
    {
        foreach(var item in stagePanelsShowOnScreen)
        {
            if(item.index == stageManager.ReturnCurrentIndex())
            {
                item.MoveUp();
            }
            else
            {
                item.MoveDown();
            }
        }
    }
    
}
