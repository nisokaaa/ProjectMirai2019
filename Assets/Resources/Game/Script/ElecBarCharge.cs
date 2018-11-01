using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }else
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
