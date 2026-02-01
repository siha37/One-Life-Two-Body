using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Option_System : MonoBehaviour
{
    Gamemanager myChar;

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider MasterVolume;
    [SerializeField] Slider BGMVolume;
    [SerializeField] Slider SFXVolume;
    float Master, BGM, SFX;


    public void OnEnable()
    {
        myChar = Gamemanager.myChar;
        MasterVolume.value = myChar.MASTER_Volume;
        BGMVolume.value = myChar.BGM_Volume;
        SFXVolume.value = myChar.SFX_Volume;
        Master = myChar.MASTER_Volume;
        BGM = myChar.BGM_Volume;
        SFX = myChar.SFX_Volume;
    }

    public void Master_Set()
    {
        float volume = (MasterVolume.value <= MasterVolume.minValue) ? -80f : MasterVolume.value;
        audioMixer.SetFloat("Master", volume);
        myChar.MASTER_Volume = volume; 
        myChar.Option_Save();
    }
    public void BGM_Set()
    {
        float volume = (BGMVolume.value <= BGMVolume.minValue) ? -80f : BGMVolume.value; 
        audioMixer.SetFloat("BGM", volume);
        myChar.BGM_Volume = volume; 
        myChar.Option_Save();
    }
    public void SFX_Set()
    {
        float volume = (SFXVolume.value <= SFXVolume.minValue) ? -80f : SFXVolume.value;
        audioMixer.SetFloat("SFX", volume);
        myChar.SFX_Volume = volume; 
        myChar.Option_Save();
    }
}
