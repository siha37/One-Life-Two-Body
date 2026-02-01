using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option_Resolution : MonoBehaviour
{
    FullScreenMode screenMode;
    [SerializeField]
    private Toggle fullscreenBtn;
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    List<Resolution> resolutionList = new List<Resolution>();
    int resolutionNUM;

    private void Start()
    {
        InitUI();
    }
    void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if(Screen.resolutions[i].refreshRate == 60 && Screen.resolutions[i].width%16 == 0 && Screen.resolutions[i].height % 9 == 0)
            {
                resolutionList.Add(Screen.resolutions[i]);
            }
        }

        int optionNum = 0;
        foreach (Resolution item in resolutionList)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.width + " X " + item.height + " " + item.refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();
        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNUM = x;
    }
    public void FullScreenBtn()
    {
        screenMode = fullscreenBtn.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public  void OkRtnClick()
    {
        SetResolution(resolutionList[resolutionNUM].width,
            resolutionList[resolutionNUM].height);
    }


    /* 해상도 설정하는 함수 */
    public void SetResolution(int deviceWidth, int deviceHeight)
    {
        int setWidth = deviceWidth-(deviceWidth % 16);
        int setHeight = deviceHeight-(deviceHeight % 9);
        //int setWidth = 1920;
        //int setHeight = 1080;
        //int deviceWidth = Screen.width; // 기기 너비 저장
        //int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), screenMode); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }
}
