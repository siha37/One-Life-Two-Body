using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_Enforce : MonoBehaviour
{
    Gamemanager myChar;
    [SerializeField]
    [ReadOnly]public bool Trun_On_Off;

    [SerializeField]
    private float Use_Resource = 5f;
    [SerializeField]
    private P2_Sound_Manager soundManager;
    [SerializeField]
    private P_Status P_s;

    [SerializeField] P2_Animation_Controll p2_Animation;

    private void Start()
    {
        myChar = Gamemanager.myChar;
        P_s = myChar.Player.GetComponent<P_Status>();
        p2_Animation = transform.parent.parent.GetComponent<P2_Animation_Controll>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && !myChar.Pause)
        {
            if(!Trun_On_Off)
            {
                soundManager.Sound_Play("Transform");
                p2_Animation.enable_EnforceColor();
                Trun_On_Off = true;
            }
        }
    }
    private void LateUpdate()
    {
        if (Trun_On_Off)
        {
            PowerUP();
        }
    }

    void PowerUP()
    {
        if(P_s.Able_Resource_Chack(SHOTTING.Use_Resource.MANA,Use_Resource))
        {
            P_s.Use_Resource_Caclulation(SHOTTING.Use_Resource.MANA,-Use_Resource * Time.deltaTime);
        }
        else
        {
            Trun_On_Off = false;
            p2_Animation.disable_EnforceColor();
            PowerDown();
        }
    }

    //±×°Å
    void PowerDown()
    {

    }
}
