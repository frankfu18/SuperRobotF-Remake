using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIBind 
{
    void Bind(GameObject _go, object _container);

    void Unbind();

    void Update(float _deltaTime);
}
