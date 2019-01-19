using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElecMode : MonoBehaviour {

    [SerializeField] bool playerElecMode = false;
    [SerializeField] GameObject ElecModeEffect;
    ParticleSystem _particleSystem;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    // Use this for initialization
    void Start () {
        ElecModeEffect = Instantiate(ElecModeEffect, transform.position, Quaternion.identity) as GameObject;
        _particleSystem = ElecModeEffect.GetComponent<ParticleSystem>();
        _particleSystem.Stop();
        ElecModeEffect.SetActive(true);

        //joycon
        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤

        
    }
	
	// Update is called once per frame
	void Update () {
        if (!(m_joycons.Count <= 0 || m_joycons == null))
        {
            Debug.Log("デバイスチェックOK");
            if (Input.GetKeyDown(KeyCode.Joystick1Button15)|| m_joyconR.GetButtonDown(Joycon.Button.SHOULDER_1) || m_joyconL.GetButtonDown(Joycon.Button.SHOULDER_1))
            {
                playerElecMode = true;
                _particleSystem.Play();
            }
            if (Input.GetKeyUp(KeyCode.Joystick1Button15) || m_joyconR.GetButtonUp(Joycon.Button.SHOULDER_1) || m_joyconL.GetButtonUp(Joycon.Button.SHOULDER_1))
            {
                _particleSystem.Stop();
                playerElecMode = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            _particleSystem.Play();
            playerElecMode = true;
            
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            _particleSystem.Stop();
            playerElecMode = false;
        }
        
        if(playerElecMode == true)
        {
            ElecModeEffect.transform.position = transform.position;
        }
    }

    public bool GetMode()
    {
        return playerElecMode;
    }
}
