using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class Basic_DataBase
{
    public int Level;
    public string Name;
    public float Speed;
    public int Shot_Type;
    public int Resource_TYPE;
    public float Resource_Amount;
    public float Shot_Delay;
    public float Damage;
    public int Penetrate_Count;
    public string NextLevel_Ex;
    public float KnockBack_Power;
    public float UP_Damage;
    public float UP_Speed;
    public float UP_ProjectileAmount;
    public float UP_Size;
    public float UP_Penetrate_Count;
    public float UP_Distance;
}

//P1
public class Basic_Projectile_DataBase: Basic_DataBase
{
    //BASIC
    public float Charging_Delay;
    public int Charging_Count;
    public float Destroy_Time;
    public int Projectile_Amount;
}


public class BezierLazer_Projectile_DataBase: Basic_DataBase
{
    public float MaxDistance;
    public float Line_Widht;
    public float Shot_MaxTime;
}

public class DelayBoom_Projectile_DataBase: Basic_DataBase
{
    public float Radius;
    public float Charging_Delay;
    public int Charging_Count;
    public float Destroy_Time;
    public int Projectile_Amount;
}

public class ShotGun_Projectile_DataBase : Basic_DataBase
{
    public float Angle;
    public float Destory_Time;
    public int Projectile_Amount;

}

public class Tudada_Projetile_DataBase: Basic_DataBase
{
    public float Destory_Time;
    //초당 총알 개수
    public int Projectile_Amount;
}

public class SinperRifle_Projectile_DataBase : Basic_DataBase
{
    public float Charging_Delay;
    public int Charging_Count;
    public float Destroy_Time;
}

public class Sowrd_Fllow_Projectile_DataBase : Basic_DataBase
{
    public float Shot_MaxTime;
}

//P1
public class SlowArea_Passive_DataBase: Basic_DataBase
{
    public float Shot_MaxTime;
    public float Radius;
}

public class Guided_Missile_Projectile_DataBase : Basic_DataBase
{
    public float Destroy_Time;
    public int Projectile_Amount;
}

public class Shild_Damage_Buff_DataBase : Basic_DataBase
{
    public float Able_Time;
}

public class Firend_Sword_DataBase : Basic_DataBase
{
    public float Shot_MaxTime;
    public int Segment_Count;
}

public class BlackHoleArea_Passive_DataBase : Basic_DataBase
{
    public float Shot_MaxTime;
    public float Radius;
    public int Projectile_Amount;
}

public class Projectile_Data_SAVE_LOAD : MonoBehaviour
{
    //Active
    public Basic_Projectile_DataBase[] Bascic_P;
    public BezierLazer_Projectile_DataBase[] Bezier_L_P;
    public DelayBoom_Projectile_DataBase[] DelayBoom_P;
    public ShotGun_Projectile_DataBase[] ShotGun_P;
    public Tudada_Projetile_DataBase[] Tudada_P;
    public SinperRifle_Projectile_DataBase[] Sniper_P;
    public Sowrd_Fllow_Projectile_DataBase[] Sowrd_P;

    //Passive
    public Guided_Missile_Projectile_DataBase[] Guided_M_P;
    public SlowArea_Passive_DataBase[] SlowArea_P;
    public Shild_Damage_Buff_DataBase[] Shild_D;
    public Firend_Sword_DataBase[] firend_Sword;
    public BlackHoleArea_Passive_DataBase[] BlackHoleArea_P;
    private void Awake()
    {
        DataLoad();
        //SAVE();
        //LOAD();
    }

    void DataLoad()
    {
        //Active

        List<Dictionary<string, object>> Dic = CSVReader.Read("Data/SKILL/P2/Basic_P_data");
        string j = JsonConvert.SerializeObject(Dic);
        Bascic_P = JsonConvert.DeserializeObject<Basic_Projectile_DataBase[]>(j);

        Dic = CSVReader.Read("Data/SKILL/P2/Bezier_L_data");
        j = JsonConvert.SerializeObject(Dic);
        Bezier_L_P = JsonConvert.DeserializeObject<BezierLazer_Projectile_DataBase[]>(j);

        Dic = CSVReader.Read("Data/SKILL/P2/Delay_B_data");
        j = JsonConvert.SerializeObject(Dic);
        DelayBoom_P = JsonConvert.DeserializeObject<DelayBoom_Projectile_DataBase[]>(j);

        Dic = CSVReader.Read("Data/SKILL/P2/ShotGun_P_data");
        j = JsonConvert.SerializeObject(Dic);
        ShotGun_P = JsonConvert.DeserializeObject<ShotGun_Projectile_DataBase[]>(j);

        Dic = CSVReader.Read("Data/SKILL/P2/Tudada_P_data");
        j = JsonConvert.SerializeObject(Dic);
        Tudada_P = JsonConvert.DeserializeObject<Tudada_Projetile_DataBase[]>(j);

        Dic = CSVReader.Read("Data/SKILL/P2/SniperRifle_P_data");
        j = JsonConvert.SerializeObject(Dic);
        Sniper_P = JsonConvert.DeserializeObject<SinperRifle_Projectile_DataBase[]>(j);

        Dic = CSVReader.Read("Data/SKILL/P2/Sowrd_F_data");
        j = JsonConvert.SerializeObject(Dic);
        Sowrd_P = JsonConvert.DeserializeObject<Sowrd_Fllow_Projectile_DataBase[]>(j);

        //Passive

        Dic = CSVReader.Read("Data/SKILL/P1/Guided_M_data");
        j = JsonConvert.SerializeObject(Dic);
        Guided_M_P = JsonConvert.DeserializeObject<Guided_Missile_Projectile_DataBase[]>(j);

        Dic = CSVReader.Read("Data/SKILL/P1/Slow_A_data");
        j = JsonConvert.SerializeObject(Dic);
        SlowArea_P = JsonConvert.DeserializeObject<SlowArea_Passive_DataBase[]>(j);
        
        Dic = CSVReader.Read("Data/SKILL/P1/Shild_D_data");
        j = JsonConvert.SerializeObject(Dic);
        Shild_D = JsonConvert.DeserializeObject<Shild_Damage_Buff_DataBase[]>(j);

        Dic = CSVReader.Read("Data/SKILL/P1/Firend_Sword_data");
        j = JsonConvert.SerializeObject(Dic);
        firend_Sword = JsonConvert.DeserializeObject<Firend_Sword_DataBase[]>(j);

        Dic = CSVReader.Read("Data/SKILL/P1/BlackHole_A_data");
        j = JsonConvert.SerializeObject(Dic);
        BlackHoleArea_P = JsonConvert.DeserializeObject<BlackHoleArea_Passive_DataBase[]>(j);
    }
}

