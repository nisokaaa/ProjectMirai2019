using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSelect : MonoBehaviour {

    Animator animator;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    [SerializeField]
    bool GameSelect = true;

    [SerializeField]
    GameObject _GameStartON;
    [SerializeField]
    GameObject _GameStartOFF;
    [SerializeField]
    GameObject _CreditON;
    [SerializeField]
    GameObject _CreditOFF;

    bool _check = false;

    // Use this for initialization
    void Start () {
        GameSelect = false;

        _CreditOFF.SetActive(true);
        _CreditON.SetActive(false);
        _GameStartOFF.SetActive(false);
        _GameStartON.SetActive(true);

        if (animator == null)
        animator = GetComponent<Animator>();

        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }
	
	// Update is called once per frame
	void Update () {
        bool _se = false;

        if (!(m_joycons.Count <= 0 || m_joycons == null))
        {
            if(m_joyconR.GetStick()[0] > 0.5f)
            {
                GameSelect = false;
            }
            if(m_joyconR.GetStick()[0] < -0.5f)
            {
                GameSelect = true;
            }
            if (m_joyconL.GetStick()[0] > 0.5f)
            {
                GameSelect = false;
            }
            if (m_joyconL.GetStick()[0] < -0.5f)
            {
                GameSelect = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameSelect = true;
            //animator.SetTrigger("GameStart");
            _CreditOFF.SetActive(false);
            _CreditON.SetActive(true);
            _GameStartOFF.SetActive(true);
            _GameStartON.SetActive(false);
            _se = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _se = true;
            //animator.SetTrigger("Credit");
            GameSelect = false;
            _CreditOFF.SetActive(true);
            _CreditON.SetActive(false);
            _GameStartOFF.SetActive(false);
            _GameStartON.SetActive(true);
        }
        //animator.SetBool("Select", GameSelect);
        if(!(Input.anyKey))
        {
            _check = false;
        }

        if (_se == true && _check == false)
        {
            _se = false;
            _check = true;
            AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
        }
    }

    public bool GetSelect()
    {
        return GameSelect;
    }
}
