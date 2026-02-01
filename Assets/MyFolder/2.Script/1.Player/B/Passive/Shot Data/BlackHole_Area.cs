using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_Area : TOP_Projectile
{
    [SerializeField] float Power;
    [SerializeField] float Radius;
    [SerializeField] float Spawn_Distance;
    [SerializeField] float BlackHole_Start_Delay = 1;

    BlackHole_OBJ blackhole_obj;
    private void Awake()
    {
        base.BaseData_Input(false);
        Projectile = Resources.Load<GameObject>("Prefab/Projectile/BlackHole");
        First_Level_Set(dataBase.BlackHoleArea_P[0]);
    }
    private void Start()
    {
        level_up_.UI_Slot_Creating(this);
    }

    public override bool LEVEL_UP()
    {
        Level_Setting(Level);
        return base.LEVEL_UP();
    }

    public override void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.BlackHoleArea_P[_level],dataBase.BlackHoleArea_P.Length);
        Power = dataBase.BlackHoleArea_P[_level].Damage;
        Destroy_Time_Pjt = dataBase.BlackHoleArea_P[_level].Shot_MaxTime;
        Shot_Delay = dataBase.BlackHoleArea_P[_level].Shot_Delay;
        Radius = dataBase.BlackHoleArea_P [_level].Radius;
        Projectile_Amount = dataBase.BlackHoleArea_P[_level].Projectile_Amount;
        if (MaxLevel-1> _level)
        {
            NextLevel_Ex = dataBase.BlackHoleArea_P[_level + 1].NextLevel_Ex;
        }
        NowTime = Shot_Delay;
    }
    private void Update()
    {
        if (blackhole_obj == null)
        {
            if (NowTime > 0)
            {
                NowTime -= Time.deltaTime;
            }
            if(NowTime <=0 )
            {
                ProjectileCreat();
                NowTime = Shot_Delay;
            }
        }
    }

    public override void ProjectileCreat()
    {
        if(p_Status.Able_Resource_Chack(Resource_TYPE,Resource_Amount))
        {
            base.ProjectileCreat();
            for(int i=0;i<Projectile_Amount;i++)
            {
                float x = Random.Range(0, Spawn_Distance);
                float y = Random.Range(0, Spawn_Distance);
                //+new Vector3(x, y, 0)
                blackhole_obj =Instantiate(Projectile,transform.position + new Vector3(x, y, 0),Quaternion.identity,myChar.BulletCollection.transform).GetComponent<BlackHole_OBJ>();
                blackhole_obj.Shot(Power, Destroy_Time_Pjt, Radius, BlackHole_Start_Delay);
            }
        }
    }
}
