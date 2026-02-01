using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy_HitDamage_UI : MonoBehaviour
{
    [SerializeField] private GameObject textGameOBJ;
    Gamemanager myChar;

    private void Start()
    {
        myChar = Gamemanager.myChar;
    }

    public void Damage_UI_Spawn(int damage)
    {
        if(myChar.ABLE_DamageUI)
        {
            TextMeshPro text = Instantiate(textGameOBJ, this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, myChar.EnemyDamageUI_Collection).GetComponent<TextMeshPro>();
            text.text = damage.ToString();
        }
    }
    public void Damage_UI_Spawn(float damage)
    {
        if (myChar.ABLE_DamageUI)
        {
            TextMeshPro text = Instantiate(textGameOBJ, this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, myChar.EnemyDamageUI_Collection).GetComponent<TextMeshPro>();
            text.text = damage.ToString();
        }
    }
    public void Damage_UI_Spawn(int damage,Color color)
    {
        if (myChar.ABLE_DamageUI)
        {
            TextMeshPro text = Instantiate(textGameOBJ, this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, myChar.EnemyDamageUI_Collection).GetComponent<TextMeshPro>();
            text.text = damage.ToString();
            text.color = color;
        }
    }
    public void Damage_UI_Spawn(float damage, Color color)
    {
        if (myChar.ABLE_DamageUI)
        {
            TextMeshPro text = Instantiate(textGameOBJ, this.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity, myChar.EnemyDamageUI_Collection).GetComponent<TextMeshPro>();
            text.text = damage.ToString();
            text.color = color;
        }
    }
    public void Damage_UI_Spawn(int damage, Color color,Vector3 pivot)
    {
        if (myChar.ABLE_DamageUI)
        {
            TextMeshPro text = Instantiate(textGameOBJ, pivot + new Vector3(0, 0.5f, 0), Quaternion.identity, myChar.EnemyDamageUI_Collection).GetComponent<TextMeshPro>();
            text.text = damage.ToString();
            text.color = color;
        }
    }
}
