using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=================================================================================
//	処理			：	サウンドデータのシングルトン管理・BGM,SE 読み込み/再生/停止
//  説明            ：　PlayBgm()/PlaySe()を使う前にLoadBgm()/LoadSe()を呼ぶこと
//	作成者			：	moriya gaku 2018 06 01
//=================================================================================
/// <summary>
/// サウンドデータクラス
/// </summary>
class Data
{
    // アクセス用のキー
    public string Key;

    // リソース名
    public string ResName;

    // AudioClip
    public AudioClip Clip;

    // コンストラクタ
    public Data(string key, string res)
    {
        Key = key;
        ResName = "Sounds/" + res;

        // AudioClipの取得
        Clip = Resources.Load(ResName) as AudioClip;
    }
}

/// <summary>
/// サウンド管理クラス
/// </summary>
public class Sound {

    // チャンネル数
    const int SE_CHANNEL = 4;

    // サウンド種別
    enum eType
    {
        Bgm = 0,
        Se,
    }

    // シングルトン
    static Sound Singleton = null;

    // インスタンス取得
    public static Sound GetInstance()
    {
        // singletonがnullだったら右辺代入
        return Singleton ?? (Singleton = new Sound());
    }

    // サウンド再生のためのゲームオブジェクト
    GameObject Object = null;

    // サウンドリソース
    AudioSource SourceBgm = null;
    AudioSource SourceSeDefault = null;
    AudioSource[] SourceSeArray;

    // BGMにアクセスするためのテーブル
    Dictionary<string, Data> BgmTable = new Dictionary<string, Data>();

    // SEにアクセスするためのテーブル
    Dictionary<string, Data> SeTable = new Dictionary<string, Data>();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Sound()
    {
        // チャンネル確保
        SourceSeArray = new AudioSource[SE_CHANNEL];
    }

    /// <summary>
    /// BGMのロード
    /// </summary>
    /// <param name="key"> 検索用のキー </param>
    /// <param name="resName"> 音声ファイル名 </param>
    public static void LoadBgm(string key, string resName)
    {
        GetInstance()._LoadBgm(key, resName);
    }
    void _LoadBgm(string key, string resName)
    {
        if (BgmTable.ContainsKey(key))
        {
            // 既に登録済みなので消す
            BgmTable.Remove(key);
        }

        BgmTable.Add(key, new Data(key, resName));
    }

    /// <summary>
    /// SEのロード
    /// </summary>
    /// <param name="key"> 検索用のキー </param>
    /// <param name="resName"> 音声ファイル名 </param>
    public static void LoadSe(string key, string resName)
    {
        GetInstance()._LoadSe(key, resName);
    }
    void _LoadSe(string key, string resName)
    {
        if (SeTable.ContainsKey(key))
        {
            // すでに登録済みなのでいったん消す
            SeTable.Remove(key);
        }
        SeTable.Add(key, new Data(key, resName));
    }

    /// <summary>
    /// AudioSourceを取得する
    /// </summary>
    /// <param name="type"> 音声のタイプ(BGM/SE) </param>
    /// <param name="channel"> 配列の添字 </param>
    /// <returns> AudioSourceデータ </returns>
    AudioSource GetAudioSource(eType type, int channel = -1)
    {
        if (Object == null)
        {
            // GameObjectが無ければ作る
            Object = new GameObject("Sound");

            // 破棄しないようにする
            GameObject.DontDestroyOnLoad(Object);

            // AudioSourceを作成
            SourceBgm = Object.AddComponent<AudioSource>();
            SourceSeDefault = Object.AddComponent<AudioSource>();

            for (int i = 0; i < SE_CHANNEL; i++)
            {
                SourceSeArray[i] = Object.AddComponent<AudioSource>();
            }
        }

        if (type == eType.Bgm)
        {
            // BGM
            return SourceBgm;
        }
        else
        {
            // SE
            if (0 <= channel && channel < SE_CHANNEL)
            {
                // チャンネル指定
                return SourceSeArray[channel];
            }
            else
            {
                return SourceSeDefault;
            }
        }
    }

    /// <summary>
    /// BGMの再生
    /// </summary>
    /// <param name="key"> 検索用のキー </param>
    /// <returns> 再生の可否 </returns>
    public static bool PlayBgm(string key)
    {
        return GetInstance()._PlayBgm(key);
    }
    bool _PlayBgm(string key)
    {
        if(!BgmTable.ContainsKey(key))
        {
            // 対応するキーがない
            return false;
        }

        // 止める
        StopBgm();

        // リソースの取得
        var data = BgmTable[key];

        // 再生
        var source = GetAudioSource(eType.Bgm);
        source.loop = true;
        source.clip = data.Clip;
        source.Play();

        return true;
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="key"> 検索用のキー </param>
    /// <returns> 再生の可否 </returns>
    public static bool PlaySe(string key)
    {
        return GetInstance()._PlaySe(key);
    }
    bool _PlaySe(string key, int channel = -1)
    {
        if(!SeTable.ContainsKey(key))
        {
            // 対応するキーがない
            return false;
        }

        // リソースの取得
        var data = SeTable[key];

        if(0 <= channel && channel < SE_CHANNEL)
        {
            // チャンネル指定
            var source = GetAudioSource(eType.Se, channel);
            source.clip = data.Clip;
            source.Play();
        }
        else
        {
            // デフォルトで再生
            var source = GetAudioSource(eType.Se);
            source.PlayOneShot(data.Clip);
        }

        return true;
    }

    /// <summary>
    /// サウンドの停止
    /// </summary>
    /// <returns> 停止の可否 </returns>
    public static bool StopBgm()
    {
        return GetInstance().Stop();
    }
    bool Stop()
    {
        
        GetAudioSource(eType.Bgm).Stop();

        return true;
    }
}