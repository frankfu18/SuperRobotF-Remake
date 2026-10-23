using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ServerListInfo
{
    public int defaultGroupId;
    public int defaultServerId;
    public Dictionary<string, AreaInfo> areaInfos;
    public List<ServerOuterLinkInfo> outerLinkInfos;
}

[System.Serializable]
public class AreaInfo
{
    public List<ServerInfo> serverInfos;
    public int defaultServerId;
    public int groupId;
}

[System.Serializable]
public class ServerInfo
{
    public int serverId;
    public int state;
    public int newState;
    public List<SeverLinkInfo> linkInfos;
}

[System.Serializable]
public class SeverLinkInfo
{
    public string ip;
    public int port;
}

[System.Serializable]
public class ServerOuterLinkInfo
{
    public string ipv6;
    public string ip;
    public int port;
}
