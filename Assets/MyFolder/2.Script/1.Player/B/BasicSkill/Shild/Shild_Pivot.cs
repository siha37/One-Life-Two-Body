using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild_Pivot : MonoBehaviour
{
    private P_Move p_move;
    private Vector3 TargetRot_Nomal;
    private float h, v;

    [SerializeField] private Transform Shild_Image;
    [SerializeField] private Sprite[] Shild_sprites;
    private int OrderNum;
    private int SpriteNUM;

    private void Start()
    {
        p_move = transform.parent.GetComponent<P_Move>();
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            if (h == 0 && v == 0)
            {
            }
            else
            {
                switch (h)
                {
                    case 1:
                        if (v == -1)
                        {
                            OrderNum = 1;
                            SpriteNUM = 3;
                        }
                        else if (v == 1)
                        {
                            OrderNum = 0;
                            SpriteNUM = 1;
                        }
                        else
                        {
                            OrderNum = 1;
                            SpriteNUM = 2;
                        }
                        break;
                    case -1:
                        if (v == -1)
                        {
                            OrderNum = 1;
                            SpriteNUM = 5;
                        }
                        else if (v == 1)
                        {
                            OrderNum = 0;
                            SpriteNUM = 7;
                        }
                        else
                        {
                            OrderNum = 1;
                            SpriteNUM = 6;
                        }
                        break;
                    case 0:
                        if (v == -1)
                        {
                            OrderNum = 1;
                            SpriteNUM = 4;
                        }
                        else if (v == 1)
                        {
                            OrderNum = 0;
                            SpriteNUM = 0;
                        }
                        break;
                    default:
                        break;
                }
                TargetRot_Nomal = new Vector3(h, v, 0);
                transform.rotation = Quaternion.FromToRotation(Vector3.up, TargetRot_Nomal);
                Shild_Image.rotation = Quaternion.identity;
                Shild_Image.GetComponent<SpriteRenderer>().sprite = Shild_sprites[SpriteNUM];
                Shild_Image.GetComponent<SpriteRenderer>().sortingOrder = OrderNum;
            }
        } 
    }
}
