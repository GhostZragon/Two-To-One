using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Audio_", menuName = "ScriptableObjects/AudioSO", order = 3)]
public class AudioGameSO : ScriptableObject
{
    public List<Audio> list;
    [Header("Add Audio here")]
    public AudioClip clip;
    private void Reset()
    {
        object[] objs = Resources.LoadAll<Object>("Audio/Source");
        int count = objs.Length;
        foreach (Object obj in objs)
        {
            Add((AudioClip)obj);
        }
    }

    public void Add(AudioClip clip)
    {
        if (clip == null) return;
        Audio audio = new Audio();
        audio.name = clip.name;
        audio.clip = clip;
        foreach(var item in this.list)
        {
            if(item.name == audio.name)
            {
                Debug.Log("Audio name is exist");
                return;
            }
        }
        list.Add(audio);
    }
}
