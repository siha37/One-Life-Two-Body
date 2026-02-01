using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TopGameData : MonoBehaviour
{
    private static TopGameData topgamedata = null;

    private void Start()
    {
        if (topgamedata != null)
        {
            if (topgamedata != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public static TopGameData Dataset
    {
        get
        {
            if (topgamedata == null)
            {
                topgamedata = FindObjectOfType(typeof(TopGameData)) as TopGameData;
                if (topgamedata == null)
                {
                    GameObject obj = new GameObject("TopPlayData");
                    topgamedata = obj.AddComponent(typeof(TopGameData)) as TopGameData;
                    DontDestroyOnLoad(obj);
                }
            }
            return topgamedata;
        }
    }

    //Player Status
    const float Start_HP = 100;
    const float Start_Speed = 5;
    const float Start_Energy = 100;
    const float Start_MP = 100;

    #region Level_UP_Status
    static float Lv_UP_HP = 3;
    public float Lv_HP { get { return Lv_UP_HP; }set { Lv_UP_HP = value; } }
    static float Lv_UP_Energy = 3;
    public float Lv_Energy { get { return Lv_UP_Energy; } set { Lv_UP_Energy = value; } }
    static float Lv_UP_MP = 3;
    public float Lv_MP { get { return Lv_UP_MP; } set { Lv_UP_MP = value; } }
    #endregion


    private float damage_p = 100;
    public float Damage_P { get { return damage_p; } set { damage_p = value; } }
    private float speed_p = 100;
    public float Speed_P { get { return speed_p; } set { speed_p = value; } }
    private float size_p;
    public float Size_P { get { return size_p; } set { size_p = value; } }
    private float b_speed = Start_Speed;
/// <summary>
/// 플레이어의 속도
/// </summary>
    public float B_Speed { get { return b_speed; } set { b_speed = Start_Speed+(Start_Speed * value / 100); } }
    private float hp = Start_HP;
    public float HP { get { return hp; } set { if (value != 0) { hp = value; } else { hp = Start_HP; } } }
    private float exp;
    public float EXP { get { return exp; } set { exp = value; } }
    private float engy = Start_Energy;
    public float Energy { get { return engy; } set { engy = value; } }
    private float mn = Start_MP;
    public float Mana { get { return mn; } set { mn = value; } }

    private float Recovery_Time_Amount = 5;
    public float Recovery_Time { get { return Recovery_Time_Amount; } set { Recovery_Time_Amount = value; } }
    private float Energy_Recovery_Amount = 0.05f;
    public float Energy_Recovery { get { return Energy_Recovery_Amount; } set { Energy_Recovery_Amount = Recovery_Amount_set(Energy, Recovery_Time); } }
    private float Mana_Recovery_Amount = 0.05f;
    public float Mana_Recovery { get { return Mana_Recovery_Amount; } set { Mana_Recovery_Amount = Recovery_Amount_set(Mana, Recovery_Time); } }
    public float Resource_Delay = 1;


    public float Dash_Delay_Time = 1.5f;
    public float Dash_Speed = 5;
    public float Dash_Resource_Amount = 15;

    private float Recovery_Amount_set(float Total, float time)
    {
        return Total / time;
    }

    public void Save()
    {

    }
    public void Load()
    {

    }

    //Enemy Status

}

