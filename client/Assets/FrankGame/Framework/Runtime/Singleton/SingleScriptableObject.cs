using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame
{
    public class SingleScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        public static string ScriptableObjectPath;

        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    if (string.IsNullOrEmpty(ScriptableObjectPath))
                    {
                        ScriptableObjectPath = typeof(T).FullName.Replace(".", "/");
                    }

                    instance = Resources.Load<T>(ScriptableObjectPath);
                }

                if (instance == null)
                {
                    instance = CreateInstance<T>();
                }

                return instance;
            }
        }
    }
}