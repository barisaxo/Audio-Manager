using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAssetSub : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioClip[] musicClip = new AudioClip[1];
    public AudioClip[] ambienceClip = new AudioClip[1];
    public AudioClip[] localSFXClip = new AudioClip[1];
    public AudioClip[] worldSFXClip = new AudioClip[1];

    void Start()
    {
        //load audio from the Resources folder here
        //musicClip[0] = Resources.Load<AudioClip>("AUDIO/Music/*file name*");
        //worldSFXClip[0] = Resources.Load<AudioClip>("AUDIO/WorldSFX/*file name*");
        //ambienceClip[0] = Resources.Load<AudioClip>("AUDIO/Ambience/*file name");
        //localSFXClip[0] = Resources.Load<AudioClip>("AUDIO/LocalSFX/*file name*");
    }
}
