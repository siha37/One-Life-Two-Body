using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUI_Collection : MonoBehaviour
{
    Gamemanager myChar;

    void Start()
    {
        myChar = Gamemanager.myChar;
        myChar.EnemyDamageUI_Collection = this.transform;
    }
}
