using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBoom_Shot_Projectile : TOP_Projectile
{

    public float Radius;
    private void Awake()
    {
        base.BaseData_Input(true);
        Projectile = Resources.Load<GameObject>("Prefab/Projectile/D_Boom");
        First_Level_Set(dataBase.DelayBoom_P[0]);
    }
    void Start()
    {
        Shot.WeapownList_Add(GetComponent<DelayBoom_Shot_Projectile>(),Skill_UI_Image);
        level_UP.Active_A_Skill.Add(this);
    }

    public override bool LEVEL_UP()
    {
        Level_Setting(Level);
        return base.LEVEL_UP();
    }


    public override void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.DelayBoom_P[_level],dataBase.Bezier_L_P.Length);
        Per_Speed = dataBase.DelayBoom_P[_level].Speed;
        Radius = dataBase.DelayBoom_P[_level].Radius;
        Shot_Delay = dataBase.DelayBoom_P[_level].Shot_Delay;
        Charging_Delay = dataBase.DelayBoom_P[_level].Charging_Delay;
        Charging_Count = dataBase.DelayBoom_P[_level].Charging_Count;
        Destroy_Time_Pjt = dataBase.DelayBoom_P[_level].Destroy_Time;
        Projectile_Amount = dataBase.DelayBoom_P[_level].Projectile_Amount;
        KnockBack_Power = dataBase.DelayBoom_P[_level].KnockBack_Power;
        if (MaxLevel - 1 > _level)
        {
            NextLevel_Ex = dataBase.DelayBoom_P[_level + 1].NextLevel_Ex;
        }
        Percent_Data();
    }

    public override void Data_Input()
    {
        base.Data_Input();
    }
    public override void ProjectileCreat(int _Charging_Count)
    {
        StartCoroutine("Pjt_Creats",_Charging_Count);
    }
    IEnumerator Pjt_Creats(int _Charging_Count)
    {
        if(!Shot.Enforce.Trun_On_Off)
        {
            int Amount = _Charging_Count + Projectile_Amount;
            for (int i = 0; i < Amount; i++)
            {
                Sound_Manager.AudioPlay(Shoting_Sound[0]);
                Vector3 pivot = Pivot_Rot.rotation.eulerAngles;
                Quaternion Rendom_Q = Quaternion.Euler(pivot.x, pivot.y, pivot.z + Random.Range(-20, 20));
                P_DelayBoom projectile = Instantiate(Projectile, Spawn_Point.position, Rendom_Q, BulletCollection.transform).GetComponent<P_DelayBoom>();
                projectile.Shot(BasicSpeed, BasicDamage, Radius, Destroy_Time_Pjt, KnockBack_Power);

                yield return YieldInstructionCache.WaitForSeconds(0.2f);
            }
        }
        else
        {
            int Amount = _Charging_Count + UP_function.UP_ProjectileAmount(Projectile_Amount,UP_ProjectileAmount);
            for (int i = 0; i < Amount; i++)
            {
                Sound_Manager.AudioPlay(Shoting_Sound[0]);
                Vector3 pivot = Pivot_Rot.rotation.eulerAngles;
                Quaternion Rendom_Q = Quaternion.Euler(pivot.x, pivot.y, pivot.z + Random.Range(-20, 20));
                P_DelayBoom projectile = Instantiate(Projectile, Spawn_Point.position, Rendom_Q, BulletCollection.transform).GetComponent<P_DelayBoom>();
                projectile.Shot(BasicSpeed, UP_function.UP_Damage(BasicDamage, UP_Damage), UP_function.UP_Size(Radius, UP_Size), Destroy_Time_Pjt, KnockBack_Power);

                yield return YieldInstructionCache.WaitForSeconds(0.2f);
            }
        }
    }
}
