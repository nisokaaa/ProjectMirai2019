using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOP_POINT_CUBE_END : MonoBehaviour {

    TutorialLoop _tutorialLoop;
    Transform _Player;

    // Use this for initialization
    void Start () {
        _tutorialLoop = GameObject.Find("TUTORIAL_LOOP").GetComponent<TutorialLoop>();
        _Player = GameObject.Find("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		//強制判定
        if(_Player.transform.position.z > transform.position.z)
        {
            _tutorialLoop.SetPositionReset();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _tutorialLoop.SetPositionReset();
        }
    }
}
