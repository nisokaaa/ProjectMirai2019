using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointStart : MonoBehaviour {
    public JumpPoint jumpPoint;

    // Use this for initialization
    void Start () {

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
            AudioManager.Instance.PlaySE(AUDIO.SE_GAME_JUMPPOINT);
            
            

            var rigidbody = other.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;

            //GetComponent<CharacterController>().enabled = false;
            other.GetComponent<PlayerController>().enabled = false;
            other.GetComponent<PlayerLeftRightElecDash>().enabled = false;
            //other.gameObject.GetComponent<CapsuleCollider>().enabled = true;

            jumpPoint.SetJump();
        }
    }
}
