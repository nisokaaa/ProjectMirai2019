using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTargetSphere : MonoBehaviour {

    [SerializeField]
    GameObject _Effect; //爆発

    [SerializeField]
    GameObject _EffectWave; //衝撃波

    [SerializeField]
    bool _bDes = false;

    [SerializeField]
    int _time = 0;

    [SerializeField]
    int _DelTime = 50;

    [SerializeField]
    GameObject _DelObject;

    // Use this for initialization
    void Start () {
        _Effect = Instantiate(_Effect,transform.position,Quaternion.identity) as GameObject;
        _Effect.SetActive(false);
        _EffectWave = Instantiate(_EffectWave, transform.position, Quaternion.identity) as GameObject;
        _EffectWave.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(_bDes == true)
        {
            _time++;

            if(_DelTime < _time)
            {
                Destroy(this.gameObject);
                Destroy(_Effect.gameObject);
                Destroy(_DelObject.gameObject);
                Destroy(_EffectWave.gameObject);
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Missile")
        {
            _bDes = true;
            _Effect.transform.position = transform.position;
            _Effect.SetActive(true);
            _EffectWave.transform.position = transform.position;
            _EffectWave.SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Missile")
        {
            _bDes = true;
            _Effect.transform.position = transform.position;
            _Effect.SetActive(true);
            _EffectWave.transform.position = transform.position;
            _EffectWave.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
