using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP_Spawn : MonoBehaviour
{
    Gamemanager myChar;
    GameObject EXPS;

    [SerializeField] private GameObject Exp_obj;
    private void Awake()
    {
        myChar = Gamemanager.myChar;
        myChar.EXPS = this.gameObject;
    }
    private void Start()
    {
        EXPS = this.gameObject;
    }
    public void Roulette(float Amount,Vector3 Pos)
    {
        Spawn(Amount, Pos); //임시 배치
    }
    private void Spawn(float Amount, Vector3 Pos)
    {
        for (int i = 0; i < Amount; i++)
        {
            Vector3 target = Pos + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            GameObject ball = Instantiate(Exp_obj,target, Quaternion.identity, EXPS.transform);
            ball.GetComponent<EXP_Ball>().Amount_Input(1);
        }
    }
}
