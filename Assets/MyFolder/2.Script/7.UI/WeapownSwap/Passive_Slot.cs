using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passive_Slot : MonoBehaviour
{
    private TOP_Projectile skill_data;
    private Image m_Image;
    private Image front_Image;

    public void Slot_Set(TOP_Projectile data,Sprite _image)
    {
        m_Image = GetComponent<Image>();
        m_Image.sprite = _image;
        front_Image = transform.GetChild(0).GetComponent<Image>();
        front_Image.sprite = _image;
        skill_data = data;
    }
    private void LateUpdate()
    {
        if(m_Image != null)
        {
            front_Image.fillAmount = 1 - skill_data.Get_NowTime() / skill_data.Get_Shot_Dealy();
        }
    }
}

