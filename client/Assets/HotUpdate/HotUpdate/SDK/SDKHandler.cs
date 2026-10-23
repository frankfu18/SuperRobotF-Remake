using UnityEngine;

public class SDKHandler : Singlet<SDKHandler>
{
    public void GameStarted(int _level)
    {
        Debug.Log("[SDK]GameStarted level:" + _level);
    }

    public void GameFinished(bool _isUserCompleteLevel, int _score, int _level) 
    {
        Debug.Log("[SDK]GameFinished isUserCompleteLevel:" + _isUserCompleteLevel + ",score:" + _score + ",level:" + _level);
    }
}
