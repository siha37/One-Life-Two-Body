using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slow_Area : TOP_Projectile
{
    public float DownSpeed;
    public float Radius;

    Slow_Area_OBJ Area = null;
    private void Awake()
    {
        base.BaseData_Input(false);
        Projectile = Resources.Load<GameObject>("Prefab/Projectile/SlowArea");
        First_Level_Set(dataBase.SlowArea_P[0]);
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
        base.Level_Setting(dataBase.SlowArea_P[_level],dataBase.SlowArea_P.Length);
        DownSpeed = dataBase.SlowArea_P[_level].Damage;
        Destroy_Time_Pjt = dataBase.SlowArea_P[_level].Shot_MaxTime;
        Shot_Delay = dataBase.SlowArea_P[_level].Shot_Delay;
        Radius = dataBase.SlowArea_P[_level].Radius;
        if(MaxLevel-1 > _level)
        {
            NextLevel_Ex = dataBase.SlowArea_P[_level + 1].NextLevel_Ex;
        }
        NowTime = Shot_Delay;
    }
    private void Update()
    {
        if (Area == null)
        {
            if (NowTime > 0)
            {
                NowTime -= Time.deltaTime;
            }
            if (NowTime <= 0)
            {
                ProjectileCreat();
                NowTime = Shot_Delay;
            }
        }
    }

    public override void ProjectileCreat()
    {
        if (p_Status.Able_Resource_Chack(Resource_TYPE,Resource_Amount))
        {
            base.ProjectileCreat();
            Area = Instantiate(Projectile, this.transform).GetComponent<Slow_Area_OBJ>();
            Area.transform.localScale = new Vector3(Radius * 2, Radius * 2, 1);
            Area.Setting(DownSpeed);
            StartCoroutine(Area_Destroy()); 
        }
    }

    IEnumerator Area_Destroy()
    {
        yield return YieldInstructionCache.WaitForSeconds(Destroy_Time_Pjt);
        Destroy(Area.gameObject);
        Area = null;
    }
}
