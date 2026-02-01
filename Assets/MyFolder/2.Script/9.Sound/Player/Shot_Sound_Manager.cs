using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Sound_Manager : MonoBehaviour
{
    public AudioSource[] audios = new AudioSource[3];
    public AudioSource Loop_audio;

    public void AudioPlay(AudioClip clip)
    {
        for (int i = 0; i < audios.Length; i++)
        {
            if (!audios[i].isPlaying)
            {
                audios[i].PlayOneShot(clip);
                break;
            }
        }
    }

    public void AudioPlay_One(AudioClip clip)
    {
        audios[0].PlayOneShot(clip);
    }
    public void  OneLoop_AudioPlay(AudioClip clip)
    {
        Loop_audio.clip = clip;
        Loop_audio.Play();
    }
    public void OneLoop_AudioStop()
    {
        Loop_audio.Stop();
    }

}
