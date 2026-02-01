using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Shild : MonoBehaviour
{
    [SerializeField] private P_Status status;
    [SerializeField] private float KnockBack_Power;
    [SerializeField] private float Damage = 0;
    [SerializeField] private SHOTTING.Use_Resource Resource_Type;
    [SerializeField] private float Resource_Amount;
    //public Shild_Damage_Buff damage_Buff;
    //[SerializeField]
    //ParticleSystem Shild_Buff_Effect;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ENEMY")
        {
            Enemy_Status e_status = col.GetComponent<Enemy_Status>();
            e_status.Hit(Damage, KnockBack_Power);
            if (status.Able_Resource_Chack_Effect(Resource_Type, Resource_Amount))
            {
                status.Use_Resource_Caclulation(Resource_Type, Resource_Amount);
            }
        }
        else if (col.tag == "ENEMY_PROJECTILE")
        {
            Destroy(col.gameObject);
            if (status.Able_Resource_Chack_Effect(Resource_Type, Resource_Amount))
            {
                status.Use_Resource_Caclulation(Resource_Type, Resource_Amount);
            }
        }
        //if (col.CompareTag("PROJECTILE") && damage_Buff != null && damage_Buff.Able)
        //{
        //    damage_Buff.Active_Buff(col.GetComponent<BulletBasic>());
        //}
    }

    //private void LateUpdate()
    //{
    //    if(damage_Buff != null)
    //    {
    //        if(damage_Buff.Able && !Shild_Buff_Effect.isPlaying)
    //        {
    //            Shild_Buff_Effect.time = 0;
    //            Shild_Buff_Effect.Play();
    //        }
    //        else if(!damage_Buff.Able)
    //        {
    //            Shild_Buff_Effect.Stop();
    //        }
    //    }
    //}
}
