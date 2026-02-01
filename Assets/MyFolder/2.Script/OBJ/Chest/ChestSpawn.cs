using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawn : MonoBehaviour
{
    InGameTimer timer;
    Gamemanager myChar;

    [Header("TargetTime")]
    [SerializeField] int M;
    [SerializeField] int S;
    [ReadOnly][SerializeField] int Counting;
    [ReadOnly][SerializeField] int Chack_Time =0;

    [Space(15)]
    [Header("Object")]
    [SerializeField] GameObject Chest_Prefab;
    [SerializeField] GameObject miniGame_;

    [Space(10)]
    [Header("Target")]
    [SerializeField] Transform camera_pos;

    [Space(10)]
    [Header("SpawnData")]
    [SerializeField] float Radius;

    private void Start()
    {
        myChar = Gamemanager.myChar;
        Counting = 0;
        camera_pos = Camera.main.transform;
    }

    private void Update()
    {
        if((int)myChar.CurrentTIMER == TimerCaclulation(Counting+1) && Chack_Time != (int)myChar.CurrentTIMER)
        {
            Chack_Time = (int)myChar.CurrentTIMER;
            Counting++;
            Spawning();
        }
    }
    private int TimerCaclulation(int NUM)
    {
        return (M * 60 + S)* NUM;
    }
    private void Spawning()
    {
       Chest_OBJ obj = Instantiate(Chest_Prefab, Spawn_Pos_Caculation(), Quaternion.identity,this.transform).GetComponent<Chest_OBJ>();
        obj.Chest_Set(miniGame_);
    }
    private Vector3 Spawn_Pos_Caculation()
    {
        float Angle = Random.Range(0,360);
        Vector3 newDir = new Vector3(Mathf.Cos(Angle),Mathf.Sin(Angle),0);
        newDir = newDir * Radius;
        return new Vector3(camera_pos.position.x, camera_pos.position.y, 0) + newDir;
    }
}
