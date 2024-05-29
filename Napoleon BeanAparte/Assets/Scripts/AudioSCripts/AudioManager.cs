using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] Sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound sound in Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
            sound.source.volume = MenuBehaviour.SoundValue;
        }
    }

    public void Play(string name)
    {
        Sound foundSound = null;

        foreach (Sound sound in Sounds)
        {
            if (sound.name == name)
            {
                foundSound = sound;
                break;
            }
        }

        if (foundSound != null)
        {

            foundSound.source.Play();

        }
        else //for debugging ;)
        {
            Console.WriteLine($"Sound with name '{name}' not found.");
        }
    }

    public void Stop(string name)
    {
        Sound foundSound = null;

        foreach (Sound sound in Sounds)
        {
            if (sound.name == name)
            {
                foundSound = sound;
                break;
            }
        }

        if (foundSound != null)
        {

            foundSound.source.Stop();

        }
        else //for debugging ;)
        {
            Console.WriteLine($"Sound with name '{name}' not found.");
        }
    }
}
