using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAttack : MonoBehaviour {

    [SerializeField]
    bool _MissileAttack = false;

    [SerializeField]
    GameObject _MissileObject;

    [SerializeField]
    Transform _Target;

    bool _IntervalTime = false;
    int IntervalTimeCnt = 0;

    [SerializeField] int IntervalTimeSetTime = 100;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(IntervalTimeCnt > IntervalTimeSetTime && _IntervalTime == true)
        {
            _IntervalTime = false;
        }

        if(_IntervalTime == true)
        {
            IntervalTimeCnt++;
            return;
        }

		if(_MissileAttack == true)
        {
            IntervalTimeCnt = 0;
            _IntervalTime = true;
            _MissileAttack = false;
            Instantiate(_MissileObject, _Target.position,Quaternion.identity);
        }
	}

    public void SetMissileAttack()
    {
        _MissileAttack = true;
    }
}
