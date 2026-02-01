using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SHOTTING;
using TMPro;


public class Arm_Shot : MonoBehaviour
{
    #region DATA
    //class
    Gamemanager myChar;
    P_Status p_status;
    public Arm_Enforce Enforce;
    [SerializeField] private WeapownSwap swap;
    [SerializeField] Arm_Rot arm_rot;
    [SerializeField] P2_Animation_Controll animation_Con;

    //in data
    [SerializeField] private bool Shot_Able;
    [SerializeField] private float CurrentTime;
    [SerializeField] private float leftoverCoolTime;
    [SerializeField] public int Charging_Counting;
    [SerializeField] private bool Charging = false;
    [SerializeField] private bool Hold_Shot = false;
    [SerializeField] private bool Shotting = false;
    private bool Whill_Able = true;
    public bool SHOT_ONOFF = true;
    float NowTime;


    //Data Input Variable
    [ReadOnly] public float Shot_Delay;
    [ReadOnly] public float Charging_Delay;
    [ReadOnly] public int Charge_Count;
    [ReadOnly] public Use_Resource Resource_Type;
    [ReadOnly] public float Resource_Amount;


    //Projectile Script Data
    public List<TOP_Projectile> TOP_Projectiles = new List<TOP_Projectile>();
    [SerializeField] private int Now_Weapon_NUM =0;
    private int Before_NUM;

    //Component
    [SerializeField] private Animator anim;
    private Main_PostProcessing Main_Volme;

    //Canva UI
    [SerializeField] private Image CoolTimeUI;
    [SerializeField] private TextMeshProUGUI Charging_Num_Text;
    [SerializeField] private Color Rast_Charging_Color;
    #endregion


    private void Start()
    {
        myChar = Gamemanager.myChar;
        p_status = myChar.Player.GetComponent<P_Status>();
        Enforce = GetComponent<Arm_Enforce>();
        Main_Volme = myChar.MainVolume;
        anim = transform.parent.GetComponent<Animator>();
        Now_Weapon_NUM = 0;
    }

    void Update()
    {
        if(!myChar.Pause)
        {
            if(TOP_Projectiles.Count > 0)
            {
                SHOTTING();
            }

            UpdateTimer();
            if (Charging)
            {
                Charging_Timer();
            }
        }
    }


    #region Weapown_Change
    public void WeapownList_Add(TOP_Projectile skill, Sprite _icon)
    {
        TOP_Projectiles.Add(skill);
        swap.ImageCreate(_icon, TOP_Projectiles.Count - 1);
    }
    private void MouseWhill()
    {
        if (Whill_Able)
        {
            float whellinput = Input.GetAxis("Mouse ScrollWheel");
            if (whellinput > 0)
            {
                int Before_NUM = Now_Weapon_NUM;
                if (Now_Weapon_NUM == 0)
                {
                    Now_Weapon_NUM = TOP_Projectiles.Count - 1;
                }
                else
                {
                    Now_Weapon_NUM--;
                }
                swap.Weapown_NUM_Change = Now_Weapon_NUM;
                Projectil_Change(Now_Weapon_NUM, Before_NUM);
                StartCoroutine("MouseWhillDelay");
            }
            else if (whellinput < 0)
            {
                int Before_NUM = Now_Weapon_NUM;
                if (Now_Weapon_NUM == TOP_Projectiles.Count - 1)
                {
                    Now_Weapon_NUM = 0;
                }
                else
                {
                    Now_Weapon_NUM++;
                }
                swap.Weapown_NUM_Change = Now_Weapon_NUM;
                Projectil_Change(Now_Weapon_NUM, Before_NUM);
                StartCoroutine("MouseWhillDelay");
            }
        }
    }
    IEnumerator MouseWhillDelay()
    {
        Whill_Able = false;
        yield return YieldInstructionCache.WaitForSeconds(0.3f);
        Whill_Able = true;
    }
    private void Shoting_Stop_WeapownChanging(ShotType type, int BeforeNUM)
    {
        if (Shotting)
        {
            switch (type)
            {
                case ShotType.Click:
                    break;
                case ShotType.ClickANDHold:
                    ClickAndHoldEND(BeforeNUM);
                    break;
                case ShotType.Hold:
                    HoldShotStop(BeforeNUM);
                    break;
                case ShotType.Passive:
                    break;
                default:
                    break;
            }
        }
    }
    //¹«±â Ã¤ÀÎÁö
    private void Projectil_Change(int NUM, int Before_NUM)
    {
        Charging_Num_Text.gameObject.SetActive(false);
        Shoting_Stop_WeapownChanging(TOP_Projectiles[Before_NUM].S_TYPE, Before_NUM);
        if (!Shot_Able)
        {
            TOP_Projectiles[Before_NUM].GET_SET_CurrentTime = CurrentTime;
            CurrentTime = -1;
            Shot_Able = true;
        }
        TOP_Projectiles[NUM].Data_Input();
        leftoverCoolTime = TOP_Projectiles[NUM].GET_SET_CurrentTime;
        if (leftoverCoolTime > 0)
        {
            Start_Timer();
        }
        else
        {
            CoolTimeUI.fillAmount = 1;
        }
    }

