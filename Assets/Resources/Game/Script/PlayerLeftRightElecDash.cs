using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「左右電気ダッシュ」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：志村まさき
/// </summary>
public class PlayerLeftRightElecDash : MonoBehaviour {

    bool _rightJump = false;
    bool _leftJump = false;
    ElecBarControl elecBarControl;

    // Use this for initialization
    void Start () {
        if (elecBarControl == null)
        {
            elecBarControl = GameObject.Find("ElecBarController").GetComponent<ElecBarControl>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        if (elecBarControl.GetGageValue() <= 0.0f)
            return;

        if (Input.GetKey(KeyCode.Q) || _leftJump == true)
        {
            elecBarControl.Decrease();
            elecBarControl.Decrease();
            elecBarControl.Decrease();
            elecBarControl.Decrease();
            rb.AddForce(transform.right * -1500.0f);
        }
        if (Input.GetKey(KeyCode.E) || _rightJump == true)
        {
            elecBarControl.Decrease();
            elecBarControl.Decrease();
            elecBarControl.Decrease();
            elecBarControl.Decrease();
            rb.AddForce(transform.right * 1500.0f);
        }
    }
}
