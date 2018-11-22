﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointStart : MonoBehaviour {
    private GameObject _parent;

    // Use this for initialization
    void Start () {
        _parent = transform.root.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// プレイヤーのキャラクターコントローラを切る
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            JumpPoint jumpPoint = _parent.GetComponent<JumpPoint>();
            jumpPoint.SetJump();

            //other.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }
    }
}
