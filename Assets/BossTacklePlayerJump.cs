using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 プレイヤーがタックルを受けそうになったらスローになって
 UIを表示
     */
public class BossTacklePlayerJump : MonoBehaviour {

    [SerializeField]
    private TimeManager timeManager;

    [SerializeField]
    Animator _UI;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _UI.SetTrigger("BossJump");
            timeManager.SlowDown();
        }
    }
}
