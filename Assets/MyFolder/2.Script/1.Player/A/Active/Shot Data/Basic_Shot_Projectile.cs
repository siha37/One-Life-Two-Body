using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Shot_Projectile : TOP_Projectile
{

    [SerializeField] private float Radian_R;
    [SerializeField] private List<P_Basic> bullet = new List<P_Basic> ();

    private void Awake()
    {
        base.BaseData_Input(true);
        EffectNUM = 1;
        Projectile = Resources.Load<GameObject>("Prefab/Projectile/Basic_Pjt");
        First_Level_Set(dataBase.Bascic_P[0]);
    }

    void Start()
    {
        Shot.WeapownList_Add(GetComponent<Basic_Shot_Projectile>(),Skill_UI_Image);
        level_UP.Active_A_Skill.Add(this);
    }

    public override bool LEVEL_UP()
    {
        Level_Setting(Level);
        return base.LEVEL_UP();
    }

    
    public override void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.Bascic_P[_level],dataBase.Bascic_P.Length);
        Per_Speed = dataBase.Bascic_P[_level].Speed;
        Shot_Delay = dataBase.Bascic_P[_level].Shot_Delay;
        Charging_Delay = dataBase.Bascic_P[_level].Charging_Delay;
        Charging_Count = dataBase.Bascic_P[_level].Charging_Count;
        Destroy_Time_Pjt = dataBase.Bascic_P[_level].Destroy_Time;
        Penetrate_Count = dataBase.Bascic_P[_level].Penetrate_Count;
        Projectile_Amount = dataBase.Bascic_P[_level].Projectile_Amount;
        if (MaxLevel - 1 > _level)
        {
            NextLevel_Ex = dataBase.Bascic_P[_level + 1].NextLevel_Ex;
        }
        Percent_Data();
    }

    public override void Data_Input()
    {
        base.Data_Input();
    }

    public override void ProjectileCreat(int _Charging_Count)
    {
        if (_Charging_Count != 0)
        {
            if (!Shot.Enforce.Trun_On_Off)
            {
                for (int i = 0; i < bullet.Count; i++)
                {
                    bullet[i].Shot(BasicSpeed, BasicDamage, Destroy_Time_Pjt);
                    bullet[i].Waiting_Change(false);
                    bullet[i].EffectStop();
                }
            }
            else
            {
                for (int i = 0; i < bullet.Count; i++)
                {
                    bullet[i].transform.localScale = UP_function.UP_Size(bullet[i].transform.localScale, UP_Size);
                    bullet[i].Shot(UP_function.UP_Speed(BasicSpeed, UP_Speed), UP_function.UP_Damage(BasicDamage, UP_Damage), Destroy_Time_Pjt);
                    bullet[i].Waiting_Change(false);
                    bullet[i].EffectStop();
                }
            } 
        }
        else
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            float rotateDegree = Mathf.Atan2(mouse.y - bullet[0].transform.position.y, mouse.x - bullet[0].transform.position.x) * Mathf.Rad2Deg;
            bullet[0].transform.rotation = Quaternion.AngleAxis(rotateDegree - 90, Vector3.forward);
            bullet[0].Shot(BasicSpeed, BasicDamage, Destroy_Time_Pjt);
            bullet[0].Waiting_Change(false);
            bullet[0].EffectStop();
        }
        Sound_Manager.AudioPlay(Shoting_Sound[0]);
        bullet.Clear();
    }

    public override void Charging_Projectile_Set(int Bullet_Count)
    {
        float Degree = 360/ Charging_Count;
        float Radian = Degree_TO_Radian(270 + (Degree * Bullet_Count));
        float x = Pivot_Rot.position.x + Radian_R * Mathf.Cos(Radian);
        float y = Pivot_Rot.position.y + Radian_R * Mathf.Sin(Radian);

        bullet.Add(Instantiate(Projectile,new Vector3(x,y,0),Quaternion.identity,myChar.P2.transform).GetComponent<P_Basic>());
        bullet[bullet.Count-1].Waiting_Change(true);
        Sound_Manager.AudioPlay(Charging_Sound);
        if (Bullet_Count == Charging_Count-1)
        {
            for(int i=0;i< bullet.Count; i++)
            {
                bullet[i].EffectPlay();
            }
        }
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
