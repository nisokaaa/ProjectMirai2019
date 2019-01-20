using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「蓄電、充電スポットゲームオブジェクト用クラス」
/// 作成者：志村まさき
public class ChargingGameObject : MonoBehaviour {

    [SerializeField] GameObject particleSystem;
    bool bExecution = false;
    int timeCnt = 0;
    [SerializeField] int timeCntMax;
    PlayerElecEffect _playerElecEffect;

    // Use this for initialization
    void Start () {
        particleSystem = Instantiate(particleSystem, transform.position, Quaternion.identity) as GameObject;
        particleSystem.SetActive(false);
        bExecution = false;
        _playerElecEffect = GameObject.Find("PlayerElecEffect").GetComponent<PlayerElecEffect>();
    }
	
	// Update is called once per frame
	void Update () {
		if(bExecution == true)
        {
            particleSystem.transform.position = transform.position;
            particleSystem.SetActive(true);
            timeCnt++;

            if(timeCnt > timeCntMax)
            {
                bExecution = false;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_GAME_GAUGE);
            timeCnt = 0;
            particleSystem.SetActive(false);
            bExecution = true;
            _playerElecEffect.SetElecPosition(this.gameObject);
        }
    }
}
