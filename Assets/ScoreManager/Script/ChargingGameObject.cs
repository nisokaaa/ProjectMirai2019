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
    // Use this for initialization
    void Start () {
        particleSystem = Instantiate(particleSystem, transform.position, Quaternion.identity) as GameObject;
        particleSystem.SetActive(false);
        bExecution = false;
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
            timeCnt = 0;
            particleSystem.SetActive(false);
            bExecution = true;
        }
    }
}
