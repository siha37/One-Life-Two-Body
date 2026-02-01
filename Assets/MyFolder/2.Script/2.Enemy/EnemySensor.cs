using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    Transform Camera_Pos;
    
    private void Start()
    {
        Camera_Pos = transform.parent.GetComponent<Transform>();
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "ENEMY" && col.GetComponent<Enemy_Status>().get_HP > 0 || col.tag=="CHEST")
        {
            Vector3 Enemy_Pos = col.transform.position;
            Vector3 Distance = Camera_Pos.position - Enemy_Pos;
            Distance *= -1;
            Vector3 Target =  Camera_Pos.position - Distance;
            col.transform.position = new Vector3(Target.x,Target.y,0);
        }
    }
}
