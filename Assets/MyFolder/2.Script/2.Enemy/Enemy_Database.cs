using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class Enemy_BasicStatus
{
    public float HP;
    public float Speed;
    public float Size;
    public float Damage;
}
[System.Serializable]
public class Enemy_Data_List
{
    public int Num;
    public float Hp;
    public float Speed;
    public float Damage;
    public float Lv_Hp;
    public float Lv_Damage;
    public int Exp_Amount;
}

[System.Serializable]
public class Elite_Enemy_List: Enemy_Data_List
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
}
public class Enemy_Database : MonoBehaviour
{
    public Enemy_BasicStatus basic;
    public Enemy_Data_List[] Enemy_List;
    public Elite_Enemy_List[] Elite_Enemy_List;
    Gamemanager myChar;

    private void Awake()
    {
        myChar = Gamemanager.myChar;
        myChar.Enemy_database = this;

        List<Dictionary<string, object>> Dic = CSVReader.Read("Data/ENEMY/Enemy_BasicStatus");
        string j = JsonConvert.SerializeObject(Dic); 
        Enemy_BasicStatus[] temp = JsonConvert.DeserializeObject<Enemy_BasicStatus[]>(j);
        basic = temp[0];

        Dic = CSVReader.Read("Data/ENEMY/Enemy_DataList");
        j = JsonConvert.SerializeObject(Dic);
        Enemy_List = JsonConvert.DeserializeObject<Enemy_Data_List[]>(j);

        Dic = CSVReader.Read("Data/ENEMY/Elite_Enemy_DataList");
        j = JsonConvert.SerializeObject(Dic);
        Elite_Enemy_List = JsonConvert.DeserializeObject<Elite_Enemy_List[]>(j);
    }
}
