using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSet : MonoBehaviour
{
    Gamemanager myChar;
    private void Start()
    {
        myChar = Gamemanager.myChar;
        StartCoroutine(LogoEnd());
    }

    IEnumerator LogoEnd()
    {
        yield return YieldInstructionCache.WaitForSeconds(2);
        SceneManager.LoadScene("Title");
    }

    public void FastEnd()
    {
        SceneManager.LoadScene("Title");
    }
}
