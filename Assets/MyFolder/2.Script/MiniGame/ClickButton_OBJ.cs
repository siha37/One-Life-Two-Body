using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickButton_OBJ : MonoBehaviour
{
    public int TYPE;
    ClickMiniGame system;
    float AbleTime;
    float Currenty_AbleTime;
    bool TimeOver =false;
    bool Over =false;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image BackGround;
    Animator anim;
    private void Update()
    {
        if(Currenty_AbleTime < AbleTime)
        {
            Currenty_AbleTime += Time.unscaledDeltaTime;
            float fill = Currenty_AbleTime / AbleTime;
            BackGround.GetComponent<RectTransform>().localScale = new Vector3(fill, fill, 0);
        }
        else if(!TimeOver)
        {
            TimeOver = true;
            Fail();
        }
    }
    public void Setting(int type,ClickMiniGame _system,float _ableTime)
    {
        TYPE = type;
        AbleTime = _ableTime;
        Currenty_AbleTime = 0;
        anim =GetComponent<Animator>();
        anim.SetFloat("TYPE", type);
        switch (type)
        { 
            case 0:
                text.text = "L";
                break;
            case 1:
                text.text = "R";
                break;
        }
        system = _system;
    }

    public void Success()
    {
        if (Over) return;
        Over = true;
        system.Success();
        anim.SetTrigger("Success");
    }
    public void Fail()
    {
        if (Over) return;
        Over = true;
        system.Fail();
        anim.SetTrigger("Fail");
    }

    public void Object_Destory()
    {
        Destroy(gameObject);
    }
}
