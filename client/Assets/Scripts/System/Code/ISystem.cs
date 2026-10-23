using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISystem
{
    void Init(MonoBehaviour _mono);

    void InitDone();

    void Update(float _deltaTime, float _unscaledDeltaTime);

    void Dispose();
}
