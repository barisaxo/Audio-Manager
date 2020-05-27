using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public AudioManager audioManager;

    [Range(0f, 1f)]
    public float worldSFXVolume;

    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Master Volume", .8f);
        audioManager.musicAS.volume = PlayerPrefs.GetFloat("Music Volume", .8f);
        audioManager.ambienceAS.volume = PlayerPrefs.GetFloat("Ambience Volume", .8f);
        audioManager.localSFXAS.volume = PlayerPrefs.GetFloat("LocalSFX Volume", .8f);
        worldSFXVolume = PlayerPrefs.GetFloat("WorldSFX Volume", .8f);
    }

    public void SetMasterVolume(float newVol)
    {
        AudioListener.volume = newVol;
        PlayerPrefs.SetFloat("Master Volume", newVol);
    }

    public void SetWorldSFXVolume(float newVol)
    {
        foreach (AudioSource aS in audioManager.noisyGO)
        { aS.volume = newVol; }
        PlayerPrefs.SetFloat("WorldSFX Volume", newVol);
        worldSFXVolume = newVol;
    }

    public void SetMusicVolume(float newVol)
    {
        audioManager.musicAS.volume = newVol;
        PlayerPrefs.SetFloat("Music Volume", newVol);
    }

    public void SetLocalSFXVolume(float newVol)
    {
        audioManager.localSFXAS.volume = newVol;
        PlayerPrefs.SetFloat("Music Volume", newVol);
    }

    public void SetAmbienceVolume(float newVol)
    {
        audioManager.ambienceAS.volume = newVol;
        PlayerPrefs.SetFloat("Ambience Volume", newVol);
    }
}