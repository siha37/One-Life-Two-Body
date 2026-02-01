using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firend_Sword : TOP_Projectile
{
    [SerializeField] Arm_HP p2_hp;
    [SerializeField] RopePysics rope;
    [SerializeField] Arm_Shot shot;

    private int Segment_Count;

    public float Shot_MaxTime;
    private void Awake()
    {
        base.BaseData_Input(false);
        First_Level_Set(dataBase.firend_Sword[0]);
    }

    private void Start()
    {
        level_up_.UI_Slot_Creating(this);
    }
    public override bool LEVEL_UP()
    {
        Level_Setting(Level);
        return base.LEVEL_UP();
    }
    public override void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.firend_Sword[_level],dataBase.firend_Sword.Length);
        Segment_Count = dataBase.firend_Sword[_level].Segment_Count;
        Shot_MaxTime = dataBase.firend_Sword[_level].Shot_MaxTime;
        Per_Damage = dataBase.firend_Sword[_level].Damage;
        Shot_Delay = dataBase.firend_Sword[_level].Shot_Delay;
        KnockBack_Power = dataBase.firend_Sword[_level].KnockBack_Power;
        if(MaxLevel-1>_level)
        {
            NextLevel_Ex = dataBase.firend_Sword[_level+1].NextLevel_Ex;
        }
        Percent_Data();
        NowTime = Shot_Delay;
    }
    private void Update()
    {
        if(!Shoting)
        {
            if (NowTime > 0)
            {
                NowTime -= Time.deltaTime;
            }
            if (NowTime <= 0)
            {
                ProjectileCreat();
                NowTime = Shot_Delay;
            }
        }
    }
    public override void ProjectileCreat()
    {
        Shoting = true;
        StartCoroutine(Shot_maintain());
        p2_hp.NO_HIT = true;
        p2_hp.Attack_Able = true;
        p2_hp.Attack_Damage = BasicDamage;
        p2_hp.KnockBack_Power = KnockBack_Power;
        rope.PysicsON = true;
        rope.StartSet(Segment_Count);
        //shot.SHOT_ONOFF = false;
    }

    private void Shot_End()
    {
        p2_hp.NO_HIT = false;
        p2_hp.Attack_Able = false;
        p2_hp.invincibility(1);
        rope.PysicsON = false;
        rope.StartSet();
        //shot.SHOT_ONOFF = true;
        Shoting = false;
    }
    IEnumerator Shot_maintain()
    {
        yield return YieldInstructionCache.WaitForSeconds(Shot_MaxTime);
        Shot_End();
    }

}
