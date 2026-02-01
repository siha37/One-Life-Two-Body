using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlot : MonoBehaviour
{
    

    public int SlotNUM;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI ex;
    public TextMeshProUGUI Lv;
    public Button button;
    public Level_UP_Click_Type level_up_type;
    public bool HEAL = false;

    private void Awake()
    {
        Name = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        ex = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if(GetComponent<Button>() != null)
        {
            button = GetComponent<Button>();
        }
    }
    public void OnClick()
    {
        if(!HEAL)
        {
            level_up_type.Selecting_Slot(SlotNUM);
        }
        else
        {
            level_up_type.HEALING();
        }
    }
}
