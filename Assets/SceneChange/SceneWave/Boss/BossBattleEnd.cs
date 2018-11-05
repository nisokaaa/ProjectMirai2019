using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleEnd : MonoBehaviour {

    bool nextScene;

    // Use this for initialization
    void Start () {
        nextScene = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            nextScene = true;
        }
    }

    public bool GetBossBattleFlag()
    {
        return nextScene;
    }
}
