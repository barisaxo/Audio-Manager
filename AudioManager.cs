using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioManager audioManager;//this
    public AudioAssetSub audioAssetSub;//call audioclips from here
    public VolumeManager volumeManager;//loads on start/ saves on adjust /any and all audio sources

    //audioSources:
    public MusicAudioSource musicAS;//for music
    public AmbienceAudioSource ambienceAS;//for ambience
    public LocalSFXAudioSource localSFXAS;//for local SFX such as gui

    //list of WorldSFXAudioSources(Noisy Game Objects)
    public List<AudioSource> noisyGO;//for GOs that need to make their own sounds

    void Start()
    {
        audioManager = GetComponent<AudioManager>();

        audioAssetSub = gameObject.AddComponent<AudioAssetSub>();
        audioAssetSub.audioManager = audioManager;

        volumeManager = gameObject.AddComponent<VolumeManager>();
        volumeManager.audioManager = audioManager;

        musicAS = new GameObject("Music").AddComponent<MusicAudioSource>();
        musicAS.transform.parent = gameObject.transform;
        musicAS.audioManager = audioManager;

        ambienceAS = new GameObject("Ambience").AddComponent<AmbienceAudioSource>();
        ambienceAS.transform.parent = gameObject.transform;
        ambienceAS.audioManager = audioManager;

        localSFXAS = new GameObject("Local SFX").AddComponent<LocalSFXAudioSource>();
        localSFXAS.transform.parent = gameObject.transform;
        localSFXAS.audioManager = audioManager;
    }

    //pass through GameObject to give worldSFX audioSource;
    public void AddAudioToNoisyGO(GameObject gO)
    {
        WorldSFXAudioSource worldSfxAS = new GameObject("World SFX").AddComponent<WorldSFXAudioSource>();
        worldSfxAS.transform.parent = gO.gameObject.transform;
        worldSfxAS.audioManager = audioManager;
    }
}
/*
 *      musicAS,ambienceAS, and localSFXAS have their own GameObjects
 *      in order to help keep the unity inspector clean, but it is not necessary.
 *      
 */