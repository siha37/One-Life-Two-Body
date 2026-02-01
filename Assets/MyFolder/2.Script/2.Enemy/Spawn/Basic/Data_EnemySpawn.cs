using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;


public class Data_EnemySpawn : MonoBehaviour
{
    public enum SpanwType { Random =0, DirectionRandom=1, Clockwise=2, CounterClockwise=3 }
    public enum SpawnDirection { UP=0, DOWN=1, LEFT=2, RIGHT =3 }

    [Serializable]
    public struct EnemySpawn_DATA_Property
    {

        public int Number;
        public int M;
        public int S;
        public List<SpanwType> SpawnTypes;
        public List<SpawnDirection> spawnDirections;
        public List<int> EnemyType;
        public List<int> EnemyCount;
        public EnemySpawn_DATA_Property(string _Number,string _M, string _S,string _SpawnType,string _Direction, string _EnemyType, string _EnemyCount)
        {
            Number = int.Parse(_Number);
            M = int.Parse(_M);
            S = int.Parse(_S);
            
            char sp = '|';
            string[] TempSpanwType = _SpawnType.Split(sp);
            SpawnTypes = new List<SpanwType>();
            for(int i = 0;i < TempSpanwType.Length;i++)
            {
                SpawnTypes.Add((SpanwType)(int.Parse(TempSpanwType[i])));
            }
            string[] TempSpawnDirections = _Direction.Split(sp);
            spawnDirections = new List<SpawnDirection>();
            for(int i=0;i < TempSpawnDirections.Length;i++)
            {
                spawnDirections.Add((SpawnDirection)(int.Parse(TempSpawnDirections[i])));
            }
            string[] TempType =_EnemyType.Split(sp);
            EnemyType = new List<int>();
            for (int i = 0; i < TempType.Length; i++)
            {
                EnemyType.Add(int.Parse(TempType[i]));
            }
            string[] TempCount = _EnemyCount.Split(sp);
            EnemyCount = new List<int>();
            for (int i = 0; i < TempCount.Length; i++)
            {
                EnemyCount.Add(int.Parse(TempCount[i]));
            }
        }
    }

    [SerializeField] public List<EnemySpawn_DATA_Property> enemySpawn_DATA_s = new List<EnemySpawn_DATA_Property>();

    private void Awake()
    {
        DataLoad();
    }
    private void DataLoad()
    {
        List<Dictionary<string, object>> Dic = CSVReader.Read("Data/ENEMY/SPAWN/Enemy_Spawn_Data");
        for(int i=0;i<Dic.Count;i++)
        {
            EnemySpawn_DATA_Property data = new EnemySpawn_DATA_Property(Dic[i]["Number"].ToString(), Dic[i]["M"].ToString(), Dic[i]["S"].ToString(), Dic[i]["SpawnType"].ToString(), Dic[i]["Direction"].ToString(), Dic[i]["EnemyType"].ToString(), Dic[i]["EnemyCount"].ToString());
            enemySpawn_DATA_s.Add(data);
        }
    }
}
