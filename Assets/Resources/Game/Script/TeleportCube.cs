using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCube : MonoBehaviour {

    [SerializeField] Transform _teleportPoint;
    [SerializeField] Transform _player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _player.transform.position = _teleportPoint.transform.position;
        }
    }
}
