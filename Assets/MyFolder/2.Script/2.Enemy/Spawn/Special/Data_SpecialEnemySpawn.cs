using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
public class Data_SpecialEnemySpawn : MonoBehaviour
{
    public enum SpanwType { OnlyOne = 0, BossAndMiniMOB = 1, Circle = 2}
    public enum SpawnDirection { UP = 0, DOWN = 1, LEFT = 2, RIGHT = 3 }

    [Serializable]
    public struct EnemyStatus_Reset
    {
        public float HP;
        public float Speed;
        public float Size;
        public float Damge;
        public EnemyStatus_Reset(float _hp,float _speed,float _size,float _damage)
        {
            HP = _hp;
            Speed = _speed;
            Size = _size;
            Damge = _damage;
        }
    }
    [Serializable]
    public struct EnemySpawn_DATA_Property
    {

        public int Number;
        public int M;
        public int S;
        public SpanwType SpawnTypes;
        public SpawnDirection spawnDirections;
        public List<int> EnemyType;
        public List<int> EnemyCount;
        public int Exp_Amount;
        public EnemyStatus_Reset status_reset;

        public EnemySpawn_DATA_Property(string _Number, string _M, string _S, string _SpawnType, string _Direction, string _EnemyType, string _EnemyCount,string _hp,string _speed,string _size,string _damage,string exp_Amount)
        {
            Number = int.Parse(_Number);
            M = int.Parse(_M);
            S = int.Parse(_S);

            SpawnTypes = (SpanwType)(int.Parse(_SpawnType));
            spawnDirections = (SpawnDirection)(int.Parse(_Direction));

            char sp = '|';

            string[] TempType = _EnemyType.Split(sp);
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
            status_reset = new EnemyStatus_Reset(float.Parse(_hp),float.Parse(_speed),float.Parse(_size),float.Parse(_damage));

            Exp_Amount = int.Parse(exp_Amount);
        }
    }

    [SerializeField] public List<EnemySpawn_DATA_Property> enemySpawn_DATA_s = new List<EnemySpawn_DATA_Property>();

    private void Awake()
    {
        DataLoad();
    }
    private void DataLoad()
    {
        List<Dictionary<string, object>> Dic = CSVReader.Read("Data/ENEMY/SPAWN/Special_Enemy_Spawn_Data");
        for (int i = 0; i < Dic.Count; i++)
        {
            EnemySpawn_DATA_Property data = new EnemySpawn_DATA_Property(Dic[i]["Number"].ToString(), Dic[i]["M"].ToString(), Dic[i]["S"].ToString(), Dic[i]["SpawnType"].ToString(), Dic[i]["Direction"].ToString(), Dic[i]["EnemyType"].ToString(), Dic[i]["EnemyCount"].ToString(),Dic[i]["HP"].ToString(),Dic[i]["Speed"].ToString(),Dic[i]["Size"].ToString(),Dic[i]["Damage"].ToString(),Dic[i]["Exp_Amount"].ToString());
            enemySpawn_DATA_s.Add(data);
        }
    }
}
