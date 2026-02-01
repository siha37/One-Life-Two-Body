using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite_Missile_OBJ : MonoBehaviour
{
    float Delay_time,Damage;
    float Currenty_time;
    public List<Transform> Player_List = new List<Transform>();
    private bool done =false;
    Transform gauge;
    Animator anim;

    public void StartSet(float BoomDelay_Time,float _damage)
    {
        Delay_time = BoomDelay_Time;
        Currenty_time = 0;
        Damage = _damage;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Currenty_time >= Delay_time )
        {
            if(!done)
            {
                done = true;
                anim.SetTrigger("BOOM");
            }
        }
        else
        {
            if (gauge != null)
            {
                gauge.localScale = new Vector3(1,1,0) * Currenty_time / Delay_time;
            }
            else
            {
                gauge = transform.GetChild(0);
            }
            Currenty_time += Time.deltaTime;
        }

    }

    public void Boom()
    {
        for (int i = 0; i < Player_List.Count; i++)
        {
            if (Player_List[i].GetComponent<P_Status>() != null)
            {
                Player_List[i].GetComponent<P_Status>().HIT(Damage);
            }
            else
            {
                Player_List[i].GetComponent<Arm_HP>().Hit(Damage);
            }
        }
    }
    public void OBJ_Destory()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Player_List.Add(col.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            for (int i = 0; i < Player_List.Count; i++)
            {
                if (Player_List[i] == col.transform)
                {
                    Player_List.RemoveAt(i);
                }
            }
        }
    }
}
