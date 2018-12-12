using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointEnd : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("aaaaaaaaaaa");
            other.GetComponent<PlayerController>().enabled = true;
            other.GetComponent<PlayerLeftRightElecDash>().enabled = true;
            
            //Destroy(other.gameObject.GetComponent<Rigidbody>());
            //other.gameObject.GetComponent<CharacterController>().enabled = true;
            //other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
