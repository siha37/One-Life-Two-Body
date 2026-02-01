using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewordManager : MonoBehaviour
{
    TopGameData TOP;
    Gamemanager myChar;

    [SerializeField] MiniGame_Manager miniGame_Manager;
    [SerializeField] Reword_Type_Rorring type;
    [SerializeField] Reword_Amount amount_sys;
    [SerializeField] Button OK_Button;
    public enum RewordType { HP_UP, ENRGY, MANA, SPEED, HEAL }
    [ReadOnly] [SerializeField] RewordType Typeenum;
    [ReadOnly] [SerializeField] float Amount;


    private void OnEnable()
    {
        TOP = TopGameData.Dataset;
        myChar = Gamemanager.myChar;
        OK_Button.interactable = false;
    }
    public void Rorring_Start()
    {
        type.Rorring_Start();
    }

    public void Type_Get(RewordType _type)
    {
        Typeenum = _type;
        amount_sys.Rorring_Start(Typeenum);
    }

    public void Amount_get(float _amount)
    {
        Amount = _amount;
        OK_Button.interactable = true;
    }

    public void Reword_paymenet()
    {
        switch (Typeenum)
        {
            case RewordType.HP_UP:
                TOP.HP += Amount;
                break;
            case RewordType.ENRGY:
                TOP.Energy += Amount;
                break;
            case RewordType.MANA:
                TOP.Mana += Amount;
                break;
            case RewordType.SPEED:
                TOP.B_Speed = Amount;
                break;
            case RewordType.HEAL:
                myChar.Player.GetComponent<P_Status>().Current_HP += Amount;
                break;
            default:
                break;
        }
        amount_sys.Text_disable();
        miniGame_Manager.END();
    }
}
