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
    [SerializeField] int time;
    bool bossBattle = false;

	// Use this for initialization
	void Start () {
        bossBattle = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(bossBattle == true)
        {
            time--;

            if(time <= 0)
            {
                time = 0;
                bossBattle = false;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BossState.SetBool("BossBattleStart", true);
            bossBattle = true;
        }
    }

    public bool GetBossBattleTime()
    {
        return bossBattle;
    }

    public int GetBossBattleStartTime()
    {
        return time;
    }
}
