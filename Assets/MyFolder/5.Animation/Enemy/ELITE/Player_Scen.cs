using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Scen : MonoBehaviour
{
    public List<Transform> Player_List = new List<Transform>();
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Player_List.Add(col.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            for (int i = 0; i < Player_List.Count; i++)
            {
                if (Player_List[i] == col.transform)
                {
                    Player_List.RemoveAt(i);
                }
            }
        }
    }
}
