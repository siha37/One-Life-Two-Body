using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class P_DelayBoom : BulletBasic
{
    float D_time = -1;
    bool Able_Timer = false;
    float KnockBack_Power;

    Enemy_Scen Scen;
    SkeletonAnimation skeleton;
    [SerializeField] AnimationReferenceAsset idle;
    [SerializeField]  AnimationReferenceAsset boom;

    public void Shot(float _speed, float _damage,float _radius,float _destory_Time,float _KnockBack_Power)
    {
        Scen = transform.GetChild(0).GetComponent<Enemy_Scen>();
        skeleton = GetComponent<SkeletonAnimation>();
        rd= GetComponent<Rigidbody2D>();

        speed = _speed;
        
        damage = _damage;

        Scen.transform.localScale = new Vector3(_radius, _radius, 0);

        D_time = _destory_Time;
        KnockBack_Power = _KnockBack_Power;
        Able_Timer = true;

        rd.AddForce(transform.up* speed * 10, ForceMode2D.Force);
    }
    private void Update()
    {
        if(Able_Timer)
        {
            if (D_time >= 0)
            {
                D_time -= Time.deltaTime;
            }
            else
            {
                Boom_1();
                Able_Timer = false;
            }
        }
        Scen.transform.position = this.transform.position;
    }

    private void Boom_1()
    {
        if(skeleton == null)
        {
            skeleton = GetComponent<SkeletonAnimation>();
        }
        TrackEntry boomTrack = skeleton.state.SetAnimation(0, "exp", false);
        boomTrack.End += Boom_2;
        skeleton.state.AddAnimation(0, "move", true,0);
    }

    private void Boom_2(TrackEntry trackEntry)
    {
        Scen.GetComponent<ParticleSystem>().Play();
        Sound_Play();
        for (int i = Scen.Enemy_List.Count - 1; i >= 0; i--)
        {
            if(Scen.Enemy_List[i] != null)
            {
                Scen.Enemy_List[i].GetComponent<Enemy_Status>().Hit(damage, KnockBack_Power, transform.position);
            }
        }
        audio_s.transform.parent = null;
        Scen.transform.parent = null;
        Destroy(Scen.gameObject, 3f);
        Destroy(audio_s, 3f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "PROJECTILE")
        {
            if(!col.GetComponent<P_DelayBoom>())
            {
                Able_Timer = false;
                Boom_1();
            }
        }
    }
}
