using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Dash : MonoBehaviour
{
    TopGameData TOP;

    Transform P_tf;
    P_Status p_status;
    private float Dash_Speed;
    private float Dash_Delay_Time =0;
    private bool Dash_Delay = true;
    private float Dash_Resource_Amount;
    [SerializeField] private SHOTTING.Use_Resource ResourceType;
    P1_Animation_Controller animation_Con;

    private void Start()
    {
        TOP = TopGameData.Dataset;
        p_status = GetComponent<P_Status>();
        P_tf = transform;
        Dash_Speed = TOP.Dash_Speed;
        Dash_Resource_Amount = TOP.Dash_Resource_Amount;
        animation_Con= GetComponent<P1_Animation_Controller>(); 
    }
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if(h != 0 && v!= 0)
        {
            h = h * 0.5f;
            v = v * 0.5f;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && Dash_Delay&& p_status.Able_Resource_Chack_Effect(ResourceType, Dash_Resource_Amount))
        {
            P_tf.transform.position += new Vector3(h, v, 0) * Dash_Speed;
            animation_Con.AnimationStatus(1, "DASH", false);
            p_status.Use_Resource_Caclulation(ResourceType, -Dash_Resource_Amount);
            Dash_Delay = false;
            Dash_Delay_Time = TOP.Dash_Delay_Time;
        }

        if(Dash_Delay_Time > 0)
        {
            Dash_Delay_Time -= Time.deltaTime;
        }
        else
        {
            Dash_Delay = true;
        }
    }
}
