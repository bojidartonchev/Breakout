using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonScript : MonoBehaviour {
    public int m_level;
	// Use this for initialization
	void Start () {
        Debug.Assert(m_level > 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void LoadAttachedLevel()
    {
        //SceneController.Instance.LoadLevel(m_level);
        SNSController.Instance.Login();
    }
}
