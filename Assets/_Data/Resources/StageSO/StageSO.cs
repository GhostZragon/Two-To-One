using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "State_",menuName = "ScriptableObjects/StageSO",order = 2)]
public class StageSO : ScriptableObject
{
    public List<Stage> stages;
    string savepath = Application.dataPath;
    public void Save()
    {
        Stage stage = null;
        for (int i = 0; i < stages.Count; i++)
        {
            stage = stages[i];
            PlayerPrefs.SetFloat(i.ToString(), stage.GetBestScore());
        }
    }
    public void Load()
    {
        Stage stage = null;
        for (int i = 0; i < stages.Count; i++)
        {
            stage = stages[i];
            int score = (int)PlayerPrefs.GetFloat(i.ToString(), 0);
            Debug.Log(score);
            stage.SetMaxScoreNoAgru(score);
        }
    }
    public List<Stage> GetStageList()
    {
        return stages;
    }

}
