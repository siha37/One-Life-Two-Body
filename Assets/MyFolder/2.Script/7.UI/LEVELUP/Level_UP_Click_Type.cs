using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level_UP_Click_Type : MonoBehaviour
{
    Gamemanager myChar;

    [SerializeField] private bool Click_And_Arrow;
    private Level_UP level_up;
    [SerializeField] private GameObject Level_UP_Panel_Click;
    [SerializeField] private GameObject Level_UP_Panel_Arrowk;
    [SerializeField] private Transform Perent_OBJ;
    [SerializeField] private List<SkillSlot> Slots;

    [SerializeField] private RectTransform SelectFrame;
    [SerializeField] private float Frame_MoveSpeed;
    [SerializeField] private int NowSelectNUM;

    [SerializeField] private GameObject Click_ex;
    [SerializeField] private GameObject Arrow_ex;
    [SerializeField] private GameObject BackGround_UI;


    private void Awake()
    {
        myChar = Gamemanager.myChar;
        level_up = myChar.Player.GetComponent<Level_UP>();  
    }
    private void Update()
    {
        ArrowControl();
    }
    public void CreatPanel(int Count)
    {
        if (Click_And_Arrow)
        {
            Click_ex.SetActive(true);
            Arrow_ex.SetActive(false);
            for (int i = 0; i < Count; i++)
            {
                Slots.Add(Instantiate(Level_UP_Panel_Click, Perent_OBJ).GetComponent<SkillSlot>());
                Slots[i].level_up_type = GetComponent<Level_UP_Click_Type>();
            }
        }
        else
        {
            Click_ex.SetActive(false);
            Arrow_ex.SetActive(true);
            for (int i = 0; i < Count; i++)
            {
                Slots.Add(Instantiate(Level_UP_Panel_Arrowk, Perent_OBJ).GetComponent<SkillSlot>());
                Slots[i].level_up_type = GetComponent<Level_UP_Click_Type>();
            }
        }
    }
    public void Reset_SkillSlot()
    {
        if (Slots.Count != 0)
        {
            for (int i = Slots.Count-1;i>= 0;i--)
            {
                SkillSlot slot= Slots[i];
                Slots.RemoveAt(i);
                Destroy(slot.gameObject);
            }
        }
    }

    public void PanelSetting(int MaxSlot_Count, bool type)
    {
        BackGround_UI.SetActive(true);
        Click_And_Arrow = type;
        Reset_SkillSlot();
        CreatPanel(MaxSlot_Count);
        Slots[0].Name.text = "HP_HEAL";
        Slots[0].ex.text = "HP 20% HEAL";
        Slots[0].Lv.text = "";
        Slots[0].button.interactable = true;
        Slots[0].HEAL = true;
        if (Click_And_Arrow)
        {
            SelectFrame.gameObject.SetActive(false);
            for (int i = 1; i < MaxSlot_Count; i++)
            {
                Slots[i].Name.text = "NAME_MISSING";
                Slots[i].ex.text = "EX_MISSING";
                Slots[i].Lv.text = "Lv_MISSING";
                Slots[i].button.interactable = false;
            }
        }
        else
        {
            SelectFrame.gameObject.SetActive(true);
            for (int i = 1; i < MaxSlot_Count; i++)
            {
                Slots[i].Name.text = "NAME_MISSING";
                Slots[i].ex.text = "EX_MISSING";
                Slots[i].Lv.text = "Lv_MISSING";
                Slots[i].SlotNUM = -1;
                Slots[i].button.interactable = false;
            }
        }
    }


    public void PanelSetting(int MaxSlot_Count,List<int> R_NUMS ,List<TOP_Projectile> Skill_list,bool type)
    {
        BackGround_UI.SetActive(true);
        Click_And_Arrow = type;
        Reset_SkillSlot();
        CreatPanel(MaxSlot_Count);
        if(Click_And_Arrow)
        {
            SelectFrame.gameObject.SetActive(false);
            for (int i = 0; i < R_NUMS.Count; i++)
            {
                Slots[i].Name.text = Skill_list[R_NUMS[i]].Name;
                Slots[i].ex.text = Skill_list[R_NUMS[i]].NextLevel_Ex.Replace("\\n", "\n");
                if (Skill_list[R_NUMS[i]].enabled)
                {
                    Slots[i].Lv.text = "Lv." + (Skill_list[R_NUMS[i]].GetLEVEL + 1).ToString();
                }
                else
                {
                    Slots[i].Lv.text = "NEW!";
                }

                Slots[i].button.interactable = true;
                Slots[i].SlotNUM = i;
            }
            for (int i = R_NUMS.Count; i < MaxSlot_Count; i++)
            {
                Slots[i].Name.text = "NAME_MISSING";
                Slots[i].ex.text = "EX_MISSING";
                Slots[i].Lv.text = "Lv_MISSING";
                Slots[i].button.interactable = false;
            }
        }
        else
        {
            SelectFrame.gameObject.SetActive(true);
            NowSelectNUM = 0;
            for (int i = 0; i < R_NUMS.Count; i++)
            {
                Slots[i].Name.text = Skill_list[R_NUMS[i]].Name;
                Slots[i].ex.text = Skill_list[R_NUMS[i]].NextLevel_Ex.Replace("\\n", "\n");
                if (Skill_list[R_NUMS[i]].enabled)
                {
                    Slots[i].Lv.text = "Lv." + (Skill_list[R_NUMS[i]].GetLEVEL + 1).ToString();
                }
                else
                {
                    Slots[i].Lv.text = "NEW!";
                }
                Slots[i].SlotNUM = i;
            }
            for (int i = R_NUMS.Count; i < MaxSlot_Count; i++)
            {
                Slots[i].Name.text = "NAME_MISSING";
                Slots[i].ex.text = "EX_MISSING";
                Slots[i].Lv.text = "Lv_MISSING";
                Slots[i].SlotNUM = -1;
            }
        }
    }
    // 선택한 슬롯의 번호를 레벨업 스크립트에 전달
    public void Selecting_Slot(int NUM)
    {
        if(NUM >= 0)
        {
            BackGround_UI.SetActive(false);
            level_up.Select_Skile(NUM);
        }
    }
    // HP 즉시 회복 선택시
    public void HEALING()
    {
        BackGround_UI.SetActive(false);
        level_up.Select_Skile();
    }

    // 키 입력을 통해 슬롯 선택 제어 ( Update )
    void ArrowControl()
    {
        if (!Click_And_Arrow && Slots.Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                NowSelectNUM--;
                if (NowSelectNUM < 0)
                {
                    NowSelectNUM = Slots.Count - 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                NowSelectNUM++;
                if (NowSelectNUM >= Slots.Count)
                {
                    NowSelectNUM = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Selecting_Slot(NowSelectNUM);
            }
            SelectFrame.position = Vector2.Lerp(SelectFrame.position, Slots[NowSelectNUM].GetComponent<RectTransform>().position, Frame_MoveSpeed);
        }
    }
}
