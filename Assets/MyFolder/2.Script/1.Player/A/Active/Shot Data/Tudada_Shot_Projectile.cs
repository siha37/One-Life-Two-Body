using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tudada_Shot_Projectile : TOP_Projectile
{
    private float time = 0;
    [SerializeField] private float X_distance;
    [SerializeField] private float bullet_spread_angle = 3;
    private void Awake()
    {
        base.BaseData_Input(true);
        EffectNUM = 2;
        Projectile = Resources.Load<GameObject>("Prefab/Projectile/ShotGun_Pjt");
        First_Level_Set(dataBase.Tudada_P[0]);
    }

    private void Start()
    {
        Shot.WeapownList_Add(GetComponent<Tudada_Shot_Projectile>(),Skill_UI_Image);
        level_UP.Active_A_Skill.Add(this);
    }

    public override bool LEVEL_UP()
    {
        Level_Setting(Level);
        return base.LEVEL_UP();
    }
    public override void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.Tudada_P[_level],dataBase.Tudada_P.Length);
        Per_Speed = dataBase.Tudada_P[_level].Speed;
        Shot_Delay = dataBase.Tudada_P[_level].Shot_Delay;
        Destroy_Time_Pjt = dataBase.Tudada_P[_level].Destory_Time;
        Projectile_Amount = dataBase.Tudada_P[_level].Projectile_Amount;
        if(MaxLevel -1 > _level)
        {
            NextLevel_Ex = dataBase.Tudada_P[_level + 1].NextLevel_Ex;
        }
        Percent_Data();
    }

    private void Update()
    {
        if(Shoting)
        {
            //총알 한발 발사 당 시간 간격
            float interval;
            if (!Shot.Enforce.Trun_On_Off)
            {
                interval = 1.0f / (float)Projectile_Amount;
            }
            else
            {
                interval = 1.0f / (float)(UP_function.UP_ProjectileAmount(Projectile_Amount, UP_ProjectileAmount));
            }

            //총알 발사 조건문
            if(time >= interval)
            {
                time = 0;
                Sound_Manager.AudioPlay_One(Shoting_Sound[0]);
                BulletCreate();
            }
            //시간 도달 미달시
            else
            {
                time += Time.deltaTime;
            }
        }

    }

    public override void Data_Input()
    {
        base.Data_Input();
    }
    public override void ProjectileCreat()
    {
        Shoting = true;
        time = 0;
    }
    private void BulletCreate()
    {
        float Cal_Y;
        if (!Shot.Enforce.Trun_On_Off)
        {
            Cal_Y = Random.Range(-X_distance, X_distance);
            Vector3 lpos = Spawn_Point.localPosition + new Vector3(Cal_Y, 0, 0);
            Vector3 point = Pivot_Rot.TransformPoint(lpos);
            Vector3 rot = Pivot_Rot.rotation.eulerAngles + new Vector3(0, 0, Random.Range(-bullet_spread_angle, bullet_spread_angle));
            P_ShotGun shot = Instantiate(Projectile, point,Quaternion.Euler(rot), BulletCollection.transform).GetComponent<P_ShotGun>();
            shot.Shot(BasicSpeed,BasicDamage,Destroy_Time_Pjt,false);
        }
        else
        {
            Cal_Y = Random.Range(-X_distance + (X_distance * UP_Size * 0.01f), X_distance + (X_distance * UP_Size * 0.01f));
            Vector3 lpos = Spawn_Point.localPosition + new Vector3(Cal_Y, 0, 0);
            Vector3 point = Pivot_Rot.TransformPoint(lpos);
            Vector3 rot = Pivot_Rot.rotation.eulerAngles + new Vector3(0, 0, Random.Range(-bullet_spread_angle, bullet_spread_angle));
            P_ShotGun shot = Instantiate(Projectile, point,  Quaternion.Euler(rot), BulletCollection.transform).GetComponent<P_ShotGun>();
            shot.Shot(UP_function.UP_Speed(BasicSpeed,UP_Speed),UP_function.UP_Damage(BasicDamage,UP_Damage),Destroy_Time_Pjt,false);

        }
    }

    public override void StopShot()
    {
        Shoting = false;
        time = 0;
    }
}
