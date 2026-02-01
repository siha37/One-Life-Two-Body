using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextAnimation : MonoBehaviour
{
    Animator anim;

    [SerializeField] private string TitleText;
    [SerializeField] private string RebootText;
    [SerializeField] private string CodeText;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI reboot;
    [SerializeField] private TextMeshProUGUI code;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Rebooting()
    {
        anim.SetTrigger("Reboot");
    }
    public void SceneMove()
    {
        SceneManager.LoadScene("Title");
    }
}
