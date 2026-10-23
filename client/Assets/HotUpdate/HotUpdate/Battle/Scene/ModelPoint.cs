using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPoint : MonoBehaviour
{
    public SkeletonAnimation skeletonGraphic;

    public void PlayAction(string actionName, bool loop = false, Action<TrackEntry> callback = null)
    {
        if(skeletonGraphic == null)
        {
            return;
        }

        skeletonGraphic.AnimationState.GetCurrent(0)?.ClearComplete();

        var trackEntry = skeletonGraphic.AnimationState.SetAnimation(0, actionName, loop);
        if (callback != null) trackEntry.Complete += entry => callback(entry);
    }

    public void SetTimeScale(float timeScale)
    {
        if (skeletonGraphic == null)
        {
            return;
        }

        skeletonGraphic.AnimationState.TimeScale = timeScale;
    }
}
