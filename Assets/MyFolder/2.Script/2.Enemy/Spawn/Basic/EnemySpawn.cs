 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    Gamemanager MyChar;

    [Tooltip("데이터 베이스")]
    private Data_EnemySpawn spawn_Data;
    [Tooltip("적들 프리팹 리스트")]
    [SerializeField] public GameObject[] EnemyPrefabs = new GameObject[3];
    [Tooltip("현 테이블의 적용된 데이터 구조체")]
    [ReadOnly][SerializeField] Data_EnemySpawn.EnemySpawn_DATA_Property nowData;
    [Tooltip("현재 구조체 리스트의 몇 번인지 확인")]
    [ReadOnly] [SerializeField] private int NowListNUM = -1;
    [Tooltip("생성된 적을 묶어둘 오브젝트")]
    [SerializeField] private Transform EnemyCollection;
    [Tooltip("이전의 생성된 적들을 자심 담아둘 오브젝트")]
    [SerializeField] private Transform BeforeEnemyCollection;
    [Tooltip("생성 적 하위 관리 리스트")]
    [SerializeField] private List<Transform> EnemyDonwCollection;
    [Tooltip("생성 좌표 기준 Collider")]
    [SerializeField] public BoxCollider2D Box;
    public float[] BoxXY = new float[2];
    private void Start()
    {
        MyChar = Gamemanager.myChar;
        spawn_Data = GetComponent<Data_EnemySpawn>();
        BoxXY[0] = Box.size.x*0.5f;
        BoxXY[1] = Box.size.y*0.5f;
    }
    private void LateUpdate()
    {
        if (spawn_Data.enemySpawn_DATA_s.Count > NowListNUM+1)
        {
            if (MyChar.CurrentTIMER >= TimerCaclulation(NowListNUM+1))
            {
                nowData = spawn_Data.enemySpawn_DATA_s[++NowListNUM];
                Spawn();
            } 
        }
    }
    private int TimerCaclulation(int NUM)
    {
        Data_EnemySpawn.EnemySpawn_DATA_Property data = spawn_Data.enemySpawn_DATA_s[NUM];
        return data.M * 60 + data.S;
    }

    void Spawn()
    {
        if(EnemyDonwCollection.Count != 0)
        {
            for (int i = EnemyDonwCollection.Count-1; i >= 0; i--)
            {
                EnemyDonwCollection[i].parent = BeforeEnemyCollection;
                EnemyDonwCollection.RemoveAt(i);   
            }
        }
        for (int i = 0; i < nowData.EnemyType.Count; i++)
        {
            EnemyDonwCollection.Add(new GameObject(NowListNUM.ToString() + "_" + i.ToString()).transform);
            EnemyDonwCollection[i].transform.parent = EnemyCollection;
            switch (nowData.SpawnTypes[i])
            {
                case Data_EnemySpawn.SpanwType.Random:
                    StartCoroutine(RandomSpawn(NowListNUM, i, EnemyPrefabs[nowData.EnemyType[i]], nowData.EnemyCount[i], EnemyDonwCollection[i]));
                    break;
                case Data_EnemySpawn.SpanwType.DirectionRandom:
                    StartCoroutine(DirectionRandom(NowListNUM,i,EnemyPrefabs[nowData.EnemyType[i]],nowData.EnemyCount[i],EnemyDonwCollection[i],nowData.spawnDirections[i]));
                    break;
                case Data_EnemySpawn.SpanwType.Clockwise:
                    StartCoroutine(Clockwise(NowListNUM, i, EnemyPrefabs[nowData.EnemyType[i]], nowData.EnemyCount[i], EnemyDonwCollection[i], IntervalSet(nowData.EnemyCount[i],true),270));
                    break;
                case Data_EnemySpawn.SpanwType.CounterClockwise:
                    StartCoroutine(Clockwise(NowListNUM, i, EnemyPrefabs[nowData.EnemyType[i]], nowData.EnemyCount[i], EnemyDonwCollection[i], IntervalSet(nowData.EnemyCount[i],false),270));
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator RandomSpawn(int ListNUM,int TypeNUM,GameObject Target,int EnemyCount,Transform parent)
    {
        //GameObject target = 
        if (ListNUM != NowListNUM)
            yield break;


        float X,Y;
        if (EnemyDonwCollection[TypeNUM].childCount < EnemyCount)
        {
            int XorY = Random.Range(0,2);
            if(XorY == 0)
            {
                int MM = Random.Range(0, 2);
                if(MM == 0)
                {
                    X = BoxXY[0];
                }
                else
                {
                    X = -BoxXY[0];
                }
                Y = Random.Range(-BoxXY[1], BoxXY[1]);
            }
            else
            {
                int MM = Random.Range(0, 2);
                if (MM == 0)
                {
                    Y = BoxXY[1];
                }
                else
                {
                    Y = -BoxXY[1];
                }
                X = Random.Range(-BoxXY[0], BoxXY[0]);
            }
           Instantiate(Target, new Vector3(X+Box.transform.position.x, Y+Box.transform.position.y, 0), Quaternion.identity, parent);
        }

        yield return YieldInstructionCache.WaitForSeconds(0.1f);

        StartCoroutine(RandomSpawn(ListNUM, TypeNUM, Target,EnemyCount, parent));
    }
    IEnumerator DirectionRandom(int ListNUM,int TypeNUM,GameObject Target,int EnemyCount,Transform parent,Data_EnemySpawn.SpawnDirection Dir)
    {
        if (ListNUM != NowListNUM)
            yield break;

        float X=0, Y=0;
        if (EnemyDonwCollection[TypeNUM].childCount < EnemyCount)
        {
            switch (Dir)
            {
                case Data_EnemySpawn.SpawnDirection.UP:
                    Y = BoxXY[1];
                    X = Random.Range(-BoxXY[0], BoxXY[0]);
                    break;
                case Data_EnemySpawn.SpawnDirection.DOWN:
                    Y = -BoxXY[1];
                    X = Random.Range(-BoxXY[0], BoxXY[0]);
                    break;
                case Data_EnemySpawn.SpawnDirection.LEFT:
                    X = -BoxXY[0];
                    Y = Random.Range(-BoxXY[1], BoxXY[1]);
                    break;
                case Data_EnemySpawn.SpawnDirection.RIGHT:
                    X = BoxXY[0];
                    Y = Random.Range(-BoxXY[1], BoxXY[1]);
                    break;
                default:
                    break;
            }
            Instantiate(Target, new Vector3(X+Box.transform.position.x, Y+Box.transform.position.y, 0), Quaternion.identity, parent);
        }

        yield return YieldInstructionCache.WaitForSeconds(0.1f);
        StartCoroutine(DirectionRandom(ListNUM, TypeNUM, Target,EnemyCount,parent, Dir));
    }
    float IntervalSet(int EnemyCount,bool Right)
    {
        // 적들 마다의 거리를 총 적의 숫자로 비교
        float i = 360/(float)EnemyCount;
        if(!Right)
        {
            i *= -1;
        }
        return i;
    }
    IEnumerator Clockwise(int ListNUM, int TypeNUM, GameObject Target, int EnemyCount, Transform parent,float interval, float Angle)
    {
        if (ListNUM != NowListNUM)
            yield break;

        float X =0, Y = 0;

        if (EnemyDonwCollection[TypeNUM].childCount < EnemyCount)
        {
            X = Mathf.Cos(Angle) * BoxXY[1];
            Y = Mathf.Sin(Angle) * BoxXY[1];
            Instantiate(Target, new Vector3(X+Box.transform.position.x,Y+Box.transform.position.y, 0), Quaternion.identity, parent);
            Angle += interval;
        }
        yield return YieldInstructionCache.WaitForSeconds(0.1f);
        StartCoroutine(Clockwise(ListNUM, TypeNUM, Target, EnemyCount, parent, interval,Angle));
    }
}
