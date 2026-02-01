using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Bezier_L : MonoBehaviour
{
    [SerializeField] List<Enemy_Status> Enemy_List = new List<Enemy_Status>();
    BezierLaser_Shot_Projectile my_L_Shot;
    [SerializeField] private float Hit_time;
    private float timer = 0;
    private void Start()
    {
        my_L_Shot = transform.parent.GetComponent<BezierLaser_Shot_Projectile>();
    }
    private void Update()
    {
        if(my_L_Shot.Shoting)
        {
            timer += Time.deltaTime;
            if (timer > Hit_time)
            {
                HIT();
                timer = 0;
            }
        }
    }
    private void HIT()
    {
        for (int i = 0; i < Enemy_List.Count; i++)
        {
            if(Enemy_List[i] != null)
            {
                Enemy_List[i].Hit(my_L_Shot.OutPutDamage);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ENEMY")
        {
            bool isON = false;
            Enemy_Status hit = col.GetComponent<Enemy_Status>();
            foreach (Enemy_Status item in Enemy_List)
            {
                if (item == hit)
                {
                    isON = true;
                }
            }
            if(!isON)
            {
                Enemy_List.Add(col.GetComponent<Enemy_Status>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag =="ENEMY")
        {
            Enemy_List.Remove(col.GetComponent<Enemy_Status>());
        }
    }

}
