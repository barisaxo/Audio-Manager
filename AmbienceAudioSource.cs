using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceAudioSource : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioSource ambience;
    public AudioClip[] ambienceClip;

    public float ambienceClipLength, timePosition;

    [Range(0f, 1f)]
    public float volume = .8f;

    void Start()
    {
        ambience = gameObject.AddComponent<AudioSource>();
        ambience.volume = volume;
        ambienceClip = audioManager.audioAssetSub.ambienceClip;
        //ambience.loop = true;
    }

    public void LoadAmbienceClip(int index)
    {
        ambience.clip = ambienceClip[index];
    }

    public void FindAmbienceClipLength()
    {
        ambienceClipLength = ambience.clip.length;
    }

    public void PlayAmbience()
    {
        ambience.Play();
    }

    public void PlayAmbienceFromTime(float fromSeconds)
    {
        ambience.time = fromSeconds;
        ambience.Play();
    }

    public void StopAmbience()
    {
        ambience.Stop();
    }

    public void PauseAmbience()
    {
        ambience.Pause();
    }

    public void UnPauseAmbience()
    {
        ambience.UnPause();
    }

    public void SetCurrentAmbienceTimePosition()
    {//to set/store a playback position
        timePosition = ambience.time;
    }

    public void PlayAmbienceFromUntilTime(float fromSeconds, float untilSeconds)
    {
        ambience.time = fromSeconds;
        ambience.Play();
        StartCoroutine(StopAtTime(untilSeconds));
    }

    public void PlayAmbienceUntilTime(float untilSeconds)
    {
        StartCoroutine(StopAtTime(untilSeconds));
    }

    IEnumerator StopAtTime(float seconds)
    {
        while (ambience.time < seconds)
        {
            yield return new WaitForSeconds(seconds - ambience.time);
        }
        ambience.Stop();
        yield return null;
    }

    public void FadeOutAmbience(float fade)
    {//do not use with looped ambience clips
        FindAmbienceClipLength();
        StartCoroutine(FadingAmbienceOut(fade));
    }

    IEnumerator FadingAmbienceOut(float fade)
    {
        while (ambience.time < ambienceClipLength)
        {
            if (ambience.time >= ambienceClipLength - 1.5f)
            {
                ambience.volume -= ambience.volume * Time.deltaTime * fade;
            }
            yield return new WaitForEndOfFrame();
        }
        ambience.volume = volume;
        yield return null;
    }

    public void FadeInAmbience(float fade)
    {
        ambience.volume = .0f;
        StartCoroutine(FadingAmbienceIn(fade));
    }

    IEnumerator FadingAmbienceIn(float fade)
    {
        while (ambience.volume < volume)
        {
            ambience.volume += Time.deltaTime * fade;
            yield return new WaitForEndOfFrame();
        }
        ambience.volume = volume;
        yield return null;
    }
}
