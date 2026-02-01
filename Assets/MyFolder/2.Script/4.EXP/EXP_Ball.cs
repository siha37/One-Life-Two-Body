using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP_Ball : MonoBehaviour
{
    Gamemanager myChar;

    Transform Target;
    Transform tf;

    //Ball Data
    public bool Move_Able;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 dir;
    public float EXP_Amount;
    private bool Changing_Dir = true;
    private bool BackDir = true;

    private void Start()
    {
        myChar = Gamemanager.myChar;
        Target = myChar.P2.transform;
        tf = transform;
    }
    public void Amount_Input(float amount)
    {
        EXP_Amount = amount;
    }
    private void Update()
    {
        if(Move_Able)
        {
            if(BackDir)
            {
                if(Changing_Dir)
                {
                    Changing_Dir = false;
                    Invoke("TrunDir",0.2f);
                }
                BackMoveON();
            }
            else
            {
                MoveON();
            }
        }
    }
    private void TrunDir()
    {
        BackDir = false;
    }
    public void BackMoveON()
    {
        dir = Target.position - tf.position;
        Vector3 target = dir.normalized * speed *Time.deltaTime * -1 /2;
        tf.position += target;

        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void MoveON()
    {
        dir = Target.position - tf.position;
        Vector3 target = dir.normalized * speed * Time.deltaTime;
        tf.position += target;

        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
