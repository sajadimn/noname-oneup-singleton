using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> where T : class, new()
{
    private static T instance;
    private bool created;
    public static List<T> allSingletons { get; } = new List<T>();

    public static T Instance
    {
        get
        {
            if (Application.isEditor && !Application.isPlaying)
                return null;

            if (instance == null)
            {
                Debug.Log($"Singleton {typeof(T)} Created Right Now");
                instance = new T();
                allSingletons.Add(instance);
                ((instance as Singleton<T>)!).created = true;
                ((instance as Singleton<T>)!).OnCreateSingleton();
            }

            return instance;
        }
    }

    public void Destroy()
    {
        OnDestroy();
        created = false;
        instance = null;
    }

    private void OnApplicationQuit()
    {
        OnDestroy();
    }

    private void OnDestroy()
    {
        if (!created)
            return;

        OnDestroySingleton();
    }

    protected virtual void OnCreateSingleton()
    {
    }

    protected virtual void OnDestroySingleton()
    {
    }
}