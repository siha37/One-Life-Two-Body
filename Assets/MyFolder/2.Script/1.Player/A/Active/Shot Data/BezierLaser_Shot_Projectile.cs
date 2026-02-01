using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierLaser_Shot_Projectile : TOP_Projectile
{
    private const int point_Amount = 4;
    private float Line_Widht;
    private Transform[] Point_List = new Transform[point_Amount];
    private Transform[] Pos_List = new Transform[point_Amount];
    private LineRenderer Line_render;

    //추가 설정치
    [HideInInspector] public float OutPutDamage;
    private int B_Amount = 20;
    private int Currenty_B_Amount;
    private float MaxDistance;
    [Range(0, 10)] [SerializeField] private float Side_Dis = 1;
    [SerializeField] private float[] Follow_Speed = new float[point_Amount - 1];
    private float Shot_MaxTime;

    private void Awake()
    {
        base.BaseData_Input(true);
        Shoting = false;
        First_Level_Set(dataBase.Bezier_L_P[0]);
        Point_Creating();
    }
    private void OnEnable()
    {
        Shot.WeapownList_Add(GetComponent<BezierLaser_Shot_Projectile>(), Skill_UI_Image);
        level_UP.Active_A_Skill.Add(this);
        Line_OBJ_Setting();
        Point_ReSetting();
    }

    [System.Obsolete]
    private void Update()
    {
        FollowPoint();
        if (Shoting)
        {
            //Side Point Move
            SidePointMove();

            //Bezier Calculation
            Line_render.widthMultiplier = Line_Widht;
            Line_render.positionCount = Currenty_B_Amount;
            for (int i = 0; i < Currenty_B_Amount; i++)
            {
                Vector3 to = bezier(Point_List, (i / (float)B_Amount));
                Line_render.SetPosition(i, to);
            }
            if (Currenty_B_Amount < B_Amount)
            {
                Currenty_B_Amount++;
            }
            Line_render.enabled = true;
        }


    }


    //Level and Data


    override public void Data_Input()
    {
        base.Data_Input();
    }

    override public bool LEVEL_UP()
    {
        Level_Setting(Level);
        return base.LEVEL_UP();
    }
    override public void Level_Setting(int _level)
    {
        base.Level_Setting(dataBase.Bezier_L_P[_level], dataBase.Bezier_L_P.Length);
        MaxDistance = dataBase.Bezier_L_P[_level].MaxDistance;
        Shot_MaxTime = dataBase.Bezier_L_P[_level].Shot_MaxTime;
        Shot_Delay = dataBase.Bezier_L_P[_level].Shot_Delay;
        Line_Widht = dataBase.Bezier_L_P[_level].Line_Widht;
        if (MaxLevel - 1 > _level)
        {
            NextLevel_Ex = dataBase.Bezier_L_P[_level + 1].NextLevel_Ex;
        }
        if (_level != 0)
        {
            Point_ReSetting();
        }
        Percent_Data();
    }

    private void Line_OBJ_Setting()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefab/Projectile/Line");
        GameObject obj = Instantiate(prefab, this.transform);
        Line_render = obj.GetComponent<LineRenderer>();
        Line_render.positionCount = B_Amount;
        Line_render.enabled = false;
    }


    //Shot
    override public void ProjectileCreat()
    {
        if (!Shot.Enforce.Trun_On_Off)
        {
            Point_ReSetting();
            OutPutDamage = BasicDamage;
        }
        else
        {

            Point_ReSetting(UP_function.UP_Distance(MaxDistance, UP_Distance));
            OutPutDamage = UP_function.UP_Damage(BasicDamage, UP_Damage);
        }
        Shoting = true;
        Sound_Manager.OneLoop_AudioPlay(Shoting_Sound[0]);
        Currenty_B_Amount = 1;
        StartCoroutine("Auto_Stop");
    }
    override public void StopShot()
    {
        Shoting = false;
        Sound_Manager.OneLoop_AudioStop();
        Line_render.enabled = false;
        StopCoroutine("Auto_Stop");
    }

    IEnumerator Auto_Stop()
    {
        yield return YieldInstructionCache.WaitForSeconds(Shot_MaxTime);
        Shot.AutoStopControll();
    }

    //setting
    private void Point_Creating()
    {
        for (int i = 0; i < point_Amount; i++)
        {
            Pos_List[i] = new GameObject("Pos" + i.ToString()).transform;
            Pos_List[i].parent = this.transform;
            if (i == 0)
            {
                Pos_List[i].position = Spawn_Point.position;
            }
            else
            {
                Pos_List[i].localPosition = Vector3.up * ((i + 1) / (float)point_Amount * MaxDistance);
                if (i + 1 != point_Amount)
                {
                    Vector3 side_dir = Vector3.right;
                    if (i % 2 == 0)
                    {
                        side_dir *= -1;
                    }
                    Pos_List[i].localPosition += side_dir * Side_Dis;
                }
            }
            Point_List[i] = new GameObject("Point" + i.ToString()).transform;
        }
    }

    private void Point_ReSetting()
    {
        for (int i = 1; i < Pos_List.Length; i++)
        {
            Pos_List[i].localPosition = Vector3.up * ((i + 1) / (float)point_Amount * MaxDistance);
            if (i + 1 != point_Amount)
            {
                Vector3 side_dir = Vector3.right;
                if (i % 2 == 0)
                {
                    side_dir *= -1;
                }
                Pos_List[i].localPosition += side_dir * Side_Dis;
            }
        }
    }
    private void Point_ReSetting(float Distance)
    {
        for (int i = 1; i < Pos_List.Length; i++)
        {
            Pos_List[i].localPosition = Vector3.up * ((i + 1) / (float)point_Amount * Distance);
            if (i + 1 != point_Amount)
            {
                Vector3 side_dir = Vector3.right;
                if (i % 2 == 0)
                {
                    side_dir *= -1;
                }
                Pos_List[i].localPosition += side_dir * Side_Dis;
            }
        }
    }
    private void SidePointMove()
    {
        for (int i = 1; i < Pos_List.Length - 1; i++)
        {
            float Side_Pingpong = Mathf.PingPong(Time.time * 40, Side_Dis * 2);
            Side_Pingpong = Side_Dis - Side_Pingpong;
            float side_dir = 1;
            if (i % 2 == 0)
            {
                side_dir *= -1;
            }
            Pos_List[i].localPosition = new Vector3(side_dir * Side_Pingpong, Pos_List[i].localPosition.y, 0);
        }
    }

    //calculation

    private Vector3 bezier(Transform[] Points, float t)
    {
        List<Vector3> ResultPoint = new List<Vector3>();
        for (int i = 0; i < Points.Length - 1; i++)
        {
            ResultPoint.Add(Points[i].position + (Points[i + 1].position - Points[i].position) * t);
        }
        if (ResultPoint.Count > 1)
        {
            return bezier(ResultPoint, t);
        }
        else
        {
            return ResultPoint[0];
        }
    }
    private Vector3 bezier(List<Vector3> Points, float t)
    {
        List<Vector3> ResultPoint = new List<Vector3>();
        for (int i = 0; i < Points.Count - 1; i++)
        {
            ResultPoint.Add(Points[i] + (Points[i + 1] - Points[i]) * t);
        }
        if (ResultPoint.Count > 1)
        {
            return bezier(ResultPoint, t);
        }
        else
        {
            return ResultPoint[0];
        }
    }

    private void FollowPoint()
    {
        Point_List[0].position = Pos_List[0].position;
        for (int i = 1; i < point_Amount; i++)
        {
            Point_List[i].position = Vector3.Lerp(Point_List[i].position, Pos_List[i].position, Follow_Speed[i - 1] * Time.deltaTime);
        }
    }

}
