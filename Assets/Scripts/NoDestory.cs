using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestory : MonoBehaviour
{
    static NoDestory _instance;
    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != _instance)
        {
            Destroy(gameObject);
        }

    }
}
