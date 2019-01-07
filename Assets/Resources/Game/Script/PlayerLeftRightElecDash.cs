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

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    // Use this for initialization
    void Start () {
        if (elecBarControl == null)
        {
            elecBarControl = GameObject.Find("ElecBarController").GetComponent<ElecBarControl>();
        }

        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        if (elecBarControl.GetGageValue() <= 0.0f)
            return;

        //ジョイコン
        if (!(m_joycons.Count <= 0 || m_joycons == null))
        {
            //ジャンプチェック
            if (m_joyconR.GetButton(Joycon.Button.SHOULDER_2))
            {
                elecBarControl.Decrease();
                elecBarControl.Decrease();
                elecBarControl.Decrease();
                elecBarControl.Decrease();
                rb.AddForce(transform.right * 1500.0f);
            }
            if (m_joyconL.GetButton(Joycon.Button.SHOULDER_2))
            {
                elecBarControl.Decrease();
                elecBarControl.Decrease();
                elecBarControl.Decrease();
                elecBarControl.Decrease();
                rb.AddForce(transform.right * -1500.0f);
            }
        }

        //キーボード
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
