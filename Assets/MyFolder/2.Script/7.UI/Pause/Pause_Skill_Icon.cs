using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause_Skill_Icon : MonoBehaviour
{
    [SerializeField]
    private Image ICON;
    [SerializeField]
    private TextMeshProUGUI level;
    public void DataSet(Sprite icon,int _level)
    {
        ICON.sprite = icon;
        level.text = "Lv. "+_level.ToString();
    }
}
