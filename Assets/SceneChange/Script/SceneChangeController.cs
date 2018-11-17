using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //非同期処理用

/// <summary>
/// 「シーンの読み込みを非同期で実行するクラス」
/// 引数　 ：set 切り替えたいシーンの名前を入力する
/// 戻り値 ：get 次に読み込むシーンの名前を取得する
/// １．ChangeScene("切り替えたいシーンの名前")を呼ぶ
/// ２．シーンを切り替えたいタイミングでChangeSceneExecution()関数を呼ぶ
/// 作成者：志村まさき
/// </summary>
public class SceneChangeController : SingletonMonoBehaviour<SceneChangeController>
{

    AsyncOperation nextScene;   //非同期読み込み状況の表示
    enum LOAD_STATE
    {
        NONE = 0,       //読み込み前
        NOW_LOADING,    //読み込み中
        LOAD_COMPLETE,  //ロード完了
        MAX_STATE
    };

    [Header("現在のシーンの状態：確認用に表示してるだけ")]
    [SerializeField]
    LOAD_STATE loadState = LOAD_STATE.NONE; //現在の読み込み状態

    string _nextSceneName;                   //読み込むシーンの名前
    

    [Header("デバック用")]
    [SerializeField, TooltipAttribute("①：切り替えたいSceneの名前を入力")]
    string debugSceneName;
    [SerializeField, TooltipAttribute("②：Debug用に切り替えフラグを立てる")]
    bool bDebug = false;
    [SerializeField, TooltipAttribute("③：Sceneを切り替える")]
    public bool bChangeSceneFlag = false;
    [Header("ConsoleWindowにDebug情報を表示させる")]
    [SerializeField, TooltipAttribute("非同期読み込みの％情報、")]
    bool bDebugLogConsoleDraw = false;

    [SerializeField]
    Fade _fade;

    [SerializeField, Range(0, 5)]
    private float _fadeTime = 3.0f;

    [SerializeField]
    GameObject FadeCanvas;

    [SerializeField]
    int _SceneChangeTimeStop = 10;
    [SerializeField]
    int _SceneChangeTimeCnt = 0;

    [SerializeField]
    GameObject _loadObject;

    public string nextSceneName{
        get { return _nextSceneName; }
        set { _nextSceneName = nextSceneName; }
    }

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        
    }

    // Use this for initialization
    void Start () {
        initParameter();

        //if(_fade == null)
        //_fade = FadeCanvas.GetComponent<Fade>();

        _SceneChangeTimeCnt = 0;
        _loadObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(bChangeSceneFlag == true && loadState == LOAD_STATE.LOAD_COMPLETE)
        {
            _SceneChangeTimeCnt++;
            _loadObject.SetActive(true);
            if (_SceneChangeTimeStop > _SceneChangeTimeCnt)
            {
                return;
            }
            _loadObject.SetActive(false);
            _SceneChangeTimeCnt = 0;

            nextScene.allowSceneActivation = true;
            initParameter();
        }

        if(bDebug == true)
        {
            bDebug = false;
            SetChangeScene(debugSceneName);
        }
	}

    void initParameter()
    {
        loadState = LOAD_STATE.NONE;
        _nextSceneName = "";
        debugSceneName = "";
        bChangeSceneFlag = false;
        bDebug = false;
    }

    //シーンを切り替える
    public void SetChangeScene( string sceneName )
    {
        //読み込み重複チェック
        if (loadState != LOAD_STATE.NONE)
        {
            return;
        }

        _nextSceneName = "";
        _nextSceneName = sceneName;
        StartCoroutine("LoadNextScene");
        
    }

    //コルーチン処理
    IEnumerator LoadNextScene()
    {
        loadState = LOAD_STATE.NOW_LOADING;

        //nextScene = Application.LoadLevelAsync(_nextSceneName);
        nextScene = SceneManager.LoadSceneAsync(_nextSceneName);

        nextScene.allowSceneActivation = false;

        while (nextScene.progress < 0.9f)
        {
            if (bDebugLogConsoleDraw == true)
            {
                Debug.Log(nextScene.progress * 100 + "%");
            }
            yield return new WaitForSeconds(0);
        }
        if (bDebugLogConsoleDraw == true)
        {
            Debug.Log("読み込み完了　100%");
        }
        loadState = LOAD_STATE.LOAD_COMPLETE;
    }

    //シーンを切り替えるフラグを立てる
    public void SetChangeSceneExecution()
    {
        
        bChangeSceneFlag = true;
    }

    public void FadeIn()
    {
        _fade.FadeIn(_fadeTime);
    }
    public void FadeOut()
    {
        _fade.FadeOut(_fadeTime);
    }
}
