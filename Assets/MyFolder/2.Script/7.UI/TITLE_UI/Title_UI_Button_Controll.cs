using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_UI_Button_Controll : MonoBehaviour
{
    [SerializeField] Toggle _toggle;

    public void PlayGame()
    {
        if (!_toggle.isOn)
        {
            LoadingScene.LoadScene("InGame");
        }
        else
        {
            LoadingScene.LoadScene("InGame_ALL");
        }
    }


    public void EndGame()
    {
        Application.Quit();
    }
}
