

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Move : MonoBehaviour
{
    //TOP 
    TopGameData TOP;

    
    private float MoveSpeed;
    private float LiveSpeed;
    Gamemanager myChar;
    public bool Moving;
    public bool Move_Able = true;

    private float h, v;
    P1_Animation_Controller animation_Con;

    private void Awake()
    {
        myChar = Gamemanager.myChar;
        myChar.Player = this.gameObject;
        TOP = TopGameData.Dataset;
        animation_Con =  GetComponent<P1_Animation_Controller>();
    }

    [System.Obsolete]
    void Update()
    {
        if (Move_Able)
        {
            MoveSpeed = TOP.B_Speed;
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            if (h == 0 && v == 0)
            {
                Moving = false;
                animation_Con.AnimationStatus(0, "IDLE", true);
            }
            else
            {
                Moving = true;
                animation_Con.AnimationStatus(0, "MOVE", true);
            }
            if (h > 0)
            {
                animation_Con.FlipX(true);
            }
            else if (h < 0)
            {
                animation_Con.FlipX(false);
            }
            if (h != 0 && v != 0)
            {
                LiveSpeed = MoveSpeed * 0.5f;
            }
            else
            {
                LiveSpeed = MoveSpeed;
            }
            this.transform.Translate((new Vector3(h, v, 0) * LiveSpeed) * Time.deltaTime);
        }
    }
}
