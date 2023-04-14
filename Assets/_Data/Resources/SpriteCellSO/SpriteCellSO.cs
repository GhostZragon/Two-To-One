using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpirtesCell")]
public class SpriteCellSO : ScriptableObject
{
    [System.Serializable]
    public struct CellSpirte
    {
        public string name;
        public Sprite sprite;
    }
    public CellSpirte[] sprite;
}
