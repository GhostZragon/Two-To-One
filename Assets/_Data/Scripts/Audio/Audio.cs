using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Audio 
{
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
    public bool isLoop = false;
    [Range(0,1f)]public float volume = 1;
    [Range(.1f,3f)]public float pitch = 1;
}
