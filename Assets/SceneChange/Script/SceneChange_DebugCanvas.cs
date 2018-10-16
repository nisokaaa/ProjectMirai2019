using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               //uguiのテキスト取得用
using UnityEngine.SceneManagement;  //シーンの名前取得用

/// <summary>
/// 「現在のシーン情報を表示するCanvas用のクラス」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：志村
/// </summary>
public class SceneChange_DebugCanvas : MonoBehaviour {

    GameObject gameObjectText;

	// Use this for initialization
	void Start () {
        //子要素を取得する
        gameObjectText = gameObject.transform.Find("Text").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        CanvasDrawSceneName();
    }

    //現在のシーンを子要素のテキストに表示する
    void CanvasDrawSceneName()
    {
        string text = "現在のシーン：" + SceneManager.GetActiveScene().name;
        gameObjectText.GetComponent<Text>().text = text;
    }
}
