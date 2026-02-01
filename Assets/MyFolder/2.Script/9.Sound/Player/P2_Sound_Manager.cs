using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_Sound_Manager : MonoBehaviour
{
    Gamemanager myChar;

    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip Exp_Sound;
    [SerializeField] AudioClip Transform_Sound;

    public void Sound_Play(AudioClip clip)
    {
        _audio.PlayOneShot(clip);
    }

    public void Sound_Play(string clipName)
    {
        switch (clipName)
        {
            case "EXP":
                _audio.PlayOneShot(Exp_Sound,0.5f);
                break;
            case "Transform":
                _audio.PlayOneShot(Transform_Sound,1);
                break;
            default:
                return;
        }
    }
}
