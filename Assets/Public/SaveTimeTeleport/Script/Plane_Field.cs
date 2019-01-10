using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Field : MonoBehaviour {
    TeleportPlayer _teleportPlayer;
    // Use this for initialization
    void Start () {
        _teleportPlayer = GameObject.Find("SaveTimeTeleportSystem").GetComponent<TeleportPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _teleportPlayer.SetAct();
        }
    }
}
