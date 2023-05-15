
using UnityEngine;

[CreateAssetMenu(fileName = "SpirtesCell")]
public class SpriteCellSO : ScriptableObject
{
    public Sprite[] sprite;

    public Sprite GetRandomSprite()
    {
        int index = Random.Range(0, sprite.Length);
        return sprite[index];
    }
}
