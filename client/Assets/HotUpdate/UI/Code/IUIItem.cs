using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIItem 
{
    GameObject GameObject { get; }

    void Create(GameObject _go, int _index, object _container);

    void Show(object _param);

    void Hide();

    void Destroy();
}
