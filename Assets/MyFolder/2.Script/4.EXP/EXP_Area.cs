using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP_Area : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EXP"))
        {
            other.GetComponent<EXP_Ball>().Move_Able =true;
        }
    }
}
