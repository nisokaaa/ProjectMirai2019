using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：志村まさき
/// </summary>
public class TutorialEnd_NextGame : MonoBehaviour
{

    [SerializeField] GameObject TutorialObject;
    [SerializeField] GameObject GameStartUI;
    Animator GameStartUIAnim;

    enum FADE
    {
        NONE = 0,
        START,
        PLAY,
        END,
        MAX
    };
    [SerializeField] FADE fade = FADE.NONE;
    [SerializeField] int endTutorialTime;
    int time= 0;

    // Use this for initialization
    void Start()
    {
        fade = FADE.NONE;
        GameStartUI.SetActive(false);
        time = 0;
        //GameStartUIAnim = GameStartUI.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (fade)
        {
            case FADE.START:
                fade = FADE.PLAY;
                GameStartUI.SetActive(true);
                break;
            case FADE.PLAY:
                time++;
                if (endTutorialTime < time)
                {
                    fade = FADE.END;
                    time = 0;
                }
                break;
            case FADE.END:
                GameStartUI.SetActive(false);
                TutorialObject.SetActive(false);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().SetGameStart();
            GameStartUI.SetActive(true);
            fade = FADE.START;
        }
    }
}
