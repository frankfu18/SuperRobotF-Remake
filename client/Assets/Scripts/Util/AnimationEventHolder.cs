using System;
using UnityEngine;

public class AnimationEventHolder : MonoBehaviour
{

    public readonly string EventName = nameof(OnFinished);


    public Action Done;

    public void OnFinished() {
        Done?.Invoke();
    }


}
