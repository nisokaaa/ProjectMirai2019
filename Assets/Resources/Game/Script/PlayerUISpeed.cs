using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUISpeed : MonoBehaviour {

    Rigidbody _speed;
    Text _text;
    int _vel;

	// Use this for initialization
	void Start () {
        _text = GetComponent<Text>();
        _speed = GameObject.Find("Player").GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        _text.text = _speed.velocity.magnitude.ToString("f1");
	}
}
