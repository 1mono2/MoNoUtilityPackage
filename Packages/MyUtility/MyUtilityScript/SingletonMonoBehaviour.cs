using UnityEngine;
using System;

namespace MyUtility
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected abstract bool DontDestroy { get; }
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    instance = (T)FindObjectOfType(t);
                    if (instance == null)
                    {
                        Debug.LogWarning(t + ": attached GameObject don't exist in this Scene");
                    }
                }

                return instance;
            }
        }

        virtual protected void Awake()
        {
            CheckInstance();
        }

        protected bool CheckInstance()
        {
            if (instance == null)
            {
                instance = this as T;
                if (DontDestroy)
                {
                    DontDestroyOnLoad(gameObject);
                }
                return true;
            }
            else if (Instance == this)
            {
                if (DontDestroy)
                {
                    DontDestroyOnLoad(gameObject);
                }
                return true;
            }
            Destroy(this);
            return false;
        }

    }
}