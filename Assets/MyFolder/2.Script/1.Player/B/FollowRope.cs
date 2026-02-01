using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRope : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private RopePysics ropePysics;
    [SerializeField] P2_Animation_Controll anim;
    [SerializeField] float UP_y;
    public float speed;

    private void Start()
    {
        lineRenderer = ropePysics.lineRenderer;
        anim =GetComponent<P2_Animation_Controll>();
    }

    [System.Obsolete]
    private void LateUpdate()
    {
        Vector3 targetPos = Vector3.Lerp(transform.position + new Vector3(0, UP_y, 0), lineRenderer.GetPosition(ropePysics.CurrentysegmentCount - 1), speed * Time.deltaTime) + new Vector3(0, -UP_y, 0);
        if (targetPos.x > transform.position.x)
        {
            anim.FlipX(true);
        }
        else if(targetPos.x < transform.position.x)
        {
            anim.FlipX(false);
        }
        if ((targetPos - this.transform.position).magnitude > 0)
        {
            anim.AnimationStatus(0, "MOVE", true);
        }
        else
        {
            anim.AnimationStatus(0, "IDLE", true);
        }
        this.transform.position = targetPos;
    }
}
