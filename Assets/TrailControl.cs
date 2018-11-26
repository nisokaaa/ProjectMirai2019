using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailControl : MonoBehaviour {

    TrailRenderer _trailRenderer;
    AcceleratedDischargeAction _acceleratedDischargeAction;

    // Use this for initialization
    void Start () {
        _acceleratedDischargeAction = GameObject.Find("Player").GetComponent<AcceleratedDischargeAction>();
        _trailRenderer = GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(_acceleratedDischargeAction.GetAcceleration() == false)
        {
            _trailRenderer.time = 0.03f;
        }
        else
        {
            _trailRenderer.time = 1.0f;
        }
	}
}
