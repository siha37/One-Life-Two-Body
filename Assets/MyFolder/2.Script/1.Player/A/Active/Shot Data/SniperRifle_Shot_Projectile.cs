using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SniperRifle_Shot_Projectile : TOP_Projectile
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float viewAngle;
    private GameObject LazerProjectile;

    protected void Awake()
    {
        base.BaseData_Input(true);
        if(cam == null)
        {
            cam = myChar.CinemachinCam;
        }
        EffectNUM = 1;
        Projectile = Resources.Load<GameObject>("Prefab/Projectile/SniperRifle_Pjt");
        LazerProjectile = Resources.Load<GameObject>("Prefab/Projectile/Sniper_Lazzer");
        First_Level_Set(dataBase.Sniper_P[0]);
    }
    private void Start()
    {
        cam.m_Lens.OrthographicSize += cam.m_Lens.OrthographicSize * viewAngle * 0.01f;
        Shot.WeapownList_Add(GetComponent<SniperRifle_Shot_Projectile>(),Skill_UI_Image);
        level_UP.Active_A_Skill.Add(this);
    }

    public override bool LEVEL_UP()
    {
        Level_Setting(Level);
        return base.LEVEL_UP();
    }

    public override void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.Sniper_P[_level],dataBase.Sniper_P.Length);
        Per_Speed = dataBase.Sniper_P[_level].Speed;
        Shot_Delay = dataBase.Sniper_P[_level].Shot_Delay;
        Destroy_Time_Pjt = dataBase.Sniper_P[_level].Destroy_Time;
        Charging_Delay = dataBase.Sniper_P[_level].Charging_Delay;
        Charging_Count = dataBase.Sniper_P[_level].Charging_Count;
        Penetrate_Count = dataBase.Sniper_P[_level].Penetrate_Count;
        if(MaxLevel -1 > _level)
        {
            NextLevel_Ex = dataBase.Sniper_P[_level + 1].NextLevel_Ex;
        }
        Percent_Data();
    }

    public override void Charing_Effect()
    {
    }
    public override void Charging_Projectile_Set(int Bullet_Count)
    {
        if (Bullet_Count == Charging_Count)
        {
            Sound_Manager.AudioPlay(Charging_Sound);
        }
    }

    public override void Data_Input()
    {
        base.Data_Input();
    }
    public override void ProjectileCreat(int _Charging_Count)
    {
        if (_Charging_Count < 4)
        {
            if (!Shot.Enforce.Trun_On_Off)
            {
                P_SniperRifle projectile = Instantiate(Projectile, Spawn_Point.position, Pivot_Rot.rotation, BulletCollection.transform).GetComponent<P_SniperRifle>();
                projectile.Shot(BasicSpeed, BasicDamage, Destroy_Time_Pjt, Penetrate_Count + _Charging_Count);
                Sound_Manager.AudioPlay(Shoting_Sound[0]);
            }
            else
            {
                P_SniperRifle projectile = Instantiate(Projectile, Spawn_Point.position, Pivot_Rot.rotation, BulletCollection.transform).GetComponent<P_SniperRifle>();
                projectile.Shot(BasicSpeed, UP_function.UP_Damage(BasicDamage, UP_Damage), Destroy_Time_Pjt, Penetrate_Count + _Charging_Count);
                Sound_Manager.AudioPlay(Shoting_Sound[0]);
            }
        }
        else
        {
            if (!Shot.Enforce.Trun_On_Off)
            {
                P_LazzerSniper projectile = Instantiate(LazerProjectile, Spawn_Point.position, Pivot_Rot.rotation, BulletCollection.transform).GetComponent<P_LazzerSniper>();
                projectile.Shot(BasicDamage);
                Sound_Manager.AudioPlay(Shoting_Sound[1]);
            }
            else
            {
                P_LazzerSniper projectile = Instantiate(LazerProjectile, Spawn_Point.position, Pivot_Rot.rotation, BulletCollection.transform).GetComponent<P_LazzerSniper>();
                projectile.Shot(UP_function.UP_Damage(BasicDamage, UP_Damage),UP_Size); 
                Sound_Manager.AudioPlay(Shoting_Sound[1]);
            }
        }
    }
}
