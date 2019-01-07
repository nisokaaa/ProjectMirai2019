using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElecMode : MonoBehaviour {

    [SerializeField] bool playerElecMode = false;
    [SerializeField] GameObject ElecModeEffect;
    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    // Use this for initialization
    void Start () {
        //joycon
        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤

        ElecModeEffect = Instantiate(ElecModeEffect, transform.position, Quaternion.identity) as GameObject;
        ElecModeEffect.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (!(m_joycons.Count <= 0 || m_joycons == null))
        {
            Debug.Log("デバイスチェックOK");
            if (Input.GetKey(KeyCode.Joystick1Button15)|| m_joyconR.GetButton(Joycon.Button.SHOULDER_1) || m_joyconL.GetButton(Joycon.Button.SHOULDER_1))
            {
                playerElecMode = true;
                ElecModeEffect.SetActive(true);
                ElecModeEffect.transform.position = transform.position;
                Debug.Log("OK");
            }
            else
            {
                playerElecMode = false;
                ElecModeEffect.SetActive(false);
            }
        }
    }

    public bool GetMode()
    {
        return playerElecMode;
    }
}
