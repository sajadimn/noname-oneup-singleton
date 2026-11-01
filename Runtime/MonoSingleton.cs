using System;
using System.Collections.Generic;
using UnityEngine;

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
    public static List<T> allMonoSingletons { get; } = new List<T>();

    protected virtual void Awake()
    {
        gameObject.name = MonoSingleton<T>.Instance.name;
    }

    public static T Instance
    {
        get
        {
            if (Application.isEditor && !Application.isPlaying)
                return null;

            if (isQuiting)
            {
                Debug.LogWarning($"MonoSingleton {typeof(T)} Already Destroyed");
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

                        instance.gameObject.name = MonoSingleton<T>.Instance.name;
                    }
                }

                if (firstInit)
                {
                    Debug.Log($"MonoSingleton {typeof(T)} Created Right Now");
                    DontDestroyOnLoad(instance.gameObject);
                    allMonoSingletons.Add(instance);
                    ((instance as MonoSingleton<T>)!).created = true;
                    ((instance as MonoSingleton<T>)!).OnCreateSingleton();
                }

                return instance;
            }
        }
    }

    public void Destroy()
    {
        instance = null;
        Destroy(this.gameObject);
    }

    private void OnApplicationQuit()
    {
        OnDestroy();
        if (!Application.isEditor)
            isQuiting = true;
    }

    private void OnDestroy()
    {
        if (!created)
            return;
        created = false;
        OnDestroySingleton();
    }

    protected virtual void OnCreateSingleton()
    {
    }

    protected virtual void OnDestroySingleton()
    {
    }
}