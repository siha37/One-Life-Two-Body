using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SHOTTING;

public class P_Status : MonoBehaviour
{
    [Header("Data")]
    TopGameData TOP;
    Gamemanager myChar;

    [Header("Component")]
    P1_Animation_Controller animation_Con;
    [SerializeField] P2_Animation_Controll animation_Con_2;
    [SerializeField] PlayerHead_Anim P1_head;

    //Player Data
    [Space(5)]
    [SerializeField] 
    private int Level;
    [SerializeField]
    private bool invincibility_ing;
    [Space(2)]
    public float Current_HP;
    private float Plus_HP=0;
    private float Current_plus_HP=0;
    private float Plus_HP_Time=0;
    private bool death_anim = false;
    
    
    [Space(2)]
    public float Current_EXP;


    [Space(2)]
    [SerializeField]
    private float Current_Mana;
    private float Plus_Mana=0;
    private float Current_plus_Mana=0;
    private float Plus_Mana_Time =0;
    private float Mana_Use_Timer = 0;


    [Space(2)]
    [SerializeField] private float Current_Energy;
    private float Plus_Energy;
    private float Current_plus_Energy;
    private float Plus_Energy_Time;
    private float Energy_Use_Timer = 0;

    [Space(2)]
    [SerializeField]
    private float Hiting_Delay_Time;

    //UI
    [SerializeField] private Image Hp_Bar;
    [SerializeField] private Image Exp_Bar;
    [SerializeField] private Image Energy_Bar;
    [SerializeField] private Image Mana_Bar;
    [SerializeField] private Image Mini_HP_Bar;

    [Space(5)]
    [SerializeField] private TextMeshProUGUI LevelText;

    [Space(5)]
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private Animator Resource_Glitch;



    //Component
    [SerializeField] private Level_UP level_UP;
    private P_Move p_move;


    //Event 
    public delegate void Allinvincibility(float time);
    public event Allinvincibility OnAll_Invincibility;

    #region UPDATE AND START
    private void Start()
    {
        OnAll_Invincibility += invincibility;
        TOP = TopGameData.Dataset;
        myChar = Gamemanager.myChar; 
        Max_EXP_Set();
        level_UP = GetComponent<Level_UP>();
        Current_HP = TOP.HP;
        Current_Energy = TOP.Energy;
        Current_Mana = TOP.Mana;
        Exp_Bar.fillAmount = 0;
        Hp_Bar.fillAmount = 1;
        Energy_Bar.fillAmount = 1;
        Mana_Bar.fillAmount = 1;
        Mini_HP_Bar.transform.parent.gameObject.SetActive(false);
        animation_Con = GetComponent<P1_Animation_Controller>();
        p_move = GetComponent<P_Move>();
    }
    private void Update()
    {
        Resource_Timer();
        PlusStatus_Timer();
        Cheting();
    }
    private void LateUpdate()
    {
        Bar_UI_FillAmount();
        Low_Hp_Effect();
    }
    #endregion

