using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Audio_", menuName = "ScriptableObjects/AudioSO", order = 3)]
public class AudioGameSO : ScriptableObject
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    [SerializeField]
    public AudioSettings audioSettings;

}
