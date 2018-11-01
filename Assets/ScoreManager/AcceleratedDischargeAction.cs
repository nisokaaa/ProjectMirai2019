using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「放電アクション 加速処理」
/// 作成者：志村まさき
public class AcceleratedDischargeAction : MonoBehaviour {
    PlayerModelAnimatorController playerModelAnimatorController;
    ElecBarControl elecBarControl;
    public GameObject particleSystem;
    

    // Use this for initialization
    void Start () {
        if(playerModelAnimatorController == null)
        {
            playerModelAnimatorController = GameObject.Find("PlayerModelAnimatorController").GetComponent<PlayerModelAnimatorController>();
        }
        if (elecBarControl == null)
        {
            elecBarControl = GameObject.Find("ElecBarController").GetComponent<ElecBarControl>();
        }

        particleSystem = Instantiate(particleSystem, transform.position, Quaternion.identity)as GameObject;
        particleSystem.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(elecBarControl.GetGageValue() <= 0.0f)
        {
            particleSystem.SetActive(false);
            return;
        }

		if(Input.GetKey(KeyCode.N))
        {
            elecBarControl.Decrease();
            elecBarControl.Decrease();
            elecBarControl.Decrease();
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
