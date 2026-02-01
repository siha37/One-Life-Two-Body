using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollection : MonoBehaviour
{
    Gamemanager myChar;
    private void Awake()
    {
        myChar = Gamemanager.myChar;
        myChar.BulletCollection = this.gameObject;
    }
}
