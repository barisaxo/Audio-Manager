using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioSource : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioSource music;
    public AudioClip[] musicClip;

    public float musicClipLength, timePosition;

    [Range(0f, 1f)]
    public float volume;

    void Start()
    {
        music = gameObject.AddComponent<AudioSource>();
        music.volume = volume;
        musicClip = audioManager.audioAssetSub.musicClip;
        music.loop = true;

        TestMusic();
    }

    void TestMusic()
    {

        LoadMusicClip(0);
        PlayMusicFromTime(25f);
        FadeOutMusic(.5f, 3f);
    }

    public void LoadMusicClip(int index)
    {
        music.clip = musicClip[index];
    }

    public void FindMusicClipLength()
    {
        musicClipLength = music.clip.length;
    }

    public void PlayMusic()
    {
        music.Play();
    }

    public void PlayMusicFromTime(float fromSeconds)
    {
        music.time = fromSeconds;
        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public void PauseMusic()
    {
        music.Pause();
    }

    public void UnPauseMusic()
    {
        music.UnPause();
    }

    public void SetCurrentMusicTimePosition()
    {//to set/store a playback position
        timePosition = music.time;
    }

    public void PlayAmbienceFromUntilTime(float fromSeconds, float untilSeconds)
    {
        music.time = fromSeconds;
        music.Play();
        StartCoroutine(StopAtTime(untilSeconds));
    }

    public void PlayMusicUntilTime(float atSeconds)
    {
        StartCoroutine(StopAtTime(atSeconds));
    }

    IEnumerator StopAtTime(float seconds)
    {
        while (music.time < seconds)
        {
          yield return new WaitForSeconds(seconds - music.time);
        }
        music.Stop();
        yield return null;
    }

    //private void Update()
    //{
    //    Debug.Log(music.time);
    //}

    public void FadeOutMusic(float fadeRate,float fadeDuration)
    {//do not use fadeout while loop == true;
        FindMusicClipLength();
        StartCoroutine(FadingMusicOut(fadeRate, fadeDuration));
    }

    IEnumerator FadingMusicOut(float fadeRate, float fadeDuration)
    {
        while (music.time < musicClipLength)
        {
            if (music.time >= musicClipLength - fadeDuration)
            {
                music.volume -= music.volume * Time.deltaTime * fadeRate;
            }
            yield return new WaitForSeconds(musicClipLength-fadeDuration - music.time);
        }
        music.volume = volume;
        yield return null;
    }

    public void FadeInMusic(float fade)
    {
        music.volume = .0f;
        StartCoroutine(FadingMusicIn(fade));
    }

    IEnumerator FadingMusicIn(float fade)
    {
        while (music.volume < volume)
        {
            music.volume += Time.deltaTime * fade;
            yield return new WaitForEndOfFrame();
        }
        music.volume = volume;
        yield return null;
    }
}
