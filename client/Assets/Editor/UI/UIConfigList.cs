using UnityEngine;
using System.Collections.Generic;

public class UIConfigList
{
    public List<string> uiConfigNameList = new List<string>();
    public List<UIInfoEditor> uiInfoList = new List<UIInfoEditor>();
}

[System.Serializable]
public class UIInfoEditor
{
    public EUILayer layer;
    public int uiConfigIndex;
    public string prefabName;
    public bool instance = true;
    public bool showPlayAnimation = true;
    public bool loadResources = false;
}
