using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBeamSystem : MonoBehaviour {

    [SerializeField]
    ParticleAttraction _particleAttraction;

    [SerializeField]
    CharacterVecRotation characterVecRotation;

    [SerializeField]
    GameObject _Target;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetBeamAction()
    {
        characterVecRotation.SetTargetChange(_Target);
        _particleAttraction.SetStart();
    }

    public void SetBeamActionStop()
    {
        characterVecRotation.SetTargetChangeReset();
        _particleAttraction.SetStop();
    }
}
