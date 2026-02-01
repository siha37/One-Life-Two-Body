using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundIMAGE_RandomSet : MonoBehaviour
{
    [Header("Resource")]
    [SerializeField] private List<Sprite> FirstBackGround_Images = new List<Sprite> ();
    [SerializeField] private List<GameObject> BackGround_OBJ_Images = new List<GameObject> ();
    [SerializeField] private List<GameObject> Col_BackGround_OBJ_Images = new List<GameObject>();
    private int F_Count, Ba_OBJ_Count, Col_Ba_OBJ_Count;

    [Header("Active-OBJ")]
    [SerializeField] private SpriteRenderer[] FirstBackGround_sprite;
    [Tooltip("실 배경 오브젝트의 개수")]
    private int BackGround_OBJ_Count;

    private void Start()
    {
        F_Count = FirstBackGround_Images.Count;
        Ba_OBJ_Count = BackGround_OBJ_Images.Count;
        Col_Ba_OBJ_Count= Col_BackGround_OBJ_Images.Count;
        BackGround_OBJ_Count = FirstBackGround_sprite.Length;
        int RandomNUM = 0;
        for (int i = 0; i < BackGround_OBJ_Count; i++)
        {
            FirstBackGround_sprite[i].sprite = FirstBackGround_Images[Random.Range(0, F_Count)];
            if (Ba_OBJ_Count > 0)
            {
                RandomNUM = Random.Range(0, Ba_OBJ_Count);
                if (BackGround_OBJ_Images[RandomNUM] != null)
                {
                    Instantiate(BackGround_OBJ_Images[RandomNUM], FirstBackGround_sprite[i].transform);
                }
            }
            if (Col_Ba_OBJ_Count > 0)
            {
                RandomNUM = Random.Range(0, Col_Ba_OBJ_Count);
                if (Col_BackGround_OBJ_Images[RandomNUM] != null)
                {
                    Instantiate(Col_BackGround_OBJ_Images[RandomNUM], FirstBackGround_sprite[i].transform);
                }
            }
        }
    }
}
