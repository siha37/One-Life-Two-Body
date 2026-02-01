using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    Gamemanager myChar;
    [SerializeField] Toggle DamageUI_Toggle;

    private void Start()
    {
        myChar = Gamemanager.myChar;
        DamageUI_Toggle.isOn = myChar.ABLE_DamageUI;
    }
    public void DamageUI_Toggle_ONOFF()
    {
        myChar.ABLE_DamageUI = DamageUI_Toggle.isOn;
    }
}
