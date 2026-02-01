using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead_Anim : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Hurt_Start()
    {
        anim.SetBool("HURT", true);
    }
    public void Hurt_End()
    {
        anim.SetBool("HURT", false);
    }

}
