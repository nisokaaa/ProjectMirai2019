using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンの名前取得用

/// <summary>
/// 「タイトル処理」
/// 流れ
/// １．タイトル開始演出
/// ２．通常タイトル表示
/// ３．タイトル終了演出
/// 作成者：志村まさき
/// </summary>
public class Title : MonoBehaviour {
    
    enum WAVE
    {
        NONE = 0,
        START,
        PLAY,
        END,
        MAX
    };

    [SerializeField] WAVE title = WAVE.NONE;
    CanvasSelect canvasSelect;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    // Use this for initialization
    void Start () {
        string text = SceneManager.GetActiveScene().name;
        if (text != "Title")
        {
            return;
        }

        title = WAVE.START;

        if (canvasSelect == null)
            canvasSelect = GameObject.Find("CanvasSelect").GetComponent<CanvasSelect>();

        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }
	
	// Update is called once per frame
	void Update () {
        string text = SceneManager.GetActiveScene().name;
        if(text != "Title")
        {
            return;
        }

        switch (title)
        {
            case WAVE.START:
                title = WAVE.PLAY;
                break;
            case WAVE.PLAY:
                if(canvasSelect.GetSelect() == false)
                {
                    return;
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    SceneChangeController.Instance.SetChangeScene("Game");
                    title = WAVE.END;
                }

                if (!(m_joycons.Count <= 0 || m_joycons == null))
                {
                    if (m_joyconR.GetButtonDown(Joycon.Button.DPAD_RIGHT))
                    {
                        SceneChangeController.Instance.SetChangeScene("Game");
                        title = WAVE.END;
                    }
                }
                break;
            case WAVE.END:
                SceneChangeController.Instance.SetChangeSceneExecution();
                title = WAVE.NONE;
                break;
        }
    }
}