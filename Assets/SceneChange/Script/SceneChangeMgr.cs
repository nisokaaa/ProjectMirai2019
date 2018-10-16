using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeMgr : SingletonMonoBehaviour<SceneChangeMgr>
{
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
