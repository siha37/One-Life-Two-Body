using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite_Missile_Pattern : MonoBehaviour
{
    Gamemanager myChar;

    Emy_Move move;
    Enemy_Elite_Status status;
    Rigidbody2D rd;
    Transform Player_Pos;
    Animator anim;

    [Space(10)]
    [Header("BasicSet")]
    SpriteRenderer myRenderer;
    [ReadOnly] private bool Skill_uising;
    [ReadOnly] private bool Pattern_Delay_Chack;
    [SerializeField] private float Pattern_Delay_CoolTime;
    [ReadOnly] private float Pattern_Delay_Currenty_CoolTime;
    public Player_Scen player_Scen;

    [Header("Image")]
    [SerializeField] SpriteRenderer Lazzer_Effect;
    [SerializeField] SpriteRenderer Foot_Effect;


    [Space(10)]
    [Header("Lazzer")]
    [SerializeField] Vector3 Target_Dir;
    [SerializeField] Transform Lazzer_Pivot;
    [SerializeField] float Lazzer_Distance;
    [SerializeField] Player_Scen Lazzer_Scen;
    [ReadOnly] bool Lazzer_using;
    float Lazzer_Damage;
    float Lazzer_Delay;
    float Lazzer_CoolTime;
    float Lazzer_Currenty_CoolTime;

    [Space(10)]
    [Header("Missile")]
    [SerializeField] GameObject Missile_Pj;
    [SerializeField] float Missile_Distance;
    [ReadOnly] bool Missile_using;
    float Missile_Damage;
    float Missile_Delay;
    float Missile_CoolTime;
    float Missile_Currenty_CoolTime;
    int Missile_Amount;
    float Missile_Radius;
    [SerializeField]List<Vector3> Missile_Poss = new List<Vector3>();


    private void Start()
    {
        myChar = Gamemanager.myChar;
        move = GetComponent<Emy_Move>();
        status = GetComponent<Enemy_Elite_Status>();
        Player_Pos = myChar.P2.transform;
        rd = GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        Lazzer_Damage = status.Pattern_1_Damage;
        Lazzer_Delay = status.Pattern_1_Delay;
        Lazzer_CoolTime = status.Pattern_1_CoolTime;

        Missile_Damage = status.Pattern_2_Damage;
        Missile_Delay = status.Pattern_2_Delay;
        Missile_CoolTime = status.Pattern_2_CoolTime;
        Missile_Amount = status.Missile_Amount;
        Missile_Radius = status.Missile_Radius;
    }

    private void Update()
    {
        Lazzer_Effect.flipX = myRenderer.flipX;
        Foot_Effect.flipX = myRenderer.flipX;
    }

    private void LateUpdate()
    {
        if (!Skill_uising)
        {
            for(int i=0;i<player_Scen.Player_List.Count;i++)
            {
                float distance = (transform.position - player_Scen.Player_List[i].position).magnitude;
                if (Lazzer_Currenty_CoolTime >= Lazzer_CoolTime && distance < Lazzer_Distance)
                {
                    Lazzer();
                }
                else if (Missile_Currenty_CoolTime >= Missile_CoolTime)
                {
                    Missile();
                }
            }
        }
        CoolTime_Updata();
    }

    #region BasicSkillSet
    //기본적인 스킬 세팅
    private void Skill_start_set()
    {
        Skill_uising = true;
        move.Move_Able = false;
    }
    private void Skill_end_set()
    {
        move.Move_Able = true;
        Pattern_Delay_Chack = true;
        Pattern_Delay_Currenty_CoolTime = 0;
    }

    private void CoolTime_Updata()
    {
        if (Lazzer_Currenty_CoolTime < Lazzer_CoolTime && !Lazzer_using)
        {
            Lazzer_Currenty_CoolTime += Time.deltaTime;
        }
        if (Missile_Currenty_CoolTime < Missile_CoolTime && !Missile_using)
        {
            Missile_Currenty_CoolTime += Time.deltaTime;
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
                Skill_uising = false;
            }
        }
    }
    #endregion

    #region Missile
    //Missile

    /// <summary>
    /// 미사일 패턴 준비 단계
    /// </summary>
    private void Missile()
    {
        Missile_using = true;
        Skill_start_set();
        float time = 60 / (Missile_Delay * 60);
        anim.SetFloat("MissileTime", time);
        anim.SetTrigger("MISSILE");
    }
    /// <summary>
    /// 미사일 발사
    /// </summary>
    public void missile_Shoting()
    {
        Missile_Poss.Clear();
        Missile_Poss.Add(Player_Pos.position);
        for (int i = 0; i < Missile_Amount-1; i++)
        {
            Vector3 target_pos = new Vector3(Random.Range(-Missile_Radius, Missile_Radius), Random.Range(-Missile_Radius, Missile_Radius),0);
            Missile_Poss.Add(Player_Pos.position + target_pos);
        }
        for(int i=0;i<Missile_Poss.Count;i++)
        {
            Elite_Missile_OBJ obj = Instantiate(Missile_Pj, Missile_Poss[i], Quaternion.identity, myChar.BulletCollection.transform).GetComponent<Elite_Missile_OBJ>();
            obj.StartSet(Missile_Delay, Missile_Damage);
        }
    }
    /// <summary>
    /// 미사일을 발사 한 후
    /// </summary>
    public void missile_End()
    {
        Missile_using = false;
        Skill_end_set();
        Missile_Currenty_CoolTime = 0;
    }
    #endregion


    #region Lazzer
    //Lazzer
    private void Lazzer()
    {
        Lazzer_using = true;
        Skill_start_set();
        float time = 60 / (Lazzer_Delay * 60);
        anim.SetFloat("LazzerTime", time);
        anim.SetTrigger("LAZZER");
    }
    public void Lazzer_Ing()
    {
        Lazzer_Pivot.rotation = Dir_Calculation(Player_Pos.position);
    }
    public void Lazzer_Damageing()
    {
        for (int i = 0; i < Lazzer_Scen.Player_List.Count; i++)
        {
            if (Lazzer_Scen.Player_List[i].GetComponent<P_Status>() != null)
            {
                Lazzer_Scen.Player_List[i].GetComponent<P_Status>().HIT(Lazzer_Damage);
            }
            else
            {
                Lazzer_Scen.Player_List[i].GetComponent<Arm_HP>().Hit(Lazzer_Damage);
            }
        }
    }
    public void Lazzer_End()
    {
        Lazzer_using = false;
        Skill_end_set();
        Lazzer_Currenty_CoolTime = 0;
    }
    #endregion
    #region Calculation
    Quaternion Dir_Calculation(Vector3 target)
    {
        float retateDegree = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(retateDegree, Vector3.forward);
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
