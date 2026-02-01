using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class P2_Animation_Controll : MonoBehaviour
{
    private MeshRenderer _renderer;
    private MaterialPropertyBlock _block;
    private MaterialPropertyBlock _Tint;
    private int Color_id;
    private int Tint_Color_id;
    [SerializeField] private Color Basic;
    [SerializeField] private Color Red;

    [SerializeField] private Color TintColor;
    [SerializeField] private Color BlackColor;
    [SerializeField] private bool Enforceing;

    private SkeletonAnimation skeleton_Anim;
    [SerializeField] AnimationReferenceAsset IDLE;
    [SerializeField] AnimationReferenceAsset MOVE;
    [SerializeField] AnimationReferenceAsset HIT;
    [SerializeField] AnimationReferenceAsset DEATH;
    [SerializeField] AnimationReferenceAsset Charging;
    [SerializeField] AnimationReferenceAsset Charging_shot;
    [SerializeField] AnimationReferenceAsset bome_shot;
    [SerializeField] AnimationReferenceAsset machinegun_shot;
    [SerializeField] AnimationReferenceAsset rifle_shot;
    


    string currenty_Status_Name;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _block = new MaterialPropertyBlock();
        _renderer.SetPropertyBlock(_block);
        Color_id = Shader.PropertyToID("_Black");
        Tint_Color_id = Shader.PropertyToID("_Color");
        skeleton_Anim = GetComponent<SkeletonAnimation>();
    }
    public void SetAnimation(int Track, AnimationReferenceAsset anim, bool loop, float timeScale)
    {
        if (currenty_Status_Name == anim.name)
        {
            return;
        }
        skeleton_Anim.AnimationState.SetAnimation(Track, anim, loop).TimeScale = timeScale;
        currenty_Status_Name = anim.name;
    }
    public void AddAnimation(int Track, AnimationReferenceAsset anim, bool loop, float timeScale)
    {
        if (currenty_Status_Name == anim.name)
        {
            return;
        }
        skeleton_Anim.AnimationState.AddAnimation(Track, anim, loop, 0).TimeScale = timeScale;
        currenty_Status_Name = anim.name;
    }
    public void SetAnimation_Ignore(int Track, AnimationReferenceAsset anim, bool loop, float timeScale)
    {
        skeleton_Anim.AnimationState.SetAnimation(Track, anim, loop).TimeScale = timeScale;
        currenty_Status_Name = anim.name;
    }
    public void ShotAtionStatus(int Track,string status,bool loop,bool Empty)
    {
        switch (status)
        {
            case "Charging":
                SetAnimation(Track, Charging, loop, 1);
                break;
            case "Charging_shot":
                SetAnimation_Ignore(Track, Charging_shot, loop, 1);
                break;
            case "bome_shot":
                SetAnimation_Ignore(Track, bome_shot, loop, 1);
                break;
            case "machinegun_shot":
                SetAnimation(Track, machinegun_shot, loop, 1);
                break;
            case "rifle_shot":
                SetAnimation_Ignore(Track, rifle_shot, loop, 1);
                break;
            default:
                break;
        }
        if(Empty)
        {
            skeleton_Anim.AnimationState.AddEmptyAnimation(Track, 0, 0);
        }
    }
    public void ShotAtionStatus_Set(int Track, string status, bool loop, bool Empty)
    {
        switch (status)
        {
            case "Charging":
                SetAnimation(Track, Charging, loop, 1);
                break;
            case "Charging_shot":
                SetAnimation_Ignore(Track, Charging_shot, loop, 1);
                break;
            case "bome_shot":
                SetAnimation_Ignore(Track, bome_shot, loop, 1);
                break;
            case "machinegun_shot":
                SetAnimation(Track, machinegun_shot, loop, 1);
                skeleton_Anim.AnimationState.AddEmptyAnimation(1, 0, 0);
                break;
            case "rifle_shot":
                SetAnimation_Ignore(Track, rifle_shot, loop, 1);
                break;
            default:
                break;
        }
        if (Empty)
        {
            skeleton_Anim.AnimationState.SetEmptyAnimation(Track, 0);
        }
    }
    public void AnimationStatus(int Track, string status, bool loop)
    {
        if (currenty_Status_Name != "Death")
        {
            if (status.Equals("MOVE"))
            {
                if (currenty_Status_Name == "HIT")
                {
                    AddAnimation(Track, MOVE, loop, 1);
                }
                else
                {
                    SetAnimation(Track, MOVE, loop, 1);
                }
            }
            else if (status.Equals("HIT"))
            {
                SetAnimation(Track, HIT, loop, 1);
                skeleton_Anim.AnimationState.AddEmptyAnimation(1, 0, 0); 
                Color_Twinkl_Start(1);
            }
            else if (status.Equals("DEATH"))
            {
                SetAnimation(Track, DEATH, loop, 1);
            }
            else
            {
                if (currenty_Status_Name == "HIT")
                {
                    AddAnimation(Track, IDLE, loop, 1);
                }
                else
                {
                    SetAnimation(Track, IDLE, loop, 1);
                }
            }
        }
    }

    [System.Obsolete]
    public void FlipX(bool flip)
    {
        skeleton_Anim.skeleton.FlipX = flip;
    }
    public void Color_Twinkl_Start(float time)
    {
        if(!Enforceing)
        {
            StartCoroutine(Color_Twinkling_Coroutine(time));
        }
    }
    IEnumerator Color_Twinkling_Coroutine(float time)
    {
        StartCoroutine("Color_Twinkling");
        yield return YieldInstructionCache.WaitForSeconds(time);
        StopCoroutine("Color_Twinkling");
        _block.SetColor(Color_id, Basic);
        _renderer.SetPropertyBlock(_block);
    }
    private IEnumerator Color_Twinkling()
    {
        while (true)
        {
            for (float i = 0; i < 1; i += 0.1f)
            {
                Color nowColor = Color.Lerp(Basic, Red, i);
                //skeleton_Anim.skeleton.SetColor(nowColor);
                _block.SetColor(Color_id, nowColor);
                _renderer.SetPropertyBlock(_block);
                yield return YieldInstructionCache.WaitForSeconds(0.05f);
            }
            for (float i = 0; i < 1; i += 0.1f)
            {
                Color nowColor = Color.Lerp(Red, Basic, i);
                _block.SetColor(Color_id, nowColor);
                _renderer.SetPropertyBlock(_block);
                yield return YieldInstructionCache.WaitForSeconds(0.05f);
            }
        }
    }

    public void enable_EnforceColor()
    {
        _block.SetColor(Tint_Color_id, TintColor);
        _block.SetColor(Color_id, BlackColor);
        _renderer.SetPropertyBlock(_block);
        Enforceing = true;
    }
    public void disable_EnforceColor()
    {
        _block.SetColor(Color_id, Basic);
        _block.SetColor(Tint_Color_id, Color.white);
        _renderer.SetPropertyBlock(_block);
        Enforceing = false;
    }
}
