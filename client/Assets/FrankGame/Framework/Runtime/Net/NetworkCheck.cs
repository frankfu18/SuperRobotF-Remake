using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public class NetworkCheck : SingleMonoBehaviour<NetworkCheck>
    {
        public bool IsReachability
        {
            get { return Application.internetReachability != NetworkReachability.NotReachable; }
        }

        public NetworkReachability CurNetworkState
        {
            get { return Application.internetReachability; }
        }

        public System.Action<bool> onNetworkStateChange;
        
        private NetworkReachability lastNetworkState;

        void Start()
        {
            lastNetworkState = Application.internetReachability;
        }

        void Update()
        {
            if (Application.internetReachability == lastNetworkState)
            {
                return;
            }

            lastNetworkState = Application.internetReachability;

            onNetworkStateChange?.Invoke(IsReachability);
        }
    }
}
