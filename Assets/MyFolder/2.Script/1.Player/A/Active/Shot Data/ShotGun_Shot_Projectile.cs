using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun_Shot_Projectile : TOP_Projectile
{

    private float Angle;
    List<float> angles = new List<float>();

    private void Awake()
    {
        base.BaseData_Input(true);
        Projectile = Resources.Load<GameObject>("Prefab/Projectile/ShotGun_Pjt");
        First_Level_Set(dataBase.ShotGun_P[0]);
    }
    private void Start()
    {
        Shot.WeapownList_Add(GetComponent<ShotGun_Shot_Projectile>(),Skill_UI_Image);
        level_UP.Active_A_Skill.Add(this);
    }


    public override bool LEVEL_UP()
    {
        Level_Setting(Level);
        return base.LEVEL_UP();
    }

    public override void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.ShotGun_P[_level], dataBase.ShotGun_P.Length);
        Per_Speed = dataBase.ShotGun_P[_level].Speed;
        Shot_Delay = dataBase.ShotGun_P[_level].Shot_Delay;
        Destroy_Time_Pjt = dataBase.ShotGun_P[_level].Destory_Time;
        Projectile_Amount = dataBase.ShotGun_P[_level].Projectile_Amount;
        Angle = dataBase.ShotGun_P[_level].Angle;
        if (MaxLevel - 1 > _level)
        {
            NextLevel_Ex = dataBase.ShotGun_P[_level + 1].NextLevel_Ex;
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
            angles.Add(Angle);
            angles.Add(-Angle);
            for (int i = 0; i < Projectile_Amount - 2; i++)
            {
                angles.Add(Random.Range(-Angle, Angle));
            }
            for (int i = 0; i < Projectile_Amount; i++)
            {
                Vector3 Rot = Pivot_Rot.eulerAngles;
                Rot += new Vector3(0, 0, angles[i]);
                P_ShotGun p_s = Instantiate(Projectile, Spawn_Point.position, Quaternion.Euler(Rot), BulletCollection.transform).GetComponent<P_ShotGun>();
                p_s.Shot(BasicSpeed, BasicDamage, Destroy_Time_Pjt);
            }
            Sound_Manager.AudioPlay(Shoting_Sound[0]);
        }
        else
        {
            angles.Add(Angle);
            angles.Add(-Angle);
            for (int i = 0; i < UP_function.UP_ProjectileAmount(Projectile_Amount,UP_ProjectileAmount) - 2; i++)
            {
                angles.Add(Random.Range(-Angle, Angle));
            }
            for (int i = 0; i < UP_function.UP_ProjectileAmount(Projectile_Amount, UP_ProjectileAmount); i++)
            {
                Vector3 Rot = Pivot_Rot.eulerAngles;
                Rot += new Vector3(0, 0, angles[i]);
                P_ShotGun p_s = Instantiate(Projectile, Spawn_Point.position, Quaternion.Euler(Rot), BulletCollection.transform).GetComponent<P_ShotGun>();
                p_s.Shot(UP_function.UP_Speed(BasicSpeed,UP_Speed), UP_function.UP_Damage(BasicDamage,UP_Damage), Destroy_Time_Pjt);
                p_s.transform.localScale = UP_function.UP_Size(p_s.transform.localScale,UP_Size);
            }
            Sound_Manager.AudioPlay(Shoting_Sound[0]);
        }
    }
}
