using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickMiniGame : MonoBehaviour
{
    [ReadOnly] private bool Active;
    [Header("OBJ")]
    [SerializeField] GameObject ClickButton;

    [Header("Random POS")]
    [SerializeField] float MaxX;
    [SerializeField] float MaxY;
    [SerializeField] float MinX;
    [SerializeField] float MinY;

    [Header("Time")]
    [SerializeField] float Spawn_MaxTime;
    [SerializeField] float Spawn_MinTime;
    [ReadOnly] float Spawn_Random_Time;
    [ReadOnly] bool Spawn_Able =true;
    [Tooltip("생성된 버튼이 활성화하는 시간")]
    [SerializeField] float Button_Able_Time;

    [Header("R&L")]
    // 0 = L /  1 = R
    [SerializeField] int[] RnL = new int[2];
    [Header("Click")]
    [SerializeField] GraphicRaycaster gr;
    PointerEventData ped;
    ClickButton_OBJ chackUI;

    [Header("Score")]
    public int Score=0;

    public void Update()
    {
        if(Active)
        {
            if (Spawn_Able)
            {
                Spawning();
            }

            chackUI = GraphicRayCasting();
            if(chackUI != null)
            {
                if (Input.GetMouseButtonDown(0) && 0 == chackUI.TYPE)
                {
                    chackUI.Success();
                }
                else if (Input.GetMouseButtonDown(1) && 1 == chackUI.TYPE)
                {
                    chackUI.Success();
                }
                else if(Input.GetMouseButtonDown(0) && 1 == chackUI.TYPE || Input.GetMouseButtonDown(1) && 0 == chackUI.TYPE)
                {
                    chackUI.Fail();
                }
            }
        }
    }
    private ClickButton_OBJ GraphicRayCasting()
    {
        ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        ClickButton_OBJ obj = null;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        if (results.Count <= 0) return null;

        // 이벤트 처리부분
        if (results[0].gameObject.tag == "MINIGAMEBUTTON")
        {
            obj = results[0].gameObject.transform.parent.GetComponent<ClickButton_OBJ>();
        }
        return obj;
    }
    private void Spawning()
    {
        Vector3 targetPos = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), 0);
        int type = RnL[Random.Range(0, RnL.Length)];
        GameObject obj= Instantiate(ClickButton,Vector3.zero, Quaternion.identity, this.transform);
        obj.GetComponent<RectTransform>().anchoredPosition = targetPos;
        obj.GetComponent<ClickButton_OBJ>().Setting(type,this, Button_Able_Time);
        //time reset
        Spawn_Random_Time = Random.Range(Spawn_MinTime, Spawn_MaxTime);
        Spawn_Able = false;
        StartCoroutine(SpawnColltime(Spawn_Random_Time));
    }
    IEnumerator SpawnColltime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Spawn_Able = true;
    }
    public void Start_Game()
    {
        Active = true;
    }
    public void End_Game()
    {
        Active =false;
        //for (int i = transform.childCount-1; i >- 0; i--)
        //{
        //    Destroy(transform.GetChild(i));
        //}
        transform.DetachChildren();
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
