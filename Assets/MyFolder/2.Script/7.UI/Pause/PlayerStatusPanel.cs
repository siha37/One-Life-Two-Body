using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatusPanel : MonoBehaviour
{
    [Header("TEXT")]
    [SerializeField] private TextMeshProUGUI hp_text;
    [SerializeField] private TextMeshProUGUI Exp_text;
    [SerializeField] private TextMeshProUGUI Mana_text;
    [SerializeField] private TextMeshProUGUI Energy_text;
    [SerializeField] private TextMeshProUGUI Damage_text;
    [SerializeField] private TextMeshProUGUI Speed_text;
    [Space(5)]

    //PlayerStatus
    private TopGameData P_top;
    public void StatusSet()
    {
        if(P_top == null)
        {
            P_top = TopGameData.Dataset;
        }
        hp_text.text =  "Hp - "+P_top.HP.ToString();
        Exp_text.text = "Exp - "+P_top.EXP.ToString();
        Mana_text.text = "Mana - " + P_top.Mana.ToString();
        Energy_text.text = "Energy - " + P_top.Energy.ToString();
        Damage_text.text = "Damage - " + P_top.Damage_P.ToString();
        Speed_text.text = "Speed - " + P_top.B_Speed.ToString();
    }
}
