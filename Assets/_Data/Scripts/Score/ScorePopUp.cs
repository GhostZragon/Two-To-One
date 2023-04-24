using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ScorePopUp : QuangLibrary
{
    public float timeScale = 1f;
    public float timeMove = 1f;
    public float timeFade = 1f;
    public float localMoveY = 30f;
    public Color color = new Color(1, 1, 1, 0);
    public Text prefab;
    public GameObject SpawnLocation;

    protected override void Awake()
    {
        base.Awake();
        
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        LoadPrefab();
        LoadSpawnLocation();
    }
    protected virtual void LoadSpawnLocation()
    {
        if (this.SpawnLocation != null) return;
        this.SpawnLocation = transform.Find("SpawnLocation").gameObject;
    }
    protected virtual void LoadPrefab()
    {
        if (this.prefab != null) return;
        prefab = transform.GetComponentInChildren<Text>();
    }

    public Text PopUp()
    {
        var go = Instantiate(prefab, SpawnLocation.transform);
        go.transform.localScale = Vector3.zero;
        go.gameObject.SetActive(true);
        MoveUpFade(go);
        return go;
        
    }
    public void MoveUpFade(Text go)
    {
        float a = 1.3f;
        Vector3 to = new Vector3(a, a, a);

        go.transform.LeanScale(to, this.timeScale).setEaseInOutBounce().setOnComplete(() =>
        {
            //Debug.Log("Text is scale");
            float dis = go.rectTransform.localPosition.y;
            dis += localMoveY;
            go.transform.LeanMoveLocalY(dis, this.timeMove).setOnComplete(() =>
            {
                LeanTween.colorText(go.rectTransform, this.color, timeFade).setOnComplete(() =>
                {
                    Destroy(go.gameObject, 0.5f);
                });
                go.transform.LeanScale(Vector3.zero, this.timeFade).setEaseInBack();
            });
        });
    }

}
