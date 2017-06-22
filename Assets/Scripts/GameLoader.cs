using UnityEngine;
using System.Collections;
using System;

public class GameLoader : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //SceneController
        if (SceneController.Instance == null)
        {
            InstantiateController<SceneController>();
        }

        //IAPController
        if (IAPController.Instance == null)
        {
            InstantiateController<IAPController>();
        }
    }

    private void InstantiateController<T> () where T : MonoBehaviour
    {
        Type compType = typeof(T);

        var obj = new GameObject(compType.ToString());
        obj.transform.parent = gameObject.transform;
        obj.AddComponent<T>();
    }
}