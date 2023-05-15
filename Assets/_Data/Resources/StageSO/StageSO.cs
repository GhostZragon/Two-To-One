using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State_")]
public class StageSO : ScriptableObject
{
    public List<Stage> stages;

    public List<Stage> GetStageList()
    {
        return stages;
    }

}
