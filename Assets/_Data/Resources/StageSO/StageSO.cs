using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State_",menuName = "ScriptableObjects/StageSO",order = 2)]
public class StageSO : ScriptableObject
{
    public List<Stage> stages;

    public List<Stage> GetStageList()
    {
        return stages;
    }

}