    public void PivotBone_Set(string bonename)
    {
        arm_rot.PivotBone_Set(bonename);
    }

    #endregion

    #region Shot_System
    private void ClickAndHoldEND()
    {
        animation_Con.ShotAtionStatus(2, TOP_Projectiles[Now_Weapon_NUM].Shot_anim, false, true);
        Charging_Num_Text.gameObject.SetActive(false);
        if (!Charging)
        {
            return;
        }
        Charging = false;
        if (Charging_Counting == 0)
        {
            TOP_Projectiles[Now_Weapon_NUM].Charging_Projectile_Set(0);
        }
        UsingResource(Resource_Type, Resource_Amount);
        TOP_Projectiles[Now_Weapon_NUM].ProjectileCreat(Charging_Counting);
        Start_Timer();
        Shotting = false;
    }
    private void ClickAndHoldEND(int NUM)
    {
        if (!Charging)
        {
            return;
        }
        Charging = false;
        if(Charging_Counting == 0)
        {
            TOP_Projectiles[Now_Weapon_NUM].Charging_Projectile_Set(0);
        }
        UsingResource(Resource_Type, Resource_Amount);
        TOP_Projectiles[NUM].ProjectileCreat(Charging_Counting);
        Start_Timer();
        Shotting = false;
    }
    private void HoldShotStop()
    {
        animation_Con.ShotAtionStatus_Set(2, null, true, true);
        Hold_Shot = false;
        TOP_Projectiles[Now_Weapon_NUM].StopShot();
        Start_Timer();
        Shotting = false;
    }
    private void HoldShotStop(int NUM)
    {
        animation_Con.ShotAtionStatus_Set(2, null, true, true);
        Hold_Shot = false;
        TOP_Projectiles[NUM].StopShot();
        Start_Timer();
        Shotting = false;
    }

    public void AutoStopControll()
    {
        Charging = false;
        HoldShotStop();
        Start_Timer();
    }

