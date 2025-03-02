using System;
using System.Collections.Generic;
using UnityEngine;

namespace Noname.OneUp.Packages.Singleton
{
    /// <summary>
    /// Inherit from this base class to create a MonoSingleton.
    /// e.g. public class MyClassName : MonoSingleton<MyClassName> {}
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool isQuiting;
        private static object lockObject = new object();
        private static T instance;
        private bool created;

        protected virtual void Awake()
        {
            MonoSingleton<T>.Instance.name = MonoSingleton<T>.Instance.name + ":MonoSingleton";
        }

        public static T Instance
        {
            get
            {
                if (Application.isEditor && !Application.isPlaying)
                    return null;

                if (isQuiting)
                {
                    Debug.LogWarning(string.Format("MonoSingleton {0} Already Destroyed", typeof(T)));
                    return null;
                }

                lock (lockObject)
                {
                    bool firstInit = instance == null;
                    if (instance == null)
                    {
                        instance = (T) FindObjectOfType(typeof(T));

                        if (instance == null)
                        {
                            string name = typeof(T).Name;
                            GameObject prefab = Resources.Load<GameObject>(name);
                            if (prefab != null)
                            {
                                instance = Instantiate(prefab).GetComponent<T>();
                            }
                            else
                            {
                                instance = new GameObject(name).AddComponent<T>();
                            }

                            instance.gameObject.name = name;
                        }
                    }

                    if (firstInit)
                    {
                        Debug.Log(string.Format("MonoSingleton {0} Created Right Now", typeof(T)));
                        DontDestroyOnLoad(instance.gameObject);
                        (instance as MonoSingleton<T>).created = true;
                        (instance as MonoSingleton<T>).OnCreateSingleton();
                    }

                    return instance;
                }
            }
        }

        private void OnApplicationQuit()
        {
            if (!Application.isEditor)
                isQuiting = true;
        }

        private void OnDestroy()
        {
            if (!created)
                return;

            OnDestroySingleton();

            if (!Application.isEditor)
                isQuiting = true;
        }

        protected virtual void OnCreateSingleton()
        {
        }

        protected virtual void OnDestroySingleton()
        {
        }
    }
}