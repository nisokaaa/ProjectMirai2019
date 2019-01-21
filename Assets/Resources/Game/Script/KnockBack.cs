using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーにアタッチする
/// 当たり判定があった時に、現状の移動量を0にして
/// 逆方向に吹き飛ばす
/// </summary>
public class KnockBack : MonoBehaviour {

    GameObject KnockBackObject;

    [SerializeField]
    bool _KnockBack = false;

    [SerializeField]
    GameObject _ParticleSystem;
    ParticleSystem _Effect;

    [SerializeField]
    int _time = 0;

    Rigidbody _Rigidbody;

    Vector3 _move;

    Animator _animator;

    // Use this for initialization
    void Start () {
        _animator = GameObject.Find("PlayerUI2").GetComponent<Animator>();
        _Rigidbody = GetComponent<Rigidbody>();
        _ParticleSystem = Instantiate(_ParticleSystem, transform.position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
        if (_KnockBack == false)
        {
            
            return;
        }
        _ParticleSystem.transform.position = transform.position;
        _ParticleSystem.SetActive(true);
        _time++;

        if (_time > 30)
        {
            _time = 0;
            _ParticleSystem.SetActive(false);
            _KnockBack = false;
            _animator.SetTrigger("UI000");
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Vector3 KnockBackVec;
            KnockBackVec = (transform.position - collision.transform.position) * 10.5f;

            _move = new Vector3(KnockBackVec.x, 100.0f, KnockBackVec.z);
            _Rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

            Vector3 force = _move * _Rigidbody.mass;

            _Rigidbody.AddForce(force, ForceMode.Impulse);
            _KnockBack = true;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Vector3 KnockBackVec;
            KnockBackVec = (transform.position - other.transform.position) * 10.5f;

            _move = new Vector3(KnockBackVec.x, 100.0f, KnockBackVec.z);
            _Rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

            Vector3 force = _move * _Rigidbody.mass;

            _Rigidbody.AddForce(force, ForceMode.Impulse);
            _KnockBack = true;
        }
    }
    public bool GetDamageflag()
    {
        return _KnockBack;
    }
}
