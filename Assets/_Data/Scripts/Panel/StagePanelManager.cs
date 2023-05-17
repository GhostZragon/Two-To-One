using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StagePanelManager : QuangLibrary
{
    public List<StagePanel> stagePanels;
    public GameObject stagePanelPrefab;
    public GameObject holder;
    public StageManager stageManager;
    public bool isTesting = true;
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
        if (!isTesting)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            stagePanelPrefab.gameObject.SetActive(false);

            Invoke(nameof(SpawnStagePanel), 0.5f);
        }
        
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
            var go = Instantiate(stagePanelPrefab, holder.transform);
            StagePanel stagePanel = go.GetComponent<StagePanel>();
            stagePanel.LoadStage(stageManager.GetStage(i));
            stagePanel.SetIndexPanel(i);
            go.SetActive(true);
        }
    }

}
