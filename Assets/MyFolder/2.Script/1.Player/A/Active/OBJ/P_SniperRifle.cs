using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_SniperRifle : BulletBasic
{

    public void Shot(float _speed, float _damage, float _destroyTime,float _penetrate)
    {
        speed = _speed;
        damage = _damage;
        DestoryTime = _destroyTime;
        Penetrate = _penetrate;
        BulletDestroy(DestoryTime, this.GetComponent<CapsuleCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ENEMY")
        {
            col.GetComponent<Enemy_Status>().Hit(damage);
            if (Penetrate <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                Penetrate--;
            }
        }
    }
}
