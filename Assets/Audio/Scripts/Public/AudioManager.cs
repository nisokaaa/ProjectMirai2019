/*
    AudioManager

    内容
    BGMやSEの再生を行う為のクラス

    使用例※使えるメソッド
    シーンマネージャークラス内

    void Start()
    {
        //音の再生
        AudioManager.Instance.PlayBGM("BGM_SHOP_000");

        ////BGMフェードアウト
        AudioManager.Instance.FadeOutBGM();

        //SE再生。AUDIO.SE_BUTTONがSEのファイル名
        AudioManager.Instance.PlaySE(AUDIO.SE_BUTTON);

        //SEのボリューム変更
        ChangeVolume(0.5f,1.0f);
    }

    ※PlayBGMは、現在曲が流れていれば、自動的に今の曲をフェードアウトして
    次のBGMを再生してくれる。
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// BGMとSEの管理をするマネージャ。シングルトン。
/// </summary>
/// 

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    //ボリューム保存用のkeyとデフォルト値
    private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
    private const string SE_VOLUME_KEY = "SE_VOLUME_KEY";
    private const float BGM_VOLUME_DEFULT = 1.0f;
    private const float SE_VOLUME_DEFULT = 1.0f;

    //BGMがフェードするのにかかる時間
    public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
    public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
    private float _bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

    //次流すBGM名、SE名
    private string _nextBGMName;
    private string _nextSEName;

    //BGMをフェードアウト中か
    private bool _isFadeOut = false;

    //BGM用、SE用に分けてオーディオソースを持つ
    public AudioSource AttachBGMSource, AttachSESource;

    //全Audioを保持
    private Dictionary<string, AudioClip> _bgmDic, _seDic;


	/// 志村追記
    //シーンの保持　シーンチェンジ感知用
    bool m_bgmchange;
    string m_sceneName;
    string m_oldsceneName;
    bool m_GameBGM;



    //=================================================================================
    //初期化
    //=================================================================================

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        //リソースフォルダから全SE&BGMのファイルを読み込みセット
        _bgmDic = new Dictionary<string, AudioClip>();
        _seDic = new Dictionary<string, AudioClip>();

        object[] bgmList = Resources.LoadAll("Audio/BGM");
        object[] seList = Resources.LoadAll("Audio/SE");

        foreach (AudioClip bgm in bgmList)
        {
            _bgmDic[bgm.name] = bgm;
        }
        foreach (AudioClip se in seList)
        {
            _seDic[se.name] = se;
        }


		///志村追記
        //現在のシーンの記録
        m_sceneName = SceneManager.GetActiveScene().name;
        m_oldsceneName = m_sceneName;
    }

    private void Start()
    {
        m_GameBGM = false;



        AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
        AttachSESource.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);


		///志村追記
        //シーン切替用　デリゲートの登録
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        m_bgmchange = true;

    }


    //=================================================================================
    //シーン切替用のデリゲート 志村追記
    //=================================================================================
    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        Debug.Log(prevScene.name + "->" + nextScene.name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name + " scene loaded");
    }

    void OnSceneUnloaded(Scene scene)
    {
        //Debug.Log(scene.name + " scene unloaded");
    }

    //=================================================================================
    //SE
    //=================================================================================

    /// <summary>
    /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
    /// </summary>
    public void PlaySE(string seName, float delay = 0.0f)
    {
        if (!_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;
        }

        _nextSEName = seName;
        Invoke("DelayPlaySE", delay);
    }

    //SEを遅らせる関数
    private void DelayPlaySE()
    {
        AttachSESource.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }

    //=================================================================================
    //BGM
    //=================================================================================

    /// <summary>
    /// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
    /// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
    /// </summary>
    public void PlayBGM(string bgmName, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH)
    {
        if (!_bgmDic.ContainsKey(bgmName))
        {
            Debug.Log(bgmName + "という名前のBGMがありません");
            return;
        }

        //現在BGMが流れていない時はそのまま流す
        if (!AttachBGMSource.isPlaying)
        {
            _nextBGMName = "";
            AttachBGMSource.clip = _bgmDic[bgmName] as AudioClip;
            AttachBGMSource.Play();
        }
        //違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
        else if (AttachBGMSource.clip.name != bgmName)
        {
            _nextBGMName = bgmName;
            FadeOutBGM(fadeSpeedRate);
        }

    }

    /// <summary>
    /// 現在流れている曲をフェードアウトさせる
    /// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
    /// </summary>
    public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
    {
        _bgmFadeSpeedRate = fadeSpeedRate;
        _isFadeOut = true;
    }
    
    private void Update()
    {
        //音楽再生
        BGM_CHANGE();

        if (!_isFadeOut)
        {
            return;
        }

        //徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
        AttachBGMSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;
        if (AttachBGMSource.volume <= 0)
        {
            AttachBGMSource.Stop();
            AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
            _isFadeOut = false;

            if (!string.IsNullOrEmpty(_nextBGMName))
            {
                PlayBGM(_nextBGMName);
            }
        }
    }

    //=================================================================================
    //音量変更
    //=================================================================================

    /// <summary>
    /// BGMとSEのボリュームを別々に変更&保存
    /// </summary>
    public void ChangeVolume(float BGMVolume, float SEVolume)
    {
        AttachBGMSource.volume = BGMVolume;
        AttachSESource.volume = SEVolume;

        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);
    }

    //=================================================================================
    // BGM切替　志村追記
    //=================================================================================
    void BGM_CHANGE()
    {
        //Debug.Log(" ★★m_oldsceneName  " + m_oldsceneName);
        //Debug.Log(" ★★GetActiveScene  " + SceneManager.GetActiveScene().name);
        //フラグ切替処理
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != m_oldsceneName)
        {
            //シーン記録情報の更新
            m_oldsceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            //Debug.Log("音楽へんっこう！！");
            m_bgmchange = true;
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Game")
        {
            if (m_GameBGM == false)
            {
                m_GameBGM = true;
            }
        }
        

        //音楽再生処理
        if (m_bgmchange == true)
        {
            //Debug.Log("音変更♪～♪♪～♪～");
            //現在のシーンを調べてBGMを再生する
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Title")
            {
                PlayBGM(AUDIO.BGM_TITLE);
            }
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Game")
            {
                PlayBGM(AUDIO.BGM_GAME);
            }
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Result")
            {
                PlayBGM(AUDIO.BGM_RESULT);
            }
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Fade")
            {
                FadeOutBGM();
            }
            m_bgmchange = false;
        }
    }
}