using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{

    //EnemyDataBase
    protected Enemy_Database database;
    protected Enemy_Data_List data;
    protected Elite_Enemy_List E_data;
    protected Gamemanager myChar;
    protected Animator anim;
    protected Transform tf;
    protected Collider2D col;
    public enum enemy_type { basic, elite };
    public enemy_type enemy_type_ = enemy_type.basic;

    //Script
    protected Emy_Move move;

    //EXP
    protected EXP_Spawn exp_s;

    //Status
    public int NUMRING;
    [ReadOnly]  public int Level=0;
    [ReadOnly]  public float Hp=0;
    [ReadOnly]  public float Speed=0;
    [ReadOnly]  public float Size=0;
    [ReadOnly]  public float Damage=0;
    [ReadOnly] protected float Exp_Amount;
    private bool Dieing = false;

    //DamageUI
    protected Enemy_HitDamage_UI Damage_UI;

    public float get_HP { get { return Hp; } }
    void Start()
    {
        start_set();
    }
    protected virtual void start_set()
    {
        myChar = Gamemanager.myChar;
        database = myChar.Enemy_database;
        col = GetComponent<Collider2D>();
        move = GetComponent<Emy_Move>();
        exp_s = myChar.EXPS.GetComponent<EXP_Spawn>();
        anim = GetComponent<Animator>();
        Damage_UI = GetComponent<Enemy_HitDamage_UI>();
        tf = this.transform;
        Status_SET(database.Enemy_List[NUMRING]);
    }

    public void Status_SET(Enemy_Data_List _data)
    {
        data = _data;
        Hp = database.basic.HP * data.Hp * 0.01f;
        Speed = database.basic.Speed * data.Speed * 0.01f;
        Damage = database.basic.Damage * data.Damage * 0.01f;
        Exp_Amount = data.Exp_Amount;
        Level_Set();
    }
    public virtual void Status_SET(Elite_Enemy_List _data)
    {
        data = _data;
        E_data = _data;
        Hp = database.basic.HP * data.Hp * 0.01f;
        Speed = database.basic.Speed * data.Speed * 0.01f;
        Damage = database.basic.Damage * data.Damage * 0.01f;
        Exp_Amount = data.Exp_Amount;
        Level_Set();
    }
    public virtual void Status_SET(float _hp,float _speed,float _size,float _damage,int _exp_Amount)
    {
        if(myChar == null)
        {
            start_set();
        }
        Hp += database.basic.HP * _hp * 0.01f;
        Speed += database.basic.Speed * _speed * 0.01f;
        Size += database.basic.Size * _size * 0.01f;
        Damage += database.basic.Damage * _damage * 0.01f;
        Exp_Amount += _exp_Amount;
    }
    public virtual void Status_SET(float _hp, float _speed, float _size, float _damage)
    {
        if (myChar == null)
        {
            start_set();
        }
        Hp += database.basic.HP * _hp * 0.01f;
        Speed += database.basic.Speed * _speed * 0.01f;
        Size += database.basic.Size * _size * 0.01f;
        Damage += database.basic.Damage * _damage * 0.01f;
    }
    private void Level_Set()
    {
        Level = (int)(myChar.CurrentTIMER /60);
        Hp += database.basic.HP * (data.Lv_Hp* Level) * 0.01f;
        Damage += database.basic.Damage * (data.Lv_Damage * Level) * 0.01f;
    }
    public bool Hit(float damage)
    {
        if (damage != 0 && !Dieing)
        {
            Hp -= damage;
            Damage_UI.Damage_UI_Spawn((int)damage);
            if (Hp <= 0)
            {
                Dieing = true;
                col.enabled = false;
                exp_s.Roulette(Exp_Amount ,this.transform.position);
                Speed = 0;
                anim.SetTrigger("DEATH");
                return true;
            }
            else
            {
                anim.SetTrigger("HIT");
            }
        }
        return false;
    }
    public bool Hit(float damage,float Power)
    {
        move.KnockBack(Power);
        return Hit(damage);
    }
    public bool Hit(float damage,float Power,Vector3 pivot)
    {
        move.KnockBack(Power, pivot);
        return Hit(damage);
    }
    public void Enemy_Death()
    {
        Destroy(this.gameObject);
    }
}
