using UnityEngine;

public interface IUIInterface 
{
    UIConfig UIConfigId { get; }

    int RefCount { get;}

    GameObject GameObject { get; }

    int SortLayer { get; }

    UIInfo UIInfo { get; }

    void Init(UIInfo _uiInfo);

    void Create(GameObject _go, int _layer, object _container);

    void SetLayer(int _layer);

    void Open(object _param, System.Action<UIPanel> _onOpenDone);

    void Shut();//关闭

    void Show(bool _first);

    void Hide();

    void Destroy();

    void AddRef();

    void ReduceRef();
}
