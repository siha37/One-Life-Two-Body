using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Guided_M_Shot_Projectile : TOP_Projectile
{
    [SerializeField] float Radian_R;
    private void Awake()
    {
        base.BaseData_Input(false);
        Projectile = Resources.Load<GameObject>("Prefab/Projectile/Guided_Missile");
        First_Level_Set(dataBase.Guided_M_P[0]);
    }
    private void Start()
    {
        level_up_.UI_Slot_Creating(this);
    }
    public override void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.Guided_M_P[_level],dataBase.Guided_M_P.Length);
        Per_Speed = dataBase.Guided_M_P[_level].Speed;
        Shot_Delay = dataBase.Guided_M_P[_level].Shot_Delay;
        Destroy_Time_Pjt = dataBase.Guided_M_P[_level].Destroy_Time;
        Projectile_Amount = dataBase.Guided_M_P[_level].Projectile_Amount;
        if (MaxLevel - 1 > _level)
        {
            NextLevel_Ex = dataBase.Guided_M_P[_level + 1].NextLevel_Ex;
        }
        Percent_Data();
        NowTime = Shot_Delay;
    }
    private void Update()
    {
        if(NowTime == -1)
        {

        }
        else if(NowTime > 0)
        {
            NowTime -= Time.deltaTime;
        }
        else if(NowTime <= 0)
        {
            ProjectileCreat();
            NowTime = Shot_Delay;
        }
    }

    public override void ProjectileCreat()
    {
        if (p_Status.Able_Resource_Chack(Resource_TYPE, Resource_Amount))
        {
            base.ProjectileCreat();
            float Degree;
            if (Projectile_Amount > 1)
            {
                Degree = 360 / Projectile_Amount;
            }
            else
            {
                Degree = 0;
            }
            for (int i = 0; i < Projectile_Amount; i++)
            {
                //spawn point set
                float Radian = Degree_TO_Radian(270 + (Degree * i));
                float x = Spawn_Point.position.x + Radian_R * Mathf.Cos(Radian);
                float y = Spawn_Point.position.y + Radian_R * Mathf.Sin(Radian);

                Guided_M_Projectile guided_M_ = Instantiate(Projectile, Spawn_Point.position, Quaternion.identity).GetComponent<Guided_M_Projectile>();
                guided_M_.Shot(BasicSpeed, BasicDamage, Destroy_Time_Pjt, new Vector3(x, y, 0));

            }
        }
    }
    public override bool LEVEL_UP()
    {
        Level_Setting(Level);
        return base.LEVEL_UP();
    }
    float Degree_TO_Radian(float degree)
    {
        return ((Mathf.PI / 180) * degree);
    }
    float Radian_TO_Degree(float Radian)
    {
        return ((180 / Mathf.PI) * Radian);
    }
}
