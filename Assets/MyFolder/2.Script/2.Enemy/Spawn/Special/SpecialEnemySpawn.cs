using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemySpawn : MonoBehaviour
{
    Gamemanager myChar;

    EnemySpawn BasicSpawn;
    Data_SpecialEnemySpawn spawn_Data;
    [ReadOnly] [SerializeField] Data_SpecialEnemySpawn.EnemySpawn_DATA_Property nowData;
    [SerializeField] private Transform SpecialEnemyCollection;
    [SerializeField] Material OutLineShader;

    [SerializeField][ReadOnly] int NowListNUM=-1;
    private void Start()
    {
        NowListNUM = -1;
        myChar = Gamemanager.myChar;
        BasicSpawn = GetComponent<EnemySpawn>();
        spawn_Data = GetComponent<Data_SpecialEnemySpawn>();
    }
    private void LateUpdate()
    {
        if (spawn_Data.enemySpawn_DATA_s.Count > NowListNUM + 1)
        {
            if (myChar.CurrentTIMER >= TimerCaclulation(NowListNUM + 1))
            {
                nowData = spawn_Data.enemySpawn_DATA_s[++NowListNUM];
                Spawn();
            }
        }
    }
    private int TimerCaclulation(int NUM)
    {
        Data_SpecialEnemySpawn.EnemySpawn_DATA_Property data = spawn_Data.enemySpawn_DATA_s[NUM];
        return data.M * 60 + data.S;
    }

    void Spawn()
    {
        switch (nowData.SpawnTypes)
        {
            case Data_SpecialEnemySpawn.SpanwType.OnlyOne:
                OnlyOne();
                break;
            case Data_SpecialEnemySpawn.SpanwType.BossAndMiniMOB:
                BossAndMiniMob();
                break;
            case Data_SpecialEnemySpawn.SpanwType.Circle:
                Circle();
                break;
            default:
                break;
        }
    }
    private void OnlyOne()
    {
        float X=0, Y=0;
        switch (nowData.spawnDirections)
        {
            case Data_SpecialEnemySpawn.SpawnDirection.UP:
                X = Random.Range(-BasicSpawn.BoxXY[0], BasicSpawn.BoxXY[0]);
                Y = BasicSpawn.BoxXY[1];
                break;
            case Data_SpecialEnemySpawn.SpawnDirection.DOWN:
                X = Random.Range(-BasicSpawn.BoxXY[0], BasicSpawn.BoxXY[0]);
                Y = -BasicSpawn.BoxXY[1];
                break;
            case Data_SpecialEnemySpawn.SpawnDirection.LEFT:
                X = -BasicSpawn.BoxXY[1];
                Y = Random.Range(-BasicSpawn.BoxXY[1], BasicSpawn.BoxXY[1]);
                break;
            case Data_SpecialEnemySpawn.SpawnDirection.RIGHT:
                X = BasicSpawn.BoxXY[1];
                Y = Random.Range(-BasicSpawn.BoxXY[1], BasicSpawn.BoxXY[1]);
                break;
            default:
                break;
        }
        Enemy_Status status = Instantiate(BasicSpawn.EnemyPrefabs[nowData.EnemyType[0]],new Vector3(X,Y,0)+ BasicSpawn.transform.position, Quaternion.identity,SpecialEnemyCollection).GetComponent<Enemy_Status>();
        status.Status_SET(nowData.status_reset.HP, nowData.status_reset.Speed, nowData.status_reset.Size, nowData.status_reset.Damge,nowData.Exp_Amount);
        status.GetComponent<SpriteRenderer>().material = OutLineShader;
    }
    private void BossAndMiniMob()
    {
        float X = 0, Y = 0;
        switch (nowData.spawnDirections)
        {
            case Data_SpecialEnemySpawn.SpawnDirection.UP:
                X = Random.Range(-BasicSpawn.BoxXY[0], BasicSpawn.BoxXY[0]);
                Y = BasicSpawn.BoxXY[1];
                break;
            case Data_SpecialEnemySpawn.SpawnDirection.DOWN:
                X = Random.Range(-BasicSpawn.BoxXY[0], BasicSpawn.BoxXY[0]);
                Y = -BasicSpawn.BoxXY[1];
                break;
            case Data_SpecialEnemySpawn.SpawnDirection.LEFT:
                X = -BasicSpawn.BoxXY[1];
                Y = Random.Range(-BasicSpawn.BoxXY[1], BasicSpawn.BoxXY[1]);
                break;
            case Data_SpecialEnemySpawn.SpawnDirection.RIGHT:
                X = BasicSpawn.BoxXY[1];
                Y = Random.Range(-BasicSpawn.BoxXY[1], BasicSpawn.BoxXY[1]);
                break;
            default:
                break;
        }
        Enemy_Status main_enemy = Instantiate(BasicSpawn.EnemyPrefabs[nowData.EnemyType[0]], new Vector3(X, Y, 0)+ BasicSpawn.transform.position, Quaternion.identity, SpecialEnemyCollection).GetComponent<Enemy_Status>();
        main_enemy.Status_SET(nowData.status_reset.HP, nowData.status_reset.Speed, nowData.status_reset.Size, nowData.status_reset.Damge, nowData.Exp_Amount);
        main_enemy.GetComponent<SpriteRenderer>().material = OutLineShader;
        for (int i=0;i<nowData.EnemyCount[1];i++)
        {
            X = Random.Range(main_enemy.transform.position.x-3, main_enemy.transform.position.x+3);
            Y = Random.Range(main_enemy.transform.position.y-3,main_enemy.transform.position.y+3);
            Enemy_Status status = Instantiate(BasicSpawn.EnemyPrefabs[nowData.EnemyType[1]], new Vector3(X, Y, 0)+ BasicSpawn.transform.position, Quaternion.identity, SpecialEnemyCollection).GetComponent<Enemy_Status>();
            status.Status_SET(nowData.status_reset.HP, nowData.status_reset.Speed, nowData.status_reset.Size, nowData.status_reset.Damge);
        }

    }

    private void Circle()
    {
        float X = 0, Y = 0;
        float angle =0 , Add_angle =0;
        Add_angle =  360/ nowData.EnemyCount[0];
        for(int i=0;i<nowData.EnemyCount[0];i++)
        {
            X = Mathf.Cos(angle) * BasicSpawn.BoxXY[0];
            Y = Mathf.Sin(angle) * BasicSpawn.BoxXY[0];
            Instantiate(BasicSpawn.EnemyPrefabs[nowData.EnemyType[0]], new Vector3(X, Y, 0)+ BasicSpawn.transform.position, Quaternion.identity, SpecialEnemyCollection);
            angle += Add_angle;
        }
    }
}
