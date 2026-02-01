using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraF : MonoBehaviour
{
    public GameObject Player = null;

    private Vector3 PlayerPosition;


    private new Camera camera;

    public float MoveSpeed;
    public float minusY;
    public float minusZ;


    private void Awake()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("P_BODY");
        }
    }

    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    private void Update()
    {
        if (Player.gameObject != null)
        {
            PlayerPosition.Set(Player.transform.position.x, Player.transform.position.y - minusY, this.transform.position.z);
            Vector3 target = Vector3.Lerp(this.transform.position, PlayerPosition, MoveSpeed * Time.deltaTime);
            camera.transform.position = target;

        }
    }
}
