using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public class SingleMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this as T;
            }
        }
    }
}