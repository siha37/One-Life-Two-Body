using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ShotGun : BulletBasic
{

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    public void Shot(float _speed, float _damage, float _destoryTime)
    {
        rd = GetComponent<Rigidbody2D>();

        speed = _speed;
        damage = _damage;
        DestoryTime = _destoryTime;
        rd.velocity = Vector3.up * speed * Time.deltaTime;
        BulletDestroy(DestoryTime, this.GetComponent<BoxCollider2D>());
        StartCoroutine(DownSpeed());
    }
    public void Shot(float _speed,float _damage,float _destoryTime,bool DownSpeed_Chack)
    {
        rd = GetComponent<Rigidbody2D>();

        speed = _speed;
        damage = _damage;
        DestoryTime = _destoryTime;
        rd.velocity = Vector3.up * speed * Time.deltaTime;
        BulletDestroy(DestoryTime, this.GetComponent<BoxCollider2D>());
        if(DownSpeed_Chack)
        {
            StartCoroutine(DownSpeed());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "ENEMY")
        {
            col.GetComponent<Enemy_Status>().Hit(damage);
            Destroy(this.gameObject);
        }
    }
}
