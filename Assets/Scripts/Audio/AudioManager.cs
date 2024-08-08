using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class Sound
{
    public string name;
    public AK.Wwise.Event sound = null;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds;
    public Sound[] sfxSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("BackgroundMusic");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Music not found");
        }
        else
        {
            sound.sound.Post(this.gameObject);
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("SFX not found");
        }
        else
        {
            sound.sound.Post(this.gameObject);
        }
    }

    //Meun
    bool isActiveMusic = true;
    bool isActiveSFX = true;
    public void ToggleMusic(GameObject cross)
    {
        isActiveMusic = !isActiveMusic;
        AkSoundEngine.SetRTPCValue("MusicVolume", ToFloat(isActiveMusic));
        cross.SetActive(!isActiveMusic);
    }
    public void ToggleSFX(GameObject cross)
    {
        isActiveSFX = !isActiveSFX;
        AkSoundEngine.SetRTPCValue("SFXVolume", ToFloat(isActiveSFX));
        cross.SetActive(!isActiveSFX);
    }
    public void MusiceVolume(float volume)
    {
        if (!isActiveMusic) return;
        AkSoundEngine.SetRTPCValue("MusicVolume", volume);
    }
    public void SFXVolume(float volume)
    {
        if (!isActiveSFX) return;
        AkSoundEngine.SetRTPCValue("SFXVolume", volume);
    }
    private float ToFloat(bool value)
    {
        return value ? 1.0f : 0.0f;
    }
}
