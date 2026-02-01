using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGaem_Timer : MonoBehaviour
{
    [Header("Main Time Property")]
    [SerializeField] float TotalTime;
    [ReadOnly] [SerializeField] float CurrentyTime;
    [ReadOnly][SerializeField] bool Active;

    [Space(10)]
    [Header("UI")]
    [SerializeField] Image bigGage;

    [Space(10)]
    [Header("game system Component")]
    [SerializeField] MiniGame_Manager manager;
    [SerializeField] ClickMiniGame ClickMini;
    [SerializeField] KeyPadMiniGame keyMini;

    private void OnEnable()
    {
        Reset_Timer();
    }
    private void Update()
    {
        if(Active)
        {
            Able_TImer();
            Gage_UPdata();
        }
    }

    private void Reset_Timer()
    {
        CurrentyTime = 0;
    }

    private void Gage_UPdata()
    {
        bigGage.fillAmount = 1-CurrentyTime / TotalTime;
    }

    private void Able_TImer()
    {
        //끝남
        if (CurrentyTime > TotalTime)
        {
            EndTimer();
            manager.EndGame();
        }
        else //진행
        {
            CurrentyTime += Time.unscaledDeltaTime;
        }
    }

    public void StartTimer()
    {
        Active = true;
    }
    public void EndTimer()
    {
        Active = false;
    }
}
