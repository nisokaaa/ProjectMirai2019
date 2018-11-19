using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンの名前取得用

/// <summary>
/// �uSceneChange�֌W�̃f�[�^��ێ����Ă���Q�[���I�u�W�F�N�g�p�̃N���X�v
/// �����@ �F
/// �߂�l �F
/// �P�D
/// �Q�D
/// �쐬�ҁF�u���܂���
/// </summary>
public class SceneChangeMgr : SingletonMonoBehaviour<SceneChangeMgr>
{
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    //����p�X�N���v�g
    Title titleScript;
    Result resultScript;
    Game gameScript;

    // Use this for initialization
    void Start () {
		if(titleScript == null)
        {
            titleScript = GetComponent<Title>();
        }
        if(resultScript == null)
        {
            resultScript = GetComponent<Result>();
        }
        if(gameScript == null)
        {
            gameScript = GetComponent<Game>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        string text = SceneManager.GetActiveScene().name;

        if(text == "Title")
        {
            titleScript.enabled = true;
            gameScript.enabled = false;
            resultScript.enabled = false;
        }
        if(text == "Game")
        {
            titleScript.enabled = false;
            gameScript.enabled = true;
            resultScript.enabled = false;
        }
        if(text == "Result")
        {
            titleScript.enabled = false;
            gameScript.enabled = false;
            resultScript.enabled = true;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            #if UNITY_STANDALONE
                Application.Quit();
            #endif
        }
        /*�T���v���R�[�h
		if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneChangeController.Instance.SetChangeScene("Game");
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            SceneChangeController.Instance.SetChangeSceneExecution();
        }
        */
    }
    
}
