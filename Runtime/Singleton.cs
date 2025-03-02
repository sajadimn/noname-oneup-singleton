using System.Collections.Generic;
using UnityEngine;

namespace Noname.OneUp.Packages.Singleton
{
    /// <summary>
    /// Inherit from this base class to create a Singleton.
    /// e.g. public class MyClassName : Singleton<MyClassName> {}
    /// </summary>
    public class Singleton<T> where T : class, new()
    {
        private static bool isQuiting;
        private static T instance;
        private bool created;

        public static T Instance
        {
            get
            {
                if (Application.isEditor && !Application.isPlaying)
                    return null;

                if (isQuiting)
                {
                    Debug.LogWarning(string.Format("Singleton {0} Already Destroyed", typeof(T)));
                    return null;
                }

                if (instance == null)
                {
                    Debug.Log(string.Format("Singleton {0} Created Right Now", typeof(T)));
                    instance = new T();
                    (instance as Singleton<T>).created = true;
                    (instance as Singleton<T>).OnCreateSingleton();
                }

                return instance;
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