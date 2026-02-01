using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Basic : BulletBasic
{

    Vector3 mouse;
    Camera cam;
    Collider2D col;
    public bool Waiting_Able = false;
    [SerializeField] ParticleSystem effect;
    void DataSet()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void Shot(float _speed, float _damage,float _destroyTime)
    {
        col = GetComponent<Collider2D>();
        col.enabled = true;
        transform.parent = myChar.BulletCollection.transform;
        speed = _speed;
        damage = _damage;
        DestoryTime = _destroyTime;
        BulletDestroy(DestoryTime,this.GetComponent<CapsuleCollider2D>());
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag =="ENEMY")
        {
            Destroy(gameObject);
            col.GetComponent<Enemy_Status>().Hit(damage);
        }
    }
    private void LateUpdate()
    {
        Bullet_Waiting();
    }

    public void Waiting_Change(bool a)
    {
        DataSet();
        Waiting_Able = a;
    }

    public void Bullet_Waiting()
    {
        if(Waiting_Able)
        {
            if (Time.timeScale != 0)
            {
                mouse = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));

                float rotateDegree = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.AngleAxis(rotateDegree - 90, Vector3.forward);
                
            }
        }
    }
    public void EffectPlay()
    {
        effect.Play();
    }
    public void EffectStop()
    {
        effect.Stop();
    }
}
