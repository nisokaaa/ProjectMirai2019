using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour {

    public Animator _animator;
    bool _Activefalse = false;
    TeleportPlayer _teleportPlayer;
    SaveTimeManager _saveTimeManager;

    // Use this for initialization
    void Start () {
        _teleportPlayer = GameObject.Find("SaveTimeTeleportSystem").GetComponent<TeleportPlayer>();
        _saveTimeManager = GameObject.Find("TimeManager").GetComponent<SaveTimeManager>();
    }
	
	// Update is called once per frame
	void Update () {
		if(_Activefalse == true)
        {
            this.gameObject.SetActive(false);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _saveTimeManager.SetTimeSave();
            _teleportPlayer.SetTeleportPosition(this.gameObject.transform.position);
            _Activefalse = true;
            _animator.SetTrigger("SavePoint");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _animator.SetTrigger("SavePoint");
        }
    }
}
