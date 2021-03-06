﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSFXAudioSource : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioSource worldSFX;
    public AudioClip[] worldSFXClip;

    public float worldSFXClipLength, timePosition;

    [Range(0f, 1f)]
    public float volume;

    void Start()
    {
        worldSFX = gameObject.AddComponent<AudioSource>();
        worldSFXClip = audioManager.audioAssetSub.worldSFXClip;
        volume = audioManager.volumeManager.worldSFXVolume;
        worldSFX.volume = volume;
        //add GO audiosource to list for future mass edditing such as volume
        audioManager.noisyGO.Add(gameObject.GetComponent<AudioSource>());

        //set 3D sound environment
        worldSFX.spatialBlend = 1f;
    }

    public void LoadWorldSFXClip(int index)
    {
        worldSFX.clip = worldSFXClip[index];
    }

    public void FindWorldSFXClipLength()
    {
        worldSFXClipLength = worldSFX.clip.length;
    }

    public void PlayWorldSFX()
    {
        worldSFX.Play();
    }

    public void PlayWorldSFXFromTime(float fromSeconds)
    {
        worldSFX.time = fromSeconds;
        worldSFX.Play();
    }

    public void StopWorldSFX()
    {
        worldSFX.Stop();
    }

    public void PauseWorldSFX()
    {
        worldSFX.Pause();
    }

    public void UnPauseWorldSFX()
    {
        worldSFX.UnPause();
    }

    public void SetCurrentWorldSFXTimePosition()
    {//to set/store a playback position
        timePosition = worldSFX.time;
    }

    public void PlayAmbienceFromUntilTime(float fromSeconds, float untilSeconds)
    {
        worldSFX.time = fromSeconds;
        worldSFX.Play();
        StartCoroutine(StopAtTime(untilSeconds));
    }

    public void PlayWorldSFXUntilTime(float atSeconds)
    {
        StartCoroutine(StopAtTime(atSeconds));
    }

    IEnumerator StopAtTime(float seconds)
    {
        while (worldSFX.time < seconds)
        {
            yield return new WaitForSeconds(seconds - worldSFX.time);
        }
        worldSFX.Stop();
        yield return null;
    }

    //private void Update()
    //{
    //    Debug.Log(worldSFX.time);
    //}

    public void FadeOutWorldSFX(float fadeRate, float fadeDuration)
    {//do not use fadeout while loop == true;
        FindWorldSFXClipLength();
        StartCoroutine(FadingWorldSFXOut(fadeRate, fadeDuration));
    }

    IEnumerator FadingWorldSFXOut(float fadeRate, float fadeDuration)
    {
        while (worldSFX.time < worldSFXClipLength)
        {
            if (worldSFX.time >= worldSFXClipLength - fadeDuration)
            {
                worldSFX.volume -= worldSFX.volume * Time.deltaTime * fadeRate;
            }
            yield return new WaitForEndOfFrame();
        }
        worldSFX.volume = volume;
        yield return null;
    }

    public void FadeInWorldSFX(float fadeRate)
    {
        worldSFX.volume = .0f;
        StartCoroutine(FadingWorldSFXIn(fadeRate));
    }

    IEnumerator FadingWorldSFXIn(float fadeRate)
    {
        while (worldSFX.volume < volume)
        {
            worldSFX.volume += Time.deltaTime * fadeRate;
            yield return new WaitForEndOfFrame();
        }
        worldSFX.volume = volume;
        yield return null;
    }
}