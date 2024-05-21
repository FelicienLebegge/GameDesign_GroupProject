using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [HideInInspector] //will get asigned through code
    public AudioSource source;

    public bool loop;

    [HideInInspector] //will also get asigned through code
    public bool IsPlaying;
}
