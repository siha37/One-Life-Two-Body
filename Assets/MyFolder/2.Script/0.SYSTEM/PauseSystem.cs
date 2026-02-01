using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    Gamemanager myChar;
    [Tooltip("일시정시 시 표시 UI Canvas")]
    [SerializeField] private GameObject PauseCanvas;
    [Tooltip("일시정지 시 총 스탯 아웃풋")]
    [SerializeField] private PlayerStatusPanel playerStatusPanel;
    [Tooltip("일시정지 시 총 스킬 표시 UI")]
    [SerializeField] private PlayerSkill_Pause_Panel skillPanel;
    [Tooltip("Pause 상태인지 아닌지를 구분")]
    [ReadOnly] private bool ON_OFF;

    private void Awake()
    {
        myChar = Gamemanager.myChar;
        PauseCanvas.SetActive(false);
    }

    private void Update() 
    { 
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Time.timeScale.Equals(0))
            {
                if (!ON_OFF)
                {
                    OnPause();
                }
                else
                {
                    OffPause();
                } 
            }
        }
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        myChar.Pause = true;
    }
    public void TimeFlow()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        myChar.Pause = false;
    }
    public void OnPause()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        skillPanel.Skill_Data_set();
        playerStatusPanel.StatusSet();
        PauseCanvas.SetActive(true);
        myChar.Pause = true;
        ON_OFF = true;
    }
    public void OffPause()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        PauseCanvas.SetActive(false);
        myChar.Pause = false;
        ON_OFF = false;
    }
    public void MainTitleSceneMove()
    {
        LoadingScene.LoadScene("Title");
    }
}
