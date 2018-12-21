using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEfect : MonoBehaviour {

    [SerializeField]
    ParticleSystem _particleSystem;

    [SerializeField]
    bool _ParticleSystemOn = false;

    [SerializeField]
    bool _ParticleSystemOff = false;

    Rigidbody _rigidbody;

    float _speed;

    [SerializeField]
    float limitSpeedOn = 30;

    [SerializeField]
    float limitSpeedOff = 15;

    // Use this for initialization
    void Start () {
        _rigidbody = GameObject.Find("Player").GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        _speed =_rigidbody.velocity.magnitude;

        if(_speed > limitSpeedOn)
        {
            SetEffectOn();
        }
        else
        {
            SetEffectOff();
        }

        if (_ParticleSystemOn)
        {
            _ParticleSystemOn = false;
            _particleSystem.Play();
        }
        if (_ParticleSystemOff)
        {
            _ParticleSystemOff = false;
            _particleSystem.Stop();
        }
    }

    public void SetEffectOn()
    {
        _ParticleSystemOn = true;
    }

    public void SetEffectOff()
    {
        _ParticleSystemOff = true;
    }
}
