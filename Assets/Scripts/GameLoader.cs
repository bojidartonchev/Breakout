using UnityEngine;
using System.Collections;
using System;

public class GameLoader : MonoBehaviour
{
    private GameObject sceneController;        //GameManager prefab to instantiate.
    //public GameObject soundManager;          //SoundManager prefab to instantiate.

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //Check if a SceneController has already been assigned to static variable GameManager.instance or if it's still null
        if (SceneController.Instance == null)
        {
            InstantiateController<SceneController>(sceneController);
        }

        ////Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
        //if (SoundManager.instance == null)
        //
        //    //Instantiate SoundManager prefab
        //    Instantiate(soundManager);
    }

    private void InstantiateController<T> (GameObject obj) where T : MonoBehaviour
    {
        Type compType = typeof(T);

        obj = new GameObject(compType.ToString());
        obj.transform.parent = gameObject.transform;
        obj.AddComponent<T>();
    }
}