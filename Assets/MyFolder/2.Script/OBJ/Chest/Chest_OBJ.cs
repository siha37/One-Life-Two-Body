using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_OBJ : MonoBehaviour
{
    GameObject MiniGameUI;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            GetChest();
        }
    }
    public void Chest_Set(GameObject _MiniGameUI)
    {
        MiniGameUI = _MiniGameUI;
    }
    private void GetChest()
    {
        MiniGameUI.SetActive(true);
        Destroy(gameObject);
    }
}
