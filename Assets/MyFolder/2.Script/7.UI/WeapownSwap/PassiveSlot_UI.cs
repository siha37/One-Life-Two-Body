using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveSlot_UI : MonoBehaviour
{
    [SerializeField] GameObject Passive_Slot_OBJ;
    [SerializeField] private GameObject Passive_List_OBJ;
    [SerializeField] private List<Passive_Slot> Slots;

    public void CreatSlot(TOP_Projectile skill_data)
    {
        Passive_Slot nowSlot = Instantiate(Passive_Slot_OBJ, Passive_List_OBJ.transform).GetComponent<Passive_Slot>();
        nowSlot.Slot_Set(skill_data,skill_data.Skill_UI_Image);
        Slots.Add(nowSlot); 
    }

}
