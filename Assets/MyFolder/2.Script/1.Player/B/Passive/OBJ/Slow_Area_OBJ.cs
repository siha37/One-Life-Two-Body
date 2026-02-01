using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Area_OBJ : MonoBehaviour
{
    private float SlowSpeed_Percent;

    public void Setting(float ss_p)
    {
        SlowSpeed_Percent = ss_p;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "ENEMY")
        {
            col.GetComponent<Emy_Move>().SlowSpeed_Percent = SlowSpeed_Percent;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "ENEMY")
        {
            col.GetComponent<Emy_Move>().SlowSpeed_Percent = 0;
        }
    }
}
