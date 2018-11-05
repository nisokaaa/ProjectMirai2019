using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：志村まさき
/// </summary>
public class BossBattleStart : MonoBehaviour {

    [SerializeField] Animator BossState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BossState.SetBool("BossBattleStart", true);
        }
    }
}
