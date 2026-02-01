using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild_Damage_Buff : TOP_Projectile
{
    [SerializeField] P_Shild shild;
     public bool Able;
    private float Able_time;
    private float Currenty_Able_time;
    
    private void Awake()
    {
        base.BaseData_Input(false);
        First_Level_Set(dataBase.Shild_D[0]);
    }
    private void Start()
    {
        level_up_.UI_Slot_Creating(this);
    }
    private void LateUpdate()
    {
        if(!Able)
        {
            if (NowTime > 0)
            {
                NowTime -= Time.deltaTime;
            }
            if (NowTime <= 0)
            {
                Currenty_Able_time = Able_time;
                Able = true;
                NowTime = Shot_Delay;
            }
        }
        else
        {
            if (Currenty_Able_time > 0)
            {
                Currenty_Able_time -= Time.deltaTime;
            }
            else if(Currenty_Able_time <= 0)
            {
                Able = false;
            }
        }
    }
    public override bool LEVEL_UP()
    {
        if (Level == 0)
        {
            //shild.damage_Buff = GetComponent<Shild_Damage_Buff>();
        }
        Level_Setting(Level);
        return base.LEVEL_UP();
    }
    public override void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.Shild_D[_level], dataBase.Shild_D.Length);
        BasicDamage = dataBase.Shild_D[_level].Damage;
        Able_time = dataBase.Shild_D[_level].Able_Time;
        Shot_Delay = dataBase.Shild_D[_level].Shot_Delay;
        if (MaxLevel - 1 > _level)
        {
            NextLevel_Ex = dataBase.SlowArea_P[_level + 1].NextLevel_Ex;
        }
    }
    public void Active_Buff(BulletBasic bullet)
    {
        if(p_Status.Able_Resource_Chack(Resource_TYPE,Resource_Amount) && bullet!= null)
        {
            bullet.damage *= BasicDamage;
        }
    }
}
