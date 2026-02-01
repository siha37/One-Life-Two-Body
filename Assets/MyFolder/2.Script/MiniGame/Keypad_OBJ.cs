using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad_OBJ : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    KeyPadMiniGame system;
    Animator anim;
    public void Text_set(string _text ,KeyPadMiniGame _system)
    {
        anim =GetComponent<Animator>();
        system = _system;
        text.text = _text;
    }

    public void Success()
    {
        anim.SetTrigger("Success");
    }
    public void Fail()
    {
        anim.SetTrigger("Fail");
    }
}
