using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「現在のシーン情報を表示するCanvas用のクラス」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：志村まさき
/// </summary>
public class ElecBarCharge : MonoBehaviour {

    ElecBarControl elecBarControl;
    [SerializeField] bool bObjectTriggerEnter = false;
    [SerializeField] int chargeTimeMax;
    [SerializeField] int chargeCnt;

    // Use this for initialization
    void Start () {
        chargeCnt = 0;

        if (elecBarControl == null)
        {
            elecBarControl = GameObject.Find("ElecBarController").GetComponent<ElecBarControl>();
        }

	}
	
	// Update is called once per frame
	void Update () {
        ChargeElecBar();
    }

    void ChargeElecBar()
    {
        if(bObjectTriggerEnter == false)
        {
            return;
        }

        if(chargeTimeMax > chargeCnt)
        {
            chargeCnt++;
            elecBarControl.Increase();
            elecBarControl.Increase();
            elecBarControl.Increase();
            elecBarControl.Increase();
        }
        else
        {
            chargeCnt = 0;
            bObjectTriggerEnter = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ChargeObject")
        {
            bObjectTriggerEnter = true;
        }
    }
}