    #region COLLIDER
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ENEMY" && !invincibility_ing)
        {
            Enemy_Status ene = col.GetComponent<Enemy_Status>();
            HIT(ene);
            HitEffect(1);
            Mini_HP_Bar_Able();
        }
    }
    #endregion

    #region HIT AND HILL
    public bool HIT(Enemy_Status ene)
    {
        float damage = ene.Damage;
        invincibility_HIT(Hiting_Delay_Time);
        if (Plus_HP > 0)
        {
            Current_plus_HP -= damage;
            if (Current_plus_HP < 0)
            {
                Current_HP += Current_plus_HP;
            }
        }
        else
        {
            Current_HP -= damage;
        }
        animation_Con.AnimationStatus(1, "HIT", false);
        if (Current_HP <= 0)
        {
            animation_Con.AnimationStatus(1, "DEATH", false);
            animation_Con_2.AnimationStatus(1, "DEATH", false);
            p_move.Move_Able = false;
            StopAllCoroutines();
            Invoke("DieSet", 3);
        }
        return true;
    }
    public bool HIT(float damage)
    {
        invincibility_HIT(Hiting_Delay_Time);
        if (Plus_HP > 0)
        {
            Current_plus_HP -= damage;
            if (Current_plus_HP < 0)
            {
                Current_HP += Current_plus_HP;
            }
        }
        else
        {
            Current_HP -= damage;
        }
        if (Current_HP <= 0)
        {
            if(!death_anim)
            {
                death_anim = true;
                animation_Con.AnimationStatus(1, "DEATH", false);
                animation_Con_2.AnimationStatus(1, "DEATH", false);
            }
            p_move.Move_Able = false;
            StopAllCoroutines();
            Invoke("DieSet", 3);
        }
        return true;
    }

    public void Hill(float hillAmount)
    {
        Current_HP += hillAmount;
        if (Current_HP > TOP.HP)
        {
            Current_HP = TOP.HP;
        }
    }

    public void invincibility(float time)
    {
        StartCoroutine(invincibility_Delay(time));
    }
    public void invincibility_HIT(float time)
    {
        StartCoroutine(invincibility_Delay_HIT(time));
    }
    IEnumerator invincibility_Delay(float time)
    {
        P1_head.Hurt_Start();
        invincibility_ing = true;
        yield return YieldInstructionCache.WaitForSeconds(time);
        P1_head.Hurt_End();
        invincibility_ing = false;
    }
    IEnumerator invincibility_Delay_HIT(float time)
    {
        P1_head.Hurt_Start();
        invincibility_ing = true;
        yield return YieldInstructionCache.WaitForSeconds(time);
        P1_head.Hurt_End();
        invincibility_ing = false;
    }

    public void All_Invincibility_ing(float time)
    {
        OnAll_Invincibility(time);
    }
    #endregion

    #region Effect AND UI
    private void Bar_UI_FillAmount()
    {
        Hp_Bar.fillAmount = (Current_HP + Current_plus_HP) / (TOP.HP + Plus_HP);

        if(Mini_HP_Bar.transform.parent.gameObject.activeSelf)
        {
            Mini_HP_Bar.fillAmount = (Current_HP + Current_plus_HP) / (TOP.HP + Plus_HP);
        }

        Exp_Bar.fillAmount = Current_EXP / TOP.EXP;

        Energy_Bar.fillAmount = (Current_Energy+ Current_plus_Energy) / (TOP.Energy+Plus_Energy);

        Mana_Bar.fillAmount = (Current_Mana+Current_plus_Mana) / (TOP.Mana+Plus_Mana);
    }
    private void Low_Hp_Effect()
    {
        if((Current_HP+ Current_plus_HP) / (TOP.HP+ Plus_HP) < 0.05f)
        {
            myChar.MainVolume.Vignette(0.2f);
        }
        else if((Current_HP+ Current_plus_HP) / (TOP.HP+ Plus_HP) < 0.1f)
        {
            myChar.MainVolume.Vignette(0.35f);
        }
    }
    public void  Mini_HP_Bar_Able()
    {
        if(this.gameObject.activeSelf)
        {
            StopCoroutine("Mini_HP_UI_Disable");
            Mini_HP_Bar.transform.parent.gameObject.SetActive(true);
            StartCoroutine("Mini_HP_UI_Disable");
        }
    }
    IEnumerator Mini_HP_UI_Disable()
    {
        yield return YieldInstructionCache.WaitForSeconds(3);
        Mini_HP_Bar.transform.parent.gameObject.SetActive(false);
    }

    public void HitEffect(float degree)
    {
        StartCoroutine(HitEffect_Coroutin(degree));
    }
    IEnumerator HitEffect_Coroutin(float degree)
    {
        myChar.MainVolume.Vignette(0.1f*degree);
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        myChar.MainVolume.Vignette(0);
    }
    #endregion

    #region EXP
    public void EXP_GET(float Exp_Amount)
    {
        Current_EXP += Exp_Amount;
        if (Current_EXP >= TOP.EXP)
        {
            Full_Exp();
        }
    }
    private void Full_Exp()
    {
        Current_EXP = 0;
        Level++;
        Lv_Status_UP();
        Max_EXP_Set();
        level_UP.LEVEL_UP();
    }
    void Lv_Status_UP()
    {
        TOP.HP += TOP.Lv_HP;
        TOP.Energy += TOP.Lv_Energy;
        TOP.Mana += TOP.Lv_MP;
    }

    void Max_EXP_Set()
    {
        LevelText.text = "Lv."+ Level.ToString();
        TOP.EXP = 6.5f + 3.5f * Level;
    }
    #endregion

    #region Resource

    public void Lack_Resource(Use_Resource type)
    {
        if(!Resource_Glitch.GetCurrentAnimatorStateInfo(0).IsName("EnergyGlitch") || !Resource_Glitch.GetCurrentAnimatorStateInfo(0).IsName("ManaGlitch"))
        {
            switch (type)
            {
                case Use_Resource.MANA:
                    Resource_Glitch.gameObject.SetActive(true);
                    Resource_Glitch.SetTrigger("MANA");
                    break;
                case Use_Resource.ENERGY:
                    Resource_Glitch.gameObject.SetActive(true);
                    Resource_Glitch.SetTrigger("ENERGY");
                    break;
                default:
                    break;
            }
        }
    }

    private void Energy_Recovery()
    {
        if(Current_Energy < TOP.Energy)
        {
            TOP.Energy_Recovery = 0;
            Current_Energy += TOP.Energy_Recovery*Time.deltaTime;
        }
    }
    private void Mana_Recovery()
    {
        if (Current_Mana < TOP.Mana)
        {
            TOP.Mana_Recovery = 0;
            Current_Mana += TOP.Mana_Recovery*Time.deltaTime;
        }
    }
    public bool Able_Resource_Chack_Effect(Use_Resource type, float Amount)
    {
        bool result = true;
        switch (type)
        {
            case Use_Resource.MANA:
                if (Amount > Current_Mana + Current_plus_Mana)
                {
                    result = false;
                    Lack_Resource(type);
                }
                break;
            case Use_Resource.ENERGY:
                if (Amount > Current_Energy + Current_plus_Energy)
                {
                    result = false;
                    Lack_Resource(type);
                }
                break;
            default:
                break;
        }
        return result;
    }
    public bool Able_Resource_Chack(Use_Resource type, float Amount)
    {
        bool result = true;
        switch (type)
        {
            case Use_Resource.MANA:
                if (Amount > Current_Mana + Current_plus_Mana)
                {
                    result = false;
                }
                break;
            case Use_Resource.ENERGY:
                if (Amount > Current_Energy + Current_plus_Energy)
                {
                    result = false;
                }
                break;
            default:
                break;
        }
        return result;
    }
    public void Use_Resource_Caclulation(Use_Resource type,float Amount)
    {
        switch (type)
        {
            case Use_Resource.MANA:
                if (Amount < 0)
                {
                    Mana_Use_Timer = TOP.Resource_Delay;
                    if (Current_plus_Mana > 0)
                    {
                        Current_plus_Mana += Amount;
                        if (Current_plus_Mana < 0)
                        {
                            Current_Mana += Current_plus_Mana;
                        }
                    }
                    else
                    {
                        Current_Mana += Amount;
                    }
                }
                else
                {
                    if (Current_Mana + Amount < TOP.Mana)
                    {
                        Current_Mana += Amount;
                    }
                    else
                    {
                        Current_Mana = TOP.Mana;
                    }
                }
                break;
            case Use_Resource.ENERGY:
                if (Amount < 0)
                {
                    Energy_Use_Timer = TOP.Resource_Delay;
                    if (Current_plus_Energy > 0)
                    {
                        Current_plus_Energy += Amount;
                        if (Current_plus_Energy < 0)
                        {
                            Current_Energy += Current_plus_Energy;
                        }
                    }
                    else
                    {
                        Current_Energy += Amount;
                    }
                }
                else
                {
                    if(Current_Energy + Amount < TOP.Energy)
                    {
                        Current_Energy += Amount;
                    }
                    else
                    {
                        Current_Energy = TOP.Energy;
                    }
                }
                break;
            default:
                break;
        }
    }
    public void Use_Resource_Caclulation_Passive(Use_Resource type,float Amount)
    {
        switch (type)
        {
            case Use_Resource.MANA:
                if (Amount < 0)
                {
                    if (Current_plus_Mana > 0)
                    {
                        Current_plus_Mana += Amount;
                        if (Current_plus_Mana < 0)
                        {
                            Current_Mana += Current_plus_Mana;
                        }
                    }
                    else
                    {
                        if (Current_Mana + Amount < TOP.Mana)
                        {
                            Current_Mana += Amount;
                        }
                        else
                        {
                            Current_Mana = TOP.Mana;
                        }
                    }
                }
                else
                {
                    Current_Mana += Amount;
                }
                break;
            case Use_Resource.ENERGY:
                if (Amount < 0)
                {
                    if (Current_plus_Energy > 0)
                    {
                        Current_plus_Energy += Amount;
                        if (Current_plus_Energy < 0)
                        {
                            Current_Energy += Current_plus_Energy;
                        }
                    }
                    else
                    {
                        Current_Energy += Amount;
                    }
                }
                else
                {
                    if (Current_Energy + Amount < TOP.Energy)
                    {
                        Current_Energy += Amount;
                    }
                    else
                    {
                        Current_Energy = TOP.Energy;
                    }
                }
                break;
            default:
                break;
        }
    }
    private void Resource_Timer()
    {
        if (Energy_Use_Timer > 0)
        {
            Energy_Use_Timer -= Time.deltaTime;
        }
        else
        {
            Energy_Recovery();
        }
        if (Mana_Use_Timer > 0)
        {
            Mana_Use_Timer -= Time.deltaTime;
        }
        else
        {
            Mana_Recovery();
        }
    }
    #endregion

    #region Plus_Status
    public void Plus_HP_Set(float Amount,float time)
    {
        Plus_HP = Amount;
        Current_plus_HP =Amount;
        Plus_HP_Time = time;
    }
    public void Plus_Mana_Set(float Amount, float time)
    {
        Plus_Mana = Amount;
        Current_plus_Mana = Amount;
        Plus_Mana_Time = time;
    }
    public void Plus_Energy_Set(float Amount,float time)
    {
        Plus_Energy = Amount;
        Current_plus_Energy = Amount;
        Plus_Energy_Time = time;
    }
    public void PlusStatus_Timer()
    {
        if (Plus_HP_Time > 0)
        {
            Plus_HP_Time -=Time.deltaTime;
        }
        else
        {
            Plus_HP = 0;
            Current_plus_HP = 0;
        }
        if (Plus_Mana_Time > 0)
        {
            Plus_Mana_Time -=Time.deltaTime;
        }
        else
        {
            Plus_Mana = 0;
            Current_plus_Mana = 0;
        }
        if (Plus_Energy_Time > 0)
        {
            Plus_Energy_Time -= Time.deltaTime;
        }
        else
        {
            Plus_Energy = 0;
            Current_plus_Energy = 0;
        }
    }
    #endregion

    #region DIE
    private void DieSet()
    {
        GameOverUI.SetActive(true);
    }
    #endregion

    #region cheet
    void Cheting()
    {
        if(myChar.CHEETING)
        {
            if(Input.GetKeyDown(KeyCode.F9))
            {
                Current_HP += 100;
            }
            if(Input.GetKeyDown(KeyCode.F10))
            {
                Full_Exp();
            }
        }
    }
    #endregion
}
