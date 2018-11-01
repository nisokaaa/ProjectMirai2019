using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratedDischargeAction : MonoBehaviour {
    PlayerModelAnimatorController playerModelAnimatorController;
    public GameObject particleSystem;

    // Use this for initialization
    void Start () {
        if(playerModelAnimatorController == null)
        {
            playerModelAnimatorController = GameObject.Find("PlayerModelAnimatorController").GetComponent<PlayerModelAnimatorController>();
        }
        particleSystem = Instantiate(particleSystem, transform.position, Quaternion.identity)as GameObject;
        particleSystem.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.N))
        {
            particleSystem.transform.position = transform.position;
            particleSystem.SetActive(true);
            playerModelAnimatorController.PlayerAtackControl(true);
        }
        else
        {
            particleSystem.SetActive(false);
            playerModelAnimatorController.PlayerAtackControl(false);
        }
	}

    //加速アクション
}
