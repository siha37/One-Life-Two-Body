using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameTimer : MonoBehaviour
{
    Gamemanager myChar;
    [SerializeField] private TextMeshProUGUI TimerText;
    int M =0,S=0;
    private void Start()
    {
        myChar = Gamemanager.myChar;
        myChar.CurrentTIMER = 0;
    }

    private void Update()
    {
        myChar.CurrentTIMER += Time.deltaTime;

        M = (int)myChar.CurrentTIMER / 60;
        S = (int)myChar.CurrentTIMER % 60;
        if(M == 0)
        {
            string time = S.ToString("D2");
            TimerText.text = time;
        }
        else
        {
            string time = M.ToString() + ":" + S.ToString("D2");
            TimerText.text = time;
        }
    }
}
