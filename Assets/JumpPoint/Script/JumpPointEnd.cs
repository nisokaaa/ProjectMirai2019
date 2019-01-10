using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointEnd : MonoBehaviour {
    bool _check = false; //切り替えフラグ
    bool _change = false;

    GameObject _player;

	// Use this for initialization
	void Start () {
        _player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _check = true;
            Debug.Log("aaaaaaaaaaa");
            other.GetComponent<PlayerController>().enabled = true;
            other.GetComponent<PlayerLeftRightElecDash>().enabled = true;
            
            //Destroy(other.gameObject.GetComponent<Rigidbody>());
            //other.gameObject.GetComponent<CharacterController>().enabled = true;
            //other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    public void SetEnd()
    {
        //if (_check == false) { return; }
        _player.GetComponent<PlayerController>().enabled = true;
        _player.GetComponent<PlayerLeftRightElecDash>().enabled = true;
    }
}
