using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStage : MonoBehaviour
{
    public Stage stage;
    public List<Stage> stages;
    public StageSO stageSO;
    // Start is called before the first frame update
    void Start()
    {
        stages = stageSO.GetStageList();
        stage = stages[0];
    }
    void Test()
    {
        stages = new List<Stage>();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
