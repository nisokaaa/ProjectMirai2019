using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileObj : MonoBehaviour {

    [SerializeField]
    GameObject _Effect01;

    public bool _bActiveEffect = false;

    [SerializeField]
    int Resetcnt = 0;

    KnockBack _knockBack;

	// Use this for initialization
	void Start () {
        _knockBack = GameObject.Find("Player").GetComponent<KnockBack>();
        _Effect01 = Instantiate(_Effect01,transform.position,Quaternion.identity);
        _Effect01.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(_bActiveEffect == true)
        {
            _bActiveEffect = false;
            _Effect01.transform.position = transform.position;
            _Effect01.SetActive(true);
            Resetcnt++;
        }
        if(Resetcnt > 0)
        {
            Resetcnt++;
            if(Resetcnt > 60)
            {
                _Effect01.SetActive(false);
                Resetcnt = 0;
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Plane"|| collision.gameObject.tag == "Player")
        {
            _bActiveEffect = true;
        }
    }
}
