using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP_Get_Check : MonoBehaviour
{

    [SerializeField] P_Status p_status;
    [SerializeField] P2_Sound_Manager audio_manager;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "EXP")
        {
            p_status.EXP_GET(col.GetComponent<EXP_Ball>().EXP_Amount);
            audio_manager.Sound_Play("EXP");
            Destroy(col.gameObject);
        }
    }
}
