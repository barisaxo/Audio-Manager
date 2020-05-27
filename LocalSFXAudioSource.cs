using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSFXAudioSource : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioSource localSFX;
    public AudioClip[] localSFXClip;

    public float localSFXClipLength, timePosition;

    [Range(0f, 1f)]
    public float volume;

    void Start()
    {
        localSFX = gameObject.AddComponent<AudioSource>();
        localSFX.volume = volume;
        localSFXClip = audioManager.audioAssetSub.localSFXClip;
    }

    public void LoadLocalSFXClip(int index)
    {
        localSFX.clip = localSFXClip[index];
    }

    public void FindLocalSFXClipLength()
    {
        localSFXClipLength = localSFX.clip.length;
    }

    public void PlayLocalSFX()
    {
        localSFX.Play();
    }

    public void PlayLocalSFXFromTime(float fromSeconds)
    {
        localSFX.time = fromSeconds;
        localSFX.Play();
    }

    public void StopLocalSFX()
    {
        localSFX.Stop();
    }

    public void PauseLocalSFX()
    {
        localSFX.Pause();
    }

    public void UnPauseLocalSFX()
    {
        localSFX.UnPause();
    }

    public void SetCurrentLocalSFXTimePosition()
    {//to set/store a playback position
        timePosition = localSFX.time;
    }

    public void PlayAmbienceFromUntilTime(float fromSeconds, float untilSeconds)
    {
        localSFX.time = fromSeconds;
        localSFX.Play();
        StartCoroutine(StopAtTime(untilSeconds));
    }

    public void PlayLocalSFXUntilTime(float atSeconds)
    {
        StartCoroutine(StopAtTime(atSeconds));
    }

    IEnumerator StopAtTime(float seconds)
    {
        while (localSFX.time < seconds)
        {
            yield return new WaitForSeconds(seconds - localSFX.time);
        }
        localSFX.Stop();
        yield return null;
    }

    //private void Update()
    //{
    //    Debug.Log(localSFX.time);
    //}

    public void FadeOutLocalSFX(float fadeRate, float fadeDuration)
    {//do not use fadeout while loop == true;
        FindLocalSFXClipLength();
        StartCoroutine(FadingLocalSFXOut(fadeRate, fadeDuration));
    }

    IEnumerator FadingLocalSFXOut(float fadeRate, float fadeDuration)
    {
        while (localSFX.time < localSFXClipLength)
        {
            if (localSFX.time >= localSFXClipLength - fadeDuration)
            {
                localSFX.volume -= localSFX.volume * Time.deltaTime * fadeRate;
            }
            yield return new WaitForEndOfFrame();
        }
        localSFX.volume = volume;
        yield return null;
    }

    public void FadeInLocalSFX(float fadeRate)
    {
        localSFX.volume = .0f;
        StartCoroutine(FadingLocalSFXIn(fadeRate));
    }

    IEnumerator FadingLocalSFXIn(float fadeRate)
    {
        while (localSFX.volume < volume)
        {
            localSFX.volume += Time.deltaTime * fadeRate;
            yield return new WaitForEndOfFrame();
        }
        localSFX.volume = volume;
        yield return null;
    }
}
