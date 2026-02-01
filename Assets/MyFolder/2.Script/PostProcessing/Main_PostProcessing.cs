using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Main_PostProcessing : MonoBehaviour
{

    [SerializeField] private Volume volume;
    private LensDistortion lens;
    private Vignette vignette;
    Gamemanager myChar;

    private void Awake()
    {
        myChar = Gamemanager.myChar;
        myChar.MainVolume = this.GetComponent<Main_PostProcessing>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out lens);
        volume.profile.TryGet(out vignette);
    }

    public IEnumerator LensEffect(float Max)
    {
        float value =0;
        if (Max > 0)
        {
            while (Max > value)
            {
                yield return new WaitForEndOfFrame();
                value += (Max / 10);
                lens.intensity.value = value;
            }
            while (value > 0)
            {
                yield return new WaitForEndOfFrame();
                value -= (Max / 10);
                lens.intensity.value = value;
            } 
        }
        else
        {
            while (Max < value)
            {
                yield return new WaitForEndOfFrame();
                value += (Max / 10);
                lens.intensity.value = value;
            }
            while (value < 0)
            {
                yield return new WaitForEndOfFrame();
                value -= (Max / 10);
                lens.intensity.value = value;
            }
        }
    }

    public void Vignette(float persent)
    {
        if(persent ==0)
        {
            vignette.active = false;
        }
        else
        {
            vignette.active = true;
            vignette.intensity.value = persent;
        }
    }
    
}
