using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Auto_Hill : MonoBehaviour
{
    TopGameData TOP;
    [Tooltip("회복할 데이터 참고")]
    P_Status status;

    [Tooltip("힐을 몇 초마다 할지")]
    public float Active_Hill_Time;
    [Tooltip("힐 하는 퍼센트지")]
    [SerializeField] float Hill_percent;
    public float time;


    private void Start()
    {
        TOP = TopGameData.Dataset;
        status = GetComponent<P_Status>();
        time = Active_Hill_Time;
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            if(status.Current_HP < TOP.HP)
            {
                status.Hill(TOP.HP * Hill_percent * 0.01f);
            }
            time = Active_Hill_Time;
        }
    }
}
