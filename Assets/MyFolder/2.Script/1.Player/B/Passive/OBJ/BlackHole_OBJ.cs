using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_OBJ: BulletBasic
{
    float Power,Radius;
    Animator anim;
    List<Transform> enemy_list = new List<Transform>();
    bool Active =false;
    public void Shot(float _power,float _destory,float _radius,float Start_Delay)
    {
        anim  = GetComponent<Animator>();   
        Power = _power;
        DestoryTime = _destory;
        Radius = _radius;
        transform.localScale = new Vector3(Radius, Radius, 0);
        float time = 60 / (Start_Delay * 60);
        anim.SetFloat("Delay_Time",time);
        anim.SetTrigger("START");
    }
    private void Update()
    {
        if(Active)
        {
            Gravitation();
            if (Timer())
            {
                Disable_Chang();
            }
        }
    }

    private bool Timer()
    {
        if(time > DestoryTime)
        {
            return true;
        }
        else
        {
            time += Time.deltaTime;
        }
        return false;
    }
    public void Active_Chang()
    {
        Active = true;
        time = 0;
    }
    public void Disable_Chang()
    {
        Active=false;
        anim.SetTrigger("END");
    }
    public void Destroy_BlackHole()
    {
        Destroy(gameObject);
    }
    private void Gravitation()
    {
        for (int i = 0; i < enemy_list.Count; i++)
        {
            if (enemy_list[i] != null)
            {
                Vector3 dir = transform.position - enemy_list[i].position;
                dir = dir.normalized;
                enemy_list[i].transform.Translate(dir * Power * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("ENEMY"))
        {
            if(col.GetComponent<Enemy_Status>().enemy_type_ == Enemy_Status.enemy_type.basic)
            {
                enemy_list.Add(col.transform);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("ENEMY"))
        {
            if (col.GetComponent<Enemy_Status>().enemy_type_ == Enemy_Status.enemy_type.basic)
            {
                enemy_list.Remove(col.transform);
            }
        }
    }
}
