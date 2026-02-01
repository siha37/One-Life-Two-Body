using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadMiniGame : MonoBehaviour
{
    [SerializeField] bool Active;
    [SerializeField]string[] able_text = new string[6]; 
    List<string> KeyTexts = new List<string>();
    [SerializeField] GameObject Keypad_Obj;
    [SerializeField] Transform Keypad_Parent;
    List<GameObject> Keypad_List = new List<GameObject> ();
    int List_Num;
    int NowKeypad_Count;

    public int Score;

    private void Update()
    {
        if (Active)
        {
            string inputkey = Input.inputString;
            inputkey = inputkey.ToUpper();
            if (inputkey == KeyTexts[List_Num])
            {
                Success();
                Keypad_List[List_Num].GetComponent<Keypad_OBJ>().Success();
                KeyCount_UP();
            }
            else if (inputkey != "")
            {
                Fail();
                Keypad_List[List_Num].GetComponent<Keypad_OBJ>().Fail();
                KeyCount_UP();
            }
        }
    }
    public void Start_Game()
    {
        NowKeypad_Count = 4;
        Keypad_Set(NowKeypad_Count);
    }
    public void End_Game()
    {
        for (int i = 0; i < Keypad_List.Count; i++)
        {
            Destroy(Keypad_List[i]);
        }
        Active = false;
    }
    private void Keypad_Set(int pad_amount)
    {

        KeyTexts.Clear();
        if(Keypad_List.Count > 0)
        {
            for(int i= Keypad_List.Count-1; i>=0;i--)
            {
                Destroy(Keypad_List[i]);
            }
            Keypad_List.Clear();
        }
        for(int i=0;i<pad_amount;i++)
        {
            KeyTexts.Add(able_text[Random.Range(0, able_text.Length)]);
            Keypad_List.Add(Instantiate(Keypad_Obj, Vector3.zero, Quaternion.identity, Keypad_Parent));
            Keypad_List[i].GetComponent<Keypad_OBJ>().Text_set(KeyTexts[i],this);
        }
        List_Num = 0;
        Active = true;
        NowKeypad_Count++;
    }

    private bool KeyCount_UP()
    {
        if (KeyTexts.Count-1 == List_Num)
        {
            Keypad_Set(NowKeypad_Count);
            return false;
        }
        List_Num++;
        return true;
    }

    public void Success()
    {
        Score++;
    }
    public void Fail()
    {
        Score--;
    }
}
