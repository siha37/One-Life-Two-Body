using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeCollectionDestory : MonoBehaviour
{
    private void LateUpdate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).childCount <= 0)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
