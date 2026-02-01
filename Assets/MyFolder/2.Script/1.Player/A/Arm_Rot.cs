using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Arm_Rot : MonoBehaviour
{
    Vector2  mouse;
    Transform P_position;

    [SerializeField] SkeletonAnimation skeleton;
    Spine.Bone bone;
    Spine.Bone IK_bone;

    private void Awake()
    {
        P_position = this.transform.parent;
        IK_bone = skeleton.skeleton.FindBone("shot_arm_IK");
    }
    private void Update()
    {
        if(Time.timeScale != 0)
        {
            if(bone != null)
            {
                transform.position = bone.GetWorldPosition(skeleton.transform);
                Vector3 direction = IK_bone.GetWorldPosition(skeleton.transform)- transform.position;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                transform.rotation = angleAxis;
            }
        }
    }

    public void PivotBone_Set(string bonename)
    {
        bone = skeleton.skeleton.FindBone(bonename);
    }
}
