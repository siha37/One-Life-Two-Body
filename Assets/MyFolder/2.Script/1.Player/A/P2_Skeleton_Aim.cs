using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class P2_Skeleton_Aim : MonoBehaviour
{
    SkeletonAnimation skeleton;
    Camera cam;
    [SpineBone]
    public string boneName;
    Spine.Bone bone;
    public Vector3 targetPosition;

    private void Start()
    {
        cam = Camera.main;
        skeleton = GetComponent<SkeletonAnimation>();
        bone = skeleton.skeleton.FindBone(boneName);
        var aimTrack = skeleton.AnimationState.SetAnimation(1, "aim", true);
        aimTrack.AttachmentThreshold = 1f;
        aimTrack.MixDuration = 0f;
    }

    [System.Obsolete]
    private void Update()
    {
        var mousePosition = Input.mousePosition;
        var worldMousePosition = cam.ScreenToWorldPoint(mousePosition);
        var skeletonSpacePoint = skeleton.transform.InverseTransformPoint(worldMousePosition);
        skeletonSpacePoint.x *= skeleton.Skeleton.ScaleX;
        skeletonSpacePoint.y *= skeleton.Skeleton.ScaleY;
        bone.SetLocalPosition(skeletonSpacePoint);
    }
}
