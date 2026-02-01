using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Shot_Fllow : TOP_Projectile
{
    private float Shot_MaxTime;
    private P_Sowrd NowProjectile;
    private bool Self_Stop;
    private void Awake()
    {
        base.BaseData_Input(true);
        Projectile = Resources.Load<GameObject>("Prefab/Projectile/Sowrd_Pjt");
        First_Level_Set(dataBase.Sowrd_P[0]);
    }
    private void Start()
    {
        Shot.WeapownList_Add(GetComponent<Sword_Shot_Fllow>(), Skill_UI_Image);
    }

    public override bool LEVEL_UP()
    {
        Level_Setting(Level);
        return base.LEVEL_UP();
    }

    public override void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.Sowrd_P[_level],dataBase.Sowrd_P.Length);
        Per_Speed = dataBase.Sowrd_P[_level].Speed;
        Shot_Delay = dataBase.Sowrd_P[_level].Shot_Delay;
        Shot_MaxTime = dataBase.Sowrd_P[_level].Shot_MaxTime; 
        if (MaxLevel - 1 > _level)
        {
            NextLevel_Ex = dataBase.Sowrd_P[_level + 1].NextLevel_Ex;
        }
        Percent_Data();
    }
    public override void Data_Input()
    {
        base.Data_Input();
    }

    public override void ProjectileCreat()
    {
        if(!Shot.Enforce.Trun_On_Off)
        {
            NowProjectile = Instantiate(Projectile, Spawn_Point.position, Pivot_Rot.rotation, BulletCollection.transform).GetComponent<P_Sowrd>();
            NowProjectile.Shot(BasicDamage, BasicSpeed);
        }
        else
        {
            NowProjectile = Instantiate(Projectile, Spawn_Point.position, Pivot_Rot.rotation, BulletCollection.transform).GetComponent<P_Sowrd>();
            NowProjectile.transform.localScale = UP_function.UP_Size(Vector3.one,UP_Size);
            NowProjectile.Shot(UP_function.UP_Damage(BasicDamage,UP_Damage), UP_function.UP_Speed(BasicSpeed, UP_Speed));
        }
        Self_Stop = true;
        Shoting = true;
        StartCoroutine("Auto_Stop");
    }
    override public void StopShot()
    {
        //광역 공격 실시
        if (NowProjectile != null) 
        {
            if (Self_Stop)
            {
                NowProjectile.EndHitting();
            }
            else
            {
                NowProjectile.JustEnd();
            }
        }
        Shoting = false;
        NowProjectile = null;
        StopCoroutine("Auto_Stop");
    }

    IEnumerator Auto_Stop()
    {
        yield return YieldInstructionCache.WaitForSeconds(Shot_MaxTime);
        Self_Stop = false;
        Shot.AutoStopControll();
    }
}
