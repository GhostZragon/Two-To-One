using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(AudioManager))]
//public class AudioManagerCustom : Editor
//{
//    private AudioManager audioManager;
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        audioManager = (AudioManager)target;
//        if (GUILayout.Button("Test Aduio"))
//        {
//            audioManager.PlaySound();
//        }
//    }
//}
public class AudioManager : QuangLibrary
{
    [SerializeField] protected AudioGameSO audioSO;
    [SerializeField] protected int index;
    [SerializeField] protected GameObject audioPrefab;
    [SerializeField] protected GameObject holder;
    [SerializeField] protected string audioName;
    [SerializeField] protected List<String> audioNames;
    public static Action OnClicKCell;
    public static Action OnClickUI;
    public static Action<bool> OnMenuMusic;
    public static Action<bool> OnPlayGameMusic;
    public static Action OnWinGameMusic;
    public static Action OnCorrectAnswer;
    public static Action OnFalseAnswer;
    public static Action OnTimerSound;
    protected override void Awake()
    {
        base.Awake();
        this.LoadSounds();
        OnClicKCell += ClickCell;
        OnClickUI += ClickUI;
        OnMenuMusic += MenuMusic;
        OnPlayGameMusic += PlayGameMusic;
        OnWinGameMusic += WinGameMusic;
        OnCorrectAnswer += CorrectAnswer;
        OnFalseAnswer += FalseAnswer;
        OnTimerSound += TimerSoundSFX;
    }
    /*
     * ClickCell
     * ClickUI
     * MenuMusic
     * PlayGameMusic
     * WinGameMusic
     * TrueAnswer
     * FalseAnswer
     */
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadAudioSO();
        this.LoadAudioPrefab();
        this.LoadHolder();
    }

    protected virtual void LoadAudioSO()
    {
        if (this.audioSO != null) return; 
        audioSO = Resources.Load<AudioGameSO>("Audio/Audio_1");
    }
    protected virtual void LoadAudioPrefab()
    {
        if (this.audioPrefab != null) return;
        Transform obj = transform.Find("Prefabs");
        if (obj == null) return;
        audioPrefab = obj.Find("Sound").gameObject;
    }
    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        Transform obj = transform.Find("Holder");
        if (obj == null) return;
        holder = obj.gameObject;
    }

    protected virtual void LoadSounds()
    {
        foreach (Audio obj in audioSO.list)
        {
            var go = Instantiate(audioPrefab, holder.transform);
            go.name = obj.name;
            audioNames.Add(go.name);
            obj.source = go.GetComponent<AudioSource>();
            obj.source.clip = obj.clip;
            obj.source.volume = obj.volume;
            obj.source.pitch = obj.pitch;
            obj.source.loop = obj.isLoop;
        }
    }
    private void TimerSoundSFX()
    {
        PlaySound("ClockSFX", "play");
    }
    public void PlaySound()
    {
        audioName = audioNames[index];
        PlaySound(audioName,"play");
    }
    private void ClickCell()
    {
        PlaySound("ClickCell", "play");
    }
    private void ClickUI()
    {
        PlaySound("ClickUI", "play");
    }
    private void MenuMusic(bool boolen)
    {
        string s = boolen ? "play" : "stop";
        PlaySound("MenuMusic", s);
    }

    private void PlayGameMusic(bool boolen)
    {
        string s = boolen ? "play" : "stop";
        PlaySound("PlayGameMusic", s);
    }
    private void WinGameMusic()
    {
        PlaySound("WinGame","play");
    }
    private void CorrectAnswer()
    {
        PlaySound("Correct", "play");
    }
    private void FalseAnswer()
    {
        PlaySound("False", "play");
    }
    /// <summary>
    /// Play or Stop sound from audio manager.
    /// </summary>
    /// <param name="_name">Find sound with name parameter</param>
    /// <param name="playState">Play or Stop the sound</param>
    protected virtual void PlaySound(string _name, string playState)
    {
        Audio audio = audioSO.list.Find(x => x.name == _name);
        if (audio == null)
        {
            Debug.Log("Audio name is not exist");
            return;
        }
        string s = playState.ToUpper();
        if(s == "PLAY")
        {
            audio.source.Play();
        }
        else if(s == "STOP")
        {
            audio.source.Stop();
        }
        else if(s == "PAUSE")
        {
            audio.source.Pause();
        }
    }
}
