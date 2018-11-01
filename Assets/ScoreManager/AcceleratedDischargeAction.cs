using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「放電アクション 加速処理」
/// 作成者：志村まさき
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
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForce( 2.0f * rb.velocity);
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
