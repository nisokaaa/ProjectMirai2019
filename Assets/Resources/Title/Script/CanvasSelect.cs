using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSelect : MonoBehaviour {

    Animator animator;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    bool GameSelect = true;

    // Use this for initialization
    void Start () {
        GameSelect = false;

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
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameSelect = false;
        }
        animator.SetBool("Select", GameSelect);
        
    }

    public bool GetSelect()
    {
        return GameSelect;
    }
}
