using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundTrigger : MonoBehaviour
{
    public BackGroundScrolling scrolling;
    public int INDEX;
    [SerializeField] private int x, y;

    public void Find_xy()
    {
        x = (INDEX % 5);
        if (x < 0)
        {
            x = 4;
        }
        y = (INDEX - x) / 5;
        if(INDEX == 12)
        {
            scrolling.ReadySet(x, y,INDEX);
            scrolling.SetXY();
        }
    }

    public void Enter()
    {
        scrolling.ReadySet(x, y, INDEX);
        scrolling.SetXY();
        scrolling.Translate_BG();
    }
    public void SetPosition(int x,int y)
    {
        transform.position += new Vector3(x,y,0);
    }
}
