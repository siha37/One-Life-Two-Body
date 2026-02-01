using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Level_UP : MonoBehaviour
{
    [SerializeField] PauseSystem pause;

    //Component
    P_Status status;

    //UI
    [SerializeField] private WeapownSwap swap;
    [SerializeField] private PassiveSlot_UI passive_slot;

    //Skill Component
    public List<TOP_Projectile> A_top_projects = new List<TOP_Projectile>();
    public List<TOP_Projectile> P_top_projects = new List<TOP_Projectile>();
    //다음 레벨업 스킬 종류
    // A : 액티브  P: 패시브
    // A : true   P: false
    private bool A_or_P = false;

    //활성화 된 무기
    public List<TOP_Projectile> Active_A_Skill = new List<TOP_Projectile>();
    public List<TOP_Projectile> Active_P_Skill = new List<TOP_Projectile>();


    //시작 무기 랜덤 범위
    [SerializeField] private int MIN_StartWeapon_NUM;
    [SerializeField] private int MAX_StartWeapon_NUM;

    [SerializeField] private int MaxSlot =3;

    //P1,P2 최대 무기 소지 수
    [SerializeField] int A_Able_Skill_Count =3;
    [SerializeField] int P_Able_Skill_Count =3;

    //Level_UP_UI
    [SerializeField] private GameObject Level_UP_Panel;
     private Level_UP_Click_Type Level_UP_Panel_Set;
    TextMeshProUGUI Title;
    private List<TOP_Projectile> Skill_UP_List = new List<TOP_Projectile>();
    private List<int> NumberData = new List<int>();

    [SerializeField] private int Limit_Active, Limit_Passive;

    private void Awake()
    {
        Level_UP_Panel_Set = Level_UP_Panel.GetComponent<Level_UP_Click_Type>();
        Title = Level_UP_Panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        status = GetComponent<P_Status>();
    }

    private void Start()
    {
        int StartWeaponNUM = Random.Range(MIN_StartWeapon_NUM, MAX_StartWeapon_NUM);
        A_top_projects[StartWeaponNUM].Level_Setting(0);
        A_top_projects[StartWeaponNUM].enabled = true;
        A_top_projects[StartWeaponNUM].Data_Input();
        swap.Start_Set();
    }

    public void UI_Slot_Creating(TOP_Projectile skill)
    {
        passive_slot.CreatSlot(skill);
    }


    // 레벨업 시 발동
    public void LEVEL_UP()
    {
        // 스킬 업 가능한 리스트 생성
        List_All_Reset();
        List<TOP_Projectile> list = new List<TOP_Projectile>();

        // P1 또는 P2 스킬 업그레이드 선택 후 리스트 추가
        if (A_or_P)
        {
            if(Active_A_Skill.Count >= A_Able_Skill_Count)
            {
                for (int i = 0; i < Active_A_Skill.Count; i++)
                {
                    if (!Active_A_Skill[i].MaxLevel_Chack)
                    {
                        list.Add(Active_A_Skill[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < A_top_projects.Count; i++)
                {
                    if (!A_top_projects[i].MaxLevel_Chack)
                    {
                        list.Add(A_top_projects[i]);
                    }
                }
            }
            Title.text = "P2_Skill Select";
        }
        else
        {
            if(Active_P_Skill.Count >= P_Able_Skill_Count)
            {
                for (int i = 0; i < Active_P_Skill.Count; i++)
                {
                    if (!Active_P_Skill[i].MaxLevel_Chack)
                    {
                        list.Add(Active_P_Skill[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < P_top_projects.Count; i++)
                {
                    if (!P_top_projects[i].MaxLevel_Chack)
                    {
                        list.Add(P_top_projects[i]);
                    }
                }
            }
            Title.text = "P1_Skill Select";
        }
        pause.TimeStop();
        if(list.Count > 0)
        {
            List<int> R_NUMS = RandomNUMS(list.Count, MaxSlot);
            Level_UP_Panel.SetActive(true);
            Level_UP_Panel_Set.PanelSetting(MaxSlot, R_NUMS, list, A_or_P);
            for (int i = 0; i < R_NUMS.Count; i++)
            {
                Skill_UP_List.Add(list[R_NUMS[i]]);
            }
            NumberData = R_NUMS;
        }
        else
        {
            Level_UP_Panel.SetActive(true);
            Level_UP_Panel_Set.PanelSetting(MaxSlot, A_or_P);
        }
    }
    private void List_All_Reset()
    {
        Skill_UP_List.Clear();
        NumberData.Clear();
    }
    private List<int> RandomNUMS(int MaxNUM,int Count)
    {
        List<int> num_list = new List<int>();
        bool isSame =false;

        if(MaxNUM < Count)
        {
            Count = MaxNUM;
        }
        for(int i=0;i<Count;i++)
        {
            int a = Random.Range(0,MaxNUM);
            isSame = false;
            for(int j=0;j< num_list.Count; j++)
            {
                if(num_list[j] == a)
                {
                    isSame = true;
                    break;
                }
            }
            if (isSame)
            {
                i--;
                continue;
            }
            num_list.Add(a);
        }
        return num_list;
    }
    public void Select_Skile()
    {
        status.All_Invincibility_ing(1);
        status.Current_HP += status.Current_HP * 20 * 0.01f;
        Level_UP_Panel.SetActive(false);
        pause.TimeFlow();
    }
    public void Select_Skile(int NUM)
    {
        status.All_Invincibility_ing(1);
        if (Skill_UP_List[NUM])
        {
            if (Skill_UP_List[NUM].enabled)
            {
                if (Skill_UP_List[NUM].LEVEL_UP())
                {
                    if (A_or_P)
                    {
                        A_top_projects.Remove(Skill_UP_List[NUM]);
                    }
                    else
                    {
                        P_top_projects.Remove(Skill_UP_List[NUM]);
                    }
                } 
            }
            else
            {
                Skill_UP_List[NUM].LEVEL_UP();
                Skill_UP_List[NUM].enabled = true;
                if(A_or_P)
                {
                    //Active_A_Skill.Add(Skill_UP_List[NUM]);
                }
                else
                {
                    Active_P_Skill.Add(Skill_UP_List[NUM]);
                }
            }

            // 다음 스킬 업그레이드 플레이어 주체 변경
            // p1 -> p2
            // p2 -> p1
            switch (A_or_P)
            {
                case true:
                    A_or_P = false;
                    break;
                case false:
                    A_or_P = true;
                    break;
            }

            Level_UP_Panel.SetActive(false);
            pause.TimeFlow();
        }
    }
}
