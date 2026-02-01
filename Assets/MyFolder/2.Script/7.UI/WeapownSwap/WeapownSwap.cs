using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeapownSwap : MonoBehaviour
{
    [SerializeField] private RectTransform ListPosition;
    [SerializeField] private List<Image> Weapown_Image;

    [SerializeField] private int NowWeapown_NUM;
    public int Weapown_NUM_Change { get { return NowWeapown_NUM; } set { NowWeapown_NUM = value; Alpha(); } }
    //NowWeapown_NUM 과 비교하여 현재 슬롯 위치가 제대로 존재하는지 확인 하는 int
    [SerializeField] private int SlotPosChack_NUM;
    private float StartPosition = 90;
    private float Scalling = -90;
    [SerializeField] private float Speed;
    [SerializeField] private float P_M_Alpha = 0.3f;

    [SerializeField] private GameObject ImageSlot_OBJ;


    public void ImageCreate(Sprite _image,int TargetNUM)
    {
        Image new_Image = Instantiate(ImageSlot_OBJ, Vector3.zero, Quaternion.identity, ListPosition.transform).GetComponent<Image>();
        new_Image.sprite = _image;
        Weapown_Image.Add(new_Image);
    }
    public void Start_Set()
    {
        Weapown_NUM_Change = 0;
        ListPosition.anchoredPosition =PosSet();
    }

    private Vector2 PosSet()
    {
        return new Vector2(StartPosition + (Scalling * NowWeapown_NUM), 0);
    }
    private float PosSet_X()
    {
        return StartPosition + (Scalling * NowWeapown_NUM);
    }
    private Vector2 Lerp_PosSet(float BeforePos_X,float TargetPos_X)
    {
        return new Vector2(Mathf.Lerp(BeforePos_X, TargetPos_X, Time.deltaTime * Speed), 0);
    }
    private void Update()
    {
        ListPosition.anchoredPosition = Lerp_PosSet(ListPosition.anchoredPosition.x, PosSet_X());
    }

    private void Alpha()
    {
        if (Weapown_Image.Count > 0)
        {
            StartCoroutine(Lerp_Alpha(Weapown_Image[NowWeapown_NUM], 1));
            for (int i = NowWeapown_NUM - 1; i >= 0; i--)
            {
                int num = NowWeapown_NUM - i;
                StartCoroutine(Lerp_Alpha(Weapown_Image[i], 1 - P_M_Alpha * num));
            }
            for (int i = NowWeapown_NUM + 1; i <= Weapown_Image.Count - 1; i++)
            {
                int num = i - NowWeapown_NUM;
                StartCoroutine(Lerp_Alpha(Weapown_Image[i], 1 - P_M_Alpha * num));
            } 
        }
        else
        {
            Invoke("Alpha", 0.1f);
        }
    }
    IEnumerator Lerp_Alpha(Image _image,float TargetAlpha)
    {
        float nowAlpha = _image.color.a;
        for(float i =0;i<=1;i+=0.05f)
        {
            Color Targetcolor = new Color(_image.color.r, _image.color.g, _image.color.b,Mathf.Lerp(nowAlpha,TargetAlpha,i));
            _image.color = Targetcolor;
            yield return YieldInstructionCache.WaitForSeconds(0.02f);
        }
    }
}
