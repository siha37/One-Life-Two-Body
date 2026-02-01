using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MiniGame_Manager : MonoBehaviour
{
    [SerializeField] PauseSystem pause;

    [Header("MiniGameSystem")]
    [SerializeField] bool PlayOnAwake;
    [SerializeField] bool OnlySuccess;
    [SerializeField] KeyPadMiniGame keypad_game;
    [SerializeField] ClickMiniGame clickMiniGame;
    [SerializeField] MiniGaem_Timer timer;
    [SerializeField] RewordManager reword;
    [SerializeField] Image smallGage;
    [SerializeField] int TargetScore = 20;


    [SerializeField] TextMeshProUGUI StartTimer_text;
    [ReadOnly] bool Active;
    float time;
    private void OnEnable()
    {
        if(PlayOnAwake)
        {
            pause.TimeStop();
            Active = true;
            StartCoroutine(StartTimer_Courtine());
        }
    }

    IEnumerator StartTimer_Courtine()
    {
        StartTimer_text.gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            StartTimer_text.text = (3 - i).ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        StartTimer_text.gameObject.SetActive(false);
        keypad_game.Start_Game();
        clickMiniGame.Start_Game();
        timer.StartTimer();
    }

    private void Update()
    {
        Debug.Log((float)(clickMiniGame.Score + keypad_game.Score) / (float)TargetScore);
        smallGage.fillAmount = (float)(clickMiniGame.Score + keypad_game.Score)/ (float)TargetScore;
    }

    public void EndGame()
    {

        int TotalScore = keypad_game.Score + clickMiniGame.Score - TargetScore;
        if(OnlySuccess)
        {
            StartTimer_text.gameObject.SetActive(true);
            StartTimer_text.text = "SUCCESS";
            Active = false;
            keypad_game.End_Game();
            clickMiniGame.End_Game();
            StartCoroutine(Reword_Delay());
        }
        else if(TotalScore > 0)
        {
            StartTimer_text.gameObject.SetActive(true);
            StartTimer_text.text = "SUCCESS";
            Active = false;
            keypad_game.End_Game();
            clickMiniGame.End_Game();
            StartCoroutine(Reword_Delay());
        }
        else
        {
            StartTimer_text.gameObject.SetActive(true);
            StartTimer_text.text = "FAIL";
            keypad_game.End_Game();
            clickMiniGame.End_Game();
            StartCoroutine(Fail_Delay());
        }
    }
    private IEnumerator Fail_Delay()
    {
        yield return new WaitForSecondsRealtime(1);
        StartTimer_text.gameObject.SetActive(false);
        END();
    }
    private IEnumerator Reword_Delay()
    {
        yield return new WaitForSecondsRealtime(1);
        StartTimer_text.gameObject.SetActive(false);
        Reword();
    }
    private void Reword()
    {
        reword.gameObject.SetActive(true);
        reword.Rorring_Start();
    }
    public void END()
    {
        pause.TimeFlow();
        reword.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
