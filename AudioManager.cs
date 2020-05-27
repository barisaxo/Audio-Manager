using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioListener audioListener;
    public AudioAssetSub audioAssetSub;
    public VolumeManager volumeManager;

    //audioSources:
    public MusicAudioSource musicAS;
    public AmbienceAS ambienceAS;
    public LocalSfxAS localSFXAS;

    //list of WorldSFXAudioSources(Noisy Game Objects)
    public List<AudioSource> noisyGO;

    void Start()
    {
        audioManager = GetComponent<AudioManager>();

        audioListener = Camera.main.gameObject.GetComponent<AudioListener>();

        audioAssetSub = gameObject.AddComponent<AudioAssetSub>();
        audioAssetSub.audioManager = audioManager;

        volumeManager = gameObject.AddComponent<VolumeManager>();
        volumeManager.audioManager = audioManager;

        musicAS = new GameObject("Music").AddComponent<MusicAudioSource>();
        musicAS.transform.parent = gameObject.transform;
        musicAS.audioManager = audioManager;

        ambienceAS = new GameObject("Ambience").AddComponent<AmbienceAS>();
        ambienceAS.transform.parent = gameObject.transform;
        ambienceAS.audioManager = audioManager;

        localSFXAS = new GameObject("Local SFX").AddComponent<LocalSfxAS>();
        localSFXAS.transform.parent = gameObject.transform;
        localSFXAS.audioManager = audioManager;
    }

    //pass through GameObject to give worldSFX audioSource;
    public void AddAudioToNoisyGO(GameObject gO)
    {
        WorldSfxAS worldSfxAS = new GameObject("World SFX").AddComponent<WorldSfxAS>();
        worldSfxAS.transform.parent = gO.gameObject.transform;
        worldSfxAS.audioManager = audioManager;
    }
}