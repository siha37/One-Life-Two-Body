using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scen : MonoBehaviour
{
    public List<Transform> Enemy_List = new List<Transform>();
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ENEMY")
        {
            Enemy_List.Add(col.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "ENEMY")
        {
            for (int i = 0; i < Enemy_List.Count; i++)
            {
                if (Enemy_List[i] == col.transform)
                {
                    Enemy_List.RemoveAt(i);
                }
            }
        }
    }
}
