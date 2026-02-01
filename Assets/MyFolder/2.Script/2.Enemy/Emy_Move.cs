using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emy_Move : MonoBehaviour
{
    Gamemanager myChar;

    //Component
    Transform tf;

    //EnemyDataBase
    Enemy_Status status;
    public float SlowSpeed_Percent =0;
    [SerializeField] private Vector3 dir;
    [SerializeField] private Vector3 dirNomal;
    public bool Move_Able =true;
    public bool KnockBack_Able = true;
    SpriteRenderer spriteRender;
    Rigidbody2D rd;

    //Player Data
    Transform P_tf;


    private void Start()
    {
        myChar = Gamemanager.myChar;
        P_tf = myChar.Player.transform;
        tf = transform;
        status = GetComponent<Enemy_Status>();
        rd = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (Move_Able)
        {
            dir = P_tf.position - tf.position;
            dirNomal = dir.normalized;
            if (dirNomal.x > 0)//¿À¸¥ÂÊ
            {
                spriteRender.flipX = false;
            }
            else//¿ÞÂÊ 
            {
                spriteRender.flipX = true;
            }
            Vector3 target = dirNomal * (status.Speed - (status.Speed * SlowSpeed_Percent * 0.01f)) * Time.deltaTime;
            tf.position += target; 
        }
    }

    public void KnockBack(float Power)
    {
        if(KnockBack_Able)
        {
            Vector3 TargetVector = dirNomal * -1;
            rd.velocity = TargetVector * Power;
        }
    }
    public void KnockBack(float Power, Vector3 pivot)
    {
        if (KnockBack_Able)
        {
            Vector3 TargetVector = pivot - tf.position;
            TargetVector = TargetVector.normalized * -1;
            rd.velocity = TargetVector * Power;
        }
    }
    //IEnumerator VelocityReset()
    //{
    //    yield return YieldInstructionCache.WaitForSeconds(0.2f);
    //    rd.velocity = Vector2.zero;
    //}
    //public void KnockBack(float Power)
    //{
    //    Move_Able = false;
    //    Vector3 TargetVector = dirNomal * -1;
    //    StopAllCoroutines();
    //    StartCoroutine(VelocityReset(TargetVector * Power));
    //}
    //public void KnockBack(float Power, Vector3 pivot)
    //{
    //    Move_Able = false;
    //    Vector3 TargetVector = pivot - tf.position;
    //    TargetVector = TargetVector.normalized * -1;
    //    StopAllCoroutines();
    //    StartCoroutine(VelocityReset(TargetVector * Power));
    //}
    //IEnumerator VelocityReset(Vector3 Target)
    //{
    //    for(int i = 0; i < 5; i++)
    //    {
    //        yield return YieldInstructionCache.WaitForSeconds(0.04f);
    //        transform.Translate(Target*Time.deltaTime);
    //    }
    //    Move_Able = true;
    //}
}
