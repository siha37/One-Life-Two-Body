using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Guided_M_Projectile : MonoBehaviour
{
    Transform tf;
    Enemy_Scen scen;
    ParticleSystem LineEffect;
    ParticleSystem BoomEffect;
    
    float speed;
    float damage;

    List<Transform> Enemy_List = new List<Transform>();

    Transform MainTarget;
    Vector3 FirstTarget;

    bool FIRST_SHOT =false;
    bool SECOND_SHOT =false;

    private void Update()
    {
        if(FIRST_SHOT)
        {
            tf.position = Vector3.MoveTowards(tf.position, FirstTarget, speed*2*Time.deltaTime);
            float dic = Vector3.Distance(tf.position, FirstTarget);
            if(dic < 0.05f)
            {
                FIRST_SHOT = false;
                StartCoroutine(Delay_Second_Target_Set());
            }
        }
        else if(SECOND_SHOT)
        {
            MainTarget = Target_Enemy_Selcting();
            if(MainTarget != null)
            {
                Angle_set(MainTarget.position);
                tf.position = Vector3.MoveTowards(tf.position,MainTarget.position,speed * Time.deltaTime);
            }
            else
            {
                //Effect 분리
                BoomEffect.Play();
                LineEffect.Stop();
                LineEffect.transform.parent = null;
                BoomEffect.transform.parent = null;
                Destroy(LineEffect.gameObject, 1);
                Destroy(BoomEffect.gameObject, 1);

                Destroy(gameObject, 0.1f);
            }
        }
    }
    public void Shot(float _speed, float _damage, float _destroyTime, Vector3 _firstTarget)
    {
        tf = transform;
        scen = this.transform.GetChild(0).GetComponent<Enemy_Scen>();
        LineEffect = this.transform.GetChild(1).GetComponent<ParticleSystem>();
        BoomEffect = this.transform.GetChild(2).GetComponent<ParticleSystem>();

        //data input
        speed = _speed;
        damage = _damage;

        //spawn rotation set
        FirstTarget = _firstTarget;
        Angle_set(_firstTarget);

        FIRST_SHOT = true;
        Destroy(this.gameObject, _destroyTime);
    }
    IEnumerator Delay_Second_Target_Set()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.1f);
        SECOND_SHOT = true;
    }
    private void Angle_set(Vector3 target)
    {
        Vector3 dir = target - tf.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        tf.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private Transform Target_Enemy_Selcting()
    {
        float distence =0;
        Transform Enemy_tf = null;
        Enemy_List = scen.Enemy_List;

        if (Enemy_List.Count == 0)
        {
            return null;
        }
        else
        {
            for (int i = 0; i < Enemy_List.Count; i++)
            {
                float new_Dis =0;
                if(Enemy_List[i] == null)
                {
                    continue;
                }
                new_Dis = Vector3.SqrMagnitude(Enemy_List[i].position - tf.position);
                if (distence == 0 || distence > new_Dis)
                {
                    distence = new_Dis;
                    Enemy_tf = Enemy_List[i];
                }
            }
            return Enemy_tf;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ENEMY")
        {
            //Effect 분리
            BoomEffect.Play();
            LineEffect.Stop();
            LineEffect.transform.parent = null;
            BoomEffect.transform.parent = null;
            Destroy(LineEffect.gameObject,1);
            Destroy(BoomEffect.gameObject,1);

            col.GetComponent<Enemy_Status>().Hit(0,damage);
            Destroy(gameObject);
        }
    }
}
