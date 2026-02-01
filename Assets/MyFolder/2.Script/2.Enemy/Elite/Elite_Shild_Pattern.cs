using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite_Shild_Pattern : MonoBehaviour
{
    Gamemanager myChar;

    Emy_Move move;
    Enemy_Elite_Status status;
    Rigidbody2D rd;
    Vector3 Rush_Target;

    [Header("Swipe")]
    [ReadOnly][SerializeField] float Swipe_Damage;
    [ReadOnly][SerializeField] float Swipe_Delay;
    [ReadOnly][SerializeField] float Swipe_CoolTime;
    float Swipe_CurrentyTime;
    [SerializeField] float Swipe_Chack_Distance;
    [SerializeField] Transform HitBox_Rotation;

    [Header("Rush")]
    [ReadOnly][SerializeField] float Rush_Speed;
    [ReadOnly][SerializeField] float Rush_Currenty_Speed;
    float Rush_Delay;
    float Rush_CoolTime;
    float Rush_CurrentyTime;
    [SerializeField] float Rush_Able_Time;

    Transform target_Pos;
    Vector3 target_Dir;

    bool Pattern_Delay_Chack  =false;
    [SerializeField] float Pattern_Delay_CoolTime;
    float Pattern_Delay_Currenty_CoolTime;

    bool Skill_using =false;
    bool Swipe_using =false;
    bool Rush_uising = false;
    bool Rush_Velocity_Bool =false;
    Animator animator;

    public Player_Scen player_Scen;
    public Player_Scen swipe_Scen;

    [Space(10)]
    [Header("Audio")]
    [SerializeField] AudioSource _audio;
    public AudioClip Rush_Set_Sound;
    public AudioClip Swipe_Hit_Sound;


    private void Start()
    {
        myChar = Gamemanager.myChar;
        move = GetComponent<Emy_Move>();
        animator = GetComponent<Animator>();
        status = GetComponent<Enemy_Elite_Status>();
        rd = GetComponent<Rigidbody2D>();
        Swipe_Damage = status.Pattern_1_Damage;
        Swipe_Delay = status.Pattern_1_Delay;
        Swipe_CoolTime = status.Pattern_1_CoolTime;
        Rush_Speed = status.Rush_Speed;
        Rush_Delay = status.Pattern_2_Delay;
        Rush_CoolTime = status.Pattern_2_CoolTime;
    }
    private void LateUpdate()
    {
        if (!Skill_using)
        {
            for (int i = 0; i < player_Scen.Player_List.Count; i++)
            {
                float distance = (transform.position - player_Scen.Player_List[i].position).magnitude;
                if (Swipe_CurrentyTime >= Swipe_CoolTime && distance < Swipe_Chack_Distance)
                {
                    target_Pos = player_Scen.Player_List[i].transform;
                    Swipe();
                    break;
                }
                else if(Rush_CurrentyTime >= Rush_CoolTime)
                {
                    target_Pos = player_Scen.Player_List[i].transform;
                    Rush();
                    break;
                }
            }
        }
        CoolTime_Update();
        Rush_Velocity_Chack();
    }
    void CoolTime_Update()
    {
        if(Swipe_CurrentyTime < Swipe_CoolTime && !Swipe_using)
        {
            Swipe_CurrentyTime += Time.deltaTime;
        }
        if(Rush_CurrentyTime < Rush_CoolTime && !Rush_uising)
        {
            Rush_CurrentyTime += Time.deltaTime;
        }
        if(Pattern_Delay_Chack)
        {
            if(Pattern_Delay_Currenty_CoolTime < Pattern_Delay_CoolTime)
            {
                Pattern_Delay_Currenty_CoolTime += Time.deltaTime;
            }
            else
            {
                Pattern_Delay_Chack = false;
                Skill_using = false;
            }
        }
    }
    private void Skill_start_set()
    {
        Skill_using = true;
        move.Move_Able = false;
    }
    private void Skill_end_set()
    {
        move.Move_Able = true;
        Pattern_Delay_Chack = true;
        Pattern_Delay_Currenty_CoolTime = 0;
    }

    #region Swipe
    //강타
    public void Swipe()
    {
        Swipe_using = true;
        Skill_start_set();
        Swipe_Delay_ing();
    }
    public void Swipe_Delay_ing()
    {
        float time = 60 / (Swipe_Delay * 60);
        animator.SetFloat("Swipe_Waiting_Time", time);
        animator.SetTrigger("SWIPE");
    }
    public void Swipe_HitBox_Rotation()
    {
        float rotateDegree = Mathf.Atan2(target_Pos.position.y - transform.position.y, target_Pos.position.x - transform.position.x) * Mathf.Rad2Deg;
        HitBox_Rotation.rotation = Quaternion.AngleAxis(rotateDegree - 90, Vector3.forward);
    }
    public void Swipe_Hiting()
    {
        for(int i=0;i < swipe_Scen.Player_List.Count;i++)
        {
            if (swipe_Scen.Player_List[i].GetComponent<P_Status>() != null)
            {
                swipe_Scen.Player_List[i].GetComponent<P_Status>().HIT(Swipe_Damage);
            }
            else
            {
                swipe_Scen.Player_List[i].GetComponent<Arm_HP>().Hit(Swipe_Damage);
            }
        }
    }
    public void Swipe_End()
    {
        Swipe_using = false;
        Skill_end_set();
        Swipe_CurrentyTime = 0;
    }
    #endregion

    #region Rush
    //대쉬
    private void Rush()
    {
        Rush_uising = true;
        Skill_start_set();
        animator.SetTrigger("RUSH");
    }
    public void Rush_Start_Delay()
    {
        Rush_Currenty_Speed = Rush_Speed;
        Dir_Calculation();
        Rush_Velocity_Bool = true;
        StartCoroutine(Rush_Speed_Down());
    }
    private void Rush_Velocity_Chack()
    {
        if(Rush_Velocity_Bool)
        {
            transform.Translate(target_Dir * Rush_Currenty_Speed * Time.deltaTime);
        }
    }
    IEnumerator Rush_Speed_Down()
    {
        yield return YieldInstructionCache.WaitForSeconds(Rush_Able_Time);
        for(float i =0;i<=1;i+=0.04f)
        {
            yield return YieldInstructionCache.WaitForSeconds(0.01f);
            Rush_Currenty_Speed = Mathf.Lerp(Rush_Speed, 0, i);
        }
        Rush_End();
    }
    private void Rush_End()
    {
        animator.SetTrigger("RUSH_END");
        Rush_Velocity_Bool = false;
        Rush_uising = false;
        Skill_end_set();
        Rush_CurrentyTime = 0;
    }
    #endregion

    #region Rush_Audio
    public void Rush_Set_Audio()
    {
        _audio.PlayOneShot(Rush_Set_Sound);
    }
    public void Swipe_Hit_Audio()
    {
        _audio.PlayOneShot(Swipe_Hit_Sound);
    }
    #endregion

    #region Calculation
    void Dir_Calculation()
    {
        target_Dir = target_Pos.position - this.transform.position;
        target_Dir = target_Dir.normalized;
    }
    float Degree_TO_Radian(float degree)
    {
        return ((Mathf.PI / 180) * degree);
    }
    float Radian_TO_Degree(float Radian)
    {
        return ((180 / Mathf.PI) * Radian);
    }
    #endregion
}

