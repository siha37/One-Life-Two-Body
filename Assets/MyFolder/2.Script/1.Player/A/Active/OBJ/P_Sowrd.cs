using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Sowrd : BulletBasic
{
    [SerializeField] Animator animator;
    Camera cam;
    Transform tf;
    bool Play = true;
    bool DistantionAble = false;
    [SerializeField]
    private Enemy_Scen Scen;

    public void Shot(float _damage,float _speed)
    {
        cam = Camera.main;
        tf = transform;
        damage = _damage;
        speed = _speed;
    }

    private void Update()
    {
        if (Play)
        {
            Vector2 pos = Input.mousePosition;
            pos = cam.ScreenToWorldPoint(pos);
            Vector2 tf_pos = tf.position; 
            Vector2 dismagnitude = pos - tf_pos;
            float dis = dismagnitude.magnitude;
            float angle = Mathf.Atan2(dismagnitude.y, dismagnitude.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, speed * Time.deltaTime);
            if (dis > 1)
            {
                DistantionAble = true;
                //tf.rotation = Quaternion.Lerp(tf.rotation, rotation, Time.deltaTime*20);
                tf.rotation = rotation;
                tf.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else
            {
                DistantionAble = false;
                tf.rotation = Quaternion.Slerp(tf.rotation, Quaternion.Euler(0, 0, 0), speed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ENEMY" && DistantionAble)
        {
            col.GetComponent<Enemy_Status>().Hit(damage);
        }
    }
    public void Enemys_Hitting()
    {
        for (int i = 0; i < Scen.Enemy_List.Count; i++)
        {
            if(Scen.Enemy_List[i] != null)
            {
                Scen.Enemy_List[i].GetComponent<Enemy_Status>().Hit(damage);
            }
        }
    }
    public void EndHitting()
    {
        Play = false;
        Sound_Play();
        animator.SetTrigger("ROLLHITTING");
    }
    public void JustEnd()
    {
        Play = false;
        animator.SetTrigger("END");
    }
    public void OBJ_Destory()
    {
        Destroy(gameObject);
    }
}
