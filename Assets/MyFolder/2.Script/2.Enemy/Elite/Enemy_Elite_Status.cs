using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Elite_Status : Enemy_Status
{
    public float Pattern_1_Damage;
    public float Pattern_1_CoolTime;
    public float Pattern_1_Delay;
    public float Pattern_2_Damage;
    public float Pattern_2_CoolTime;
    public float Pattern_2_Delay;
    public float Rush_Speed;
    public int Missile_Amount;
    public float Missile_Size;
    public float Missile_Radius;
    public float Lazzer_Size;


    protected override void start_set()
    {
        myChar = Gamemanager.myChar;
        database = myChar.Enemy_database;
        col = GetComponent<Collider2D>();
        move = GetComponent<Emy_Move>();
        exp_s = myChar.EXPS.GetComponent<EXP_Spawn>();
        anim = GetComponent<Animator>();
        Damage_UI = GetComponent<Enemy_HitDamage_UI>();
        tf = this.transform;
        Status_SET(database.Elite_Enemy_List[NUMRING]);
    }

    public override void Status_SET(Elite_Enemy_List _data)
    {
        base.Status_SET(_data);
        Pattern_1_Damage = database.basic.Damage * E_data.Pattern_1_Damage * 0.01f;
        Pattern_1_CoolTime = E_data.Pattern_1_CoolTime;
        Pattern_1_Delay = E_data.Pattern_1_Delay;
        Pattern_2_Damage = database.basic.Damage * E_data.Pattern_2_Damage * 0.01f;
        Pattern_2_CoolTime = E_data.Pattern_2_CoolTime;
        Pattern_2_Delay = E_data.Pattern_2_Delay;
        Rush_Speed =  E_data.Rush_Speed;
        Missile_Amount = E_data.Missile_Amount;
        Missile_Size = E_data.Missile_Size;
        Missile_Radius = E_data .Missile_Radius;
        Lazzer_Size = E_data.Lazzer_Size;
    }

}