    private void SHOTTING()
    {
        MouseWhill();
        if (Shot_Able && SHOT_ONOFF)
        {
            switch (TOP_Projectiles[Now_Weapon_NUM].S_TYPE)
            {
                case ShotType.Click:
                    if (Input.GetKey(KeyCode.Mouse0) && p_status.Able_Resource_Chack_Effect(Resource_Type, Resource_Amount))
                    {
                        Shotting = true;
                        UsingResource(Resource_Type, Resource_Amount);
                        animation_Con.ShotAtionStatus(2, TOP_Projectiles[Now_Weapon_NUM].Shot_anim, false, true);
                        TOP_Projectiles[Now_Weapon_NUM].ProjectileCreat();
                        Start_Timer();
                    }
                    break;
                case ShotType.ClickANDHold:
                    if (Input.GetKey(KeyCode.Mouse0) && p_status.Able_Resource_Chack_Effect(Resource_Type, Resource_Amount))//Â÷Áö ¼¦
                    {
                        Shotting = true;
                        if (!Charging)
                        {
                            Charging_Num_Text.text = "0";
                            Charging_Num_Text.gameObject.SetActive(true);
                            Charging_Num_Text.color = Color.white;
                            Charging_Counting = 0;
                            NowTime = 0;
                            animation_Con.ShotAtionStatus(2, TOP_Projectiles[Now_Weapon_NUM].Charging_anim,false,false);
                            Charging = true;
                            //TOP_Projectiles[Now_Weapon_NUM].Charging_Projectile_Set(0);
                        }
                    }
                    if (Input.GetKeyUp(KeyCode.Mouse0) && p_status.Able_Resource_Chack(Resource_Type, Resource_Amount))//´ÜÀÏ ¼¦
                    {
                        ClickAndHoldEND();
                    }
                    break;
                case ShotType.Hold: //È¦µå
                    if (Input.GetKeyDown(KeyCode.Mouse0) && p_status.Able_Resource_Chack(Resource_Type, Resource_Amount))
                    {
                        Shotting = true;
                        Hold_Shot = true;
                        animation_Con.ShotAtionStatus(2, TOP_Projectiles[Now_Weapon_NUM].Shot_anim, true, false);
                        TOP_Projectiles[Now_Weapon_NUM].ProjectileCreat();
                        Use_Resource_Roof();
                    }
                    if (Input.GetKeyUp(KeyCode.Mouse0) && Hold_Shot)
                    {
                        HoldShotStop();
                    }
                    break;
            }
        }
    }
    #endregion

    #region CoolTime
    private void Start_Timer()
    {
        Shot_Able = false;
        if(leftoverCoolTime > 0)
        {
            CurrentTime = leftoverCoolTime;
            leftoverCoolTime = -1;
        }
        else
        {
            CurrentTime = Shot_Delay;
        }
    }

    void UpdateTimer()
    {
        //ÀÜÁ¸ ÄðÅ¸ÀÓ ½ÇÇà
        if (!Shot_Able && CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            CoolTimeUI.fillAmount = 1 - CurrentTime / Shot_Delay;
        }
        else if (!Shot_Able)//Áß°£ ¹«±â º¯°æ
        {
            CoolTimeUI.fillAmount = 1;
            Shot_Able = true;
            TOP_Projectiles[Now_Weapon_NUM].GET_SET_CurrentTime = -1;
        }
    }
    #endregion

    #region Charging_System
    void Charging_Timer()
    {
        if (Charge_Count > Charging_Counting)
        {
            if (Charging_Delay > NowTime)
            {
                NowTime += Time.deltaTime;
            }
            else
            {
                if (p_status.Able_Resource_Chack_Effect(Resource_Type, Resource_Amount + (Resource_Amount / 2)))
                {
                    UsingResource(Resource_Type, Resource_Amount / 2);
                    StartCoroutine(Main_Volme.LensEffect(0.3f));
                    TOP_Projectiles[Now_Weapon_NUM].Charing_Effect();
                    TOP_Projectiles[Now_Weapon_NUM].Charging_Projectile_Set(Charging_Counting++);
                    Charging_Num_Text.text = Charging_Counting.ToString();
                    if(Charge_Count == Charging_Counting )
                    {
                        Charging_Num_Text.color = Rast_Charging_Color;
                    }
                    NowTime = 0;
                }
                else
                {
                    ClickAndHoldEND();
                    Charging = false;
                    return;
                }
            }
        }
    }
    #endregion

    #region Resource
    private void Use_Resource_Roof()
    {
        if (p_status.Able_Resource_Chack_Effect(Resource_Type,Resource_Amount))
        {
            UsingResource(Resource_Type, Resource_Amount);
            if (Hold_Shot)
            {
                Invoke("Use_Resource_Roof",Time.deltaTime);
            }
        }
        else
        {
            HoldShotStop();
        }
    }
    private void UsingResource(Use_Resource type,float Amount)
    {
        p_status.Use_Resource_Caclulation(type, -Amount);
    }
    #endregion

}
