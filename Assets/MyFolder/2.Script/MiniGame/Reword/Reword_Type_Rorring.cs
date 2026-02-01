using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reword_Type_Rorring : MonoBehaviour
{
    RewordManager manager;

    [SerializeField] RectTransform Rorring_Tf;
    int Type_NUM = 6;
    [SerializeField] int[] Type_Array;
    [SerializeField] float Y = 150;

    public void Rorring_Start()
    {
        manager = transform.parent.parent.GetComponent<RewordManager>();    
        Array_Set();
        StartCoroutine(Rorring());
    }
    private void Array_Set()
    {
        Type_NUM = Rorring_Tf.transform.childCount;
        Type_Array = new int[Type_NUM];
        for(int i = 0; i < Type_NUM; i++)
        {
            Type_Array[i] = i;
        }
    }
    IEnumerator Rorring()
    {
        int RandomCount = Random.Range(0,7);
        int Count =  ((Type_NUM - 1)*2) * 3 + (RandomCount *3);
        for (int i =0; i< Count;i++)
        {
            Rorring_Tf.anchoredPosition += new Vector2(0, 50);
            if(Rorring_Tf.anchoredPosition.y >= 150*(Type_NUM-1))
            {
                Rorring_Tf.anchoredPosition = new Vector2(0,0);
            }
            yield return new WaitForSecondsRealtime(0.05f);
        }
        manager.Type_Get((RewordManager.RewordType)Type_Chack());
    }

    private int Type_Chack()
    {
        for (int i = 0; i < Type_Array.Length; i++)
        {
            if(Type_Array[i] == Rorring_Tf.anchoredPosition.y / 150)
            {
                return i;
            }
        }
        return 0;
    }

}
