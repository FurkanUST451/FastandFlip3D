using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    private static volatile T instance;

    public static T Instance 
    {
        get
        {
            if (instance==null)
            {
                instance = FindObjectOfType((typeof(T))) as T;
            }
            return instance;
        }
    }
}
