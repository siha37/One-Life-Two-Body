using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    private Gamemanager myChar;

    [Header("Target")]
    private Transform P_transform;
    [SerializeField] private float distance;
    private RaycastHit2D hit;

    [Header("Array")]
    [SerializeField] private BackGroundTrigger[] BackGroundImage;
    [SerializeField] protected int Array_NUM;

    //배경 이동 기준점
    [Header("Position")]
    [Tooltip("한칸 당 얼마나 이동하는 지 표시하는 X")]
    [SerializeField] private int Pos_X; 
    [Tooltip("한칸 당 얼마나 이동하는 지 표시하는 Y")]
    [SerializeField] private int Pos_Y;
    [Space(20)]
    [Tooltip("최종 위치로 이동하기 위한 이동량 X")]
    [SerializeField] private int T_Pos_X;
    [Tooltip("최종 위치로 이동하기 위한 이동량 Y")]
    [SerializeField] private int T_Pos_Y;

    //좌표 저장
    [Header("LIST_XY")]
    [Space(20)]
    [Tooltip("미리 잡아두는 X,Y,인덱스")]
    [SerializeField] private int Ready_X;
    [SerializeField] private int Ready_Y;
    [SerializeField] private int Ready_INDEX;
    [Space(20)]
    [Tooltip("전의 이미지의 관한 X,Y,인덱스")]
    [SerializeField] private int Before_X;
    [SerializeField] private int Before_Y;
    [SerializeField] private int Before_INDEX;
    [Space(20)]
    [Tooltip("현 위치한 이미지의 관한 X,Y,인덱스")]
    [SerializeField] private int Now_X;
    [SerializeField] private int Now_Y;
    [SerializeField] private int Now_INDEX;

    [Header("TARGET_XY")]
    [Tooltip("옮기려고하는 배경의 좌표값들(계속 돌려씀)")]
    [SerializeField] private int Target_X;
    [SerializeField] private int Target_Y;

    private void Start()
    {
        myChar = Gamemanager.myChar;
        P_transform = myChar.Player.transform;
        int i =0;
        foreach(BackGroundTrigger item in BackGroundImage)
        {
            item.INDEX = i++;
            item.scrolling = GetComponent<BackGroundScrolling>();
            item.Find_xy();
        }
    }
    private void Update()
    {
        Hit_Check();
    }
    public void  Hit_Check()
    {
        hit = Physics2D.Raycast(P_transform.position, Vector3.forward ,distance,LayerMask.GetMask("Background"));
        Debug.DrawRay(P_transform.position ,Vector3.forward * distance, Color.red);
        if(hit)
        {
            if(hit.transform.gameObject != BackGroundImage[Now_INDEX])
            {
                hit.transform.GetComponent<BackGroundTrigger>().Enter();
            }
        }
    }
    public void ReadySet(int x, int y, int Index)
    {
        Ready_X = x;
        Ready_Y = y;
        Ready_INDEX = Index;
    }
    
    public void SetXY()
    {
        Before_X = Now_X;
        Before_Y = Now_Y;
        Before_INDEX = Now_INDEX ;

        Now_X = Ready_X;
        Now_Y = Ready_Y;
        Now_INDEX = Ready_INDEX;

    }
    
    public void Translate_BG()
    {
        Vector3 B_Pos = BackGroundImage[Before_INDEX].transform.position,N_Pos = BackGroundImage[Now_INDEX].transform.position;
        if (B_Pos.x > N_Pos.x)//왼쪽
        {
            Target_X = Before_X + 2;
            if (Target_X > 4)
            {
                Target_X = Target_X - 5;
            }
            T_Pos_X = Pos_X * 5 * -1;
        }
        else if (B_Pos.x < N_Pos.x)//오른쪽
        {
            Target_X = Before_X - 2;
            if (Target_X < 0)
            {
                Target_X = 5+Target_X;
            }
            T_Pos_X = Pos_X * 5;
        }
        else
        {
            Target_X = -1;
        }
        if(B_Pos.y > N_Pos.y)//아래쪽
        {
            Target_Y = Before_Y - 2;
            if (Target_Y < 0)
            {
                Target_Y = 5 + Target_Y;
            }
            T_Pos_Y = Pos_Y * 5 * -1;
        }
        else if(B_Pos.y < N_Pos.y)//위쪽
        { 
            Target_Y = Before_Y + 2;
            if (Target_Y > 4)
            {
                Target_Y = Target_Y - 5;
            }
            T_Pos_Y = Pos_Y * 5;
        }
        else
        {
            Target_Y = -1;
        }

        if (Target_X != -1)
        {
            for(int Target_Y = 0; Target_Y < 5; Target_Y++)
            {
                Array_NUM = 5 * Target_Y + Target_X;
                BackGroundImage[Array_NUM].SetPosition(T_Pos_X, 0);
            }
        }
        else if (Target_Y != -1)
        {
            for (int Target_X = 0;Target_X < 5;Target_X++)
            {
                Array_NUM = 5 * Target_Y + Target_X;
                BackGroundImage[Array_NUM].SetPosition(0, T_Pos_Y);
            }
        }
    }

}
