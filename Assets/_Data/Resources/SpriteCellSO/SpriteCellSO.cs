
using UnityEngine;

[CreateAssetMenu(fileName = "SpirtesCell", menuName = "ScriptableObjects/SpriteCellSO", order = 1)]
public class SpriteCellSO : ScriptableObject
{
    public Sprite[] sprite;

    public Sprite GetRandomSprite()
    {
        int index = Random.Range(0, sprite.Length);
        return sprite[index];
    }
}
