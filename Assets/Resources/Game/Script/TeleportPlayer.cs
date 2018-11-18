using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour {

    [SerializeField] GameObject _player;

    [SerializeField] Transform _teleportPoint;
    [SerializeField] bool _bTeleport1 = false;

    [SerializeField] Transform _teleportPoint2;
    [SerializeField] bool _bTeleport2 = false;

    [SerializeField] Transform _teleportPoint3;
    [SerializeField] bool _bTeleport3 = false;

    [SerializeField] Transform _teleportPoint4;
    [SerializeField] bool _bTeleport4 = false;

    [SerializeField] Transform _teleportPoint5;
    [SerializeField] bool _bTeleport5 = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_bTeleport1 == true)
        {
            _player.transform.position = _teleportPoint.position;
            _bTeleport1 = false;
        }
        if (_bTeleport2 == true)
        {
            _player.transform.position = _teleportPoint2.position;
            _bTeleport2 = false;
        }
        if (_bTeleport3 == true)
        {
            _player.transform.position = _teleportPoint3.position;
            _bTeleport3 = false;
        }
        if (_bTeleport4 == true)
        {
            _player.transform.position = _teleportPoint4.position;
            _bTeleport4 = false;
        }
        if (_bTeleport5 == true)
        {
            _player.transform.position = _teleportPoint5.position;
            _bTeleport5 = false;
        }
    }
}
