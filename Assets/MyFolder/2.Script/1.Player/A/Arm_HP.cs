using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arm_HP : MonoBehaviour
{
    [Header("Data")]
    TopGameData TOP;

    Gamemanager myChar;
    P2_Animation_Controll animation_Con;
    [SerializeField] PlayerHead_Anim P2_head;
    P_Status p_status;
    [SerializeField] bool invincibility_ing;
    [SerializeField]
    private float Hiting_Delay_Time;
    [SerializeField] private Image Mini_HP_Bar;
    [SerializeField] private float DamageDivisor =2;
    public bool NO_HIT = false;
    public bool Attack_Able = false;
    public float Attack_Damage;
    public float KnockBack_Power;

    private void Awake()
    {
        TOP = TopGameData.Dataset;
        myChar = Gamemanager.myChar;
        myChar.P2 = this.gameObject;
        animation_Con = GetComponent<P2_Animation_Controll>();  
    }
    private void Start()
    {
        p_status= myChar.Player.GetComponent<P_Status>();
        p_status.OnAll_Invincibility += invincibility;
        Mini_HP_Bar.transform.parent.gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        Mini_HP_Bar.fillAmount = p_status.Current_HP / TOP.HP;
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ENEMY" && !invincibility_ing)
        {
            invincibility_HIT(Hiting_Delay_Time);
            if (!NO_HIT)
            {
                Enemy_Status ene = col.GetComponent<Enemy_Status>();
                animation_Con.AnimationStatus(1, "HIT", false);
                p_status.HIT(ene.Damage * DamageDivisor);
                HitEffect(1.5f);
                Mini_HP_Bar_Able();
            }
            if(Attack_Able)
            {
                Enemy_Status ene = col.GetComponent<Enemy_Status>();
                ene.Hit(Attack_Damage, KnockBack_Power,this.transform.position);
            }
        }
    }
    public void Hit(float Damage)
    {
        animation_Con.AnimationStatus(1, "HIT", false);
        p_status.HIT(Damage * DamageDivisor);
        HitEffect(1.5f);
        Mini_HP_Bar_Able();
    }

    public void invincibility(float time)
    {
        StartCoroutine(invincibility_Delay(time));
    }
    public void invincibility_HIT(float time)
    {
        StartCoroutine(invincibility_Delay_HIT(time));
    }
    IEnumerator invincibility_Delay_HIT(float time)
    {
        P2_head.Hurt_Start();
        invincibility_ing = true;
        yield return YieldInstructionCache.WaitForSeconds(time);
        P2_head.Hurt_End();
        invincibility_ing = false;
    }
    public void HitEffect(float degree)
    {
        StartCoroutine(HitEffect_Coroutin(degree));
    }
    IEnumerator HitEffect_Coroutin(float degree)
    {
        myChar.MainVolume.Vignette(0.1f * degree);
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        myChar.MainVolume.Vignette(0);
    }
    IEnumerator invincibility_Delay(float time)
    {
        invincibility_ing = true;
        yield return YieldInstructionCache.WaitForSeconds(time);
        invincibility_ing = false;
    }
    public void Mini_HP_Bar_Able()
    {
        StopCoroutine("Mini_HP_UI_Disable");
        Mini_HP_Bar.transform.parent.gameObject.SetActive(true);
        StartCoroutine("Mini_HP_UI_Disable");
    }

    IEnumerator Mini_HP_UI_Disable()
    {
        yield return YieldInstructionCache.WaitForSeconds(3);
        Mini_HP_Bar.transform.parent.gameObject.SetActive(false);
    }
}
