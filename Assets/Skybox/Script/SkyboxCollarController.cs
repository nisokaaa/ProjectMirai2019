using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「スカイボックスの色を時間変化によって夕方から夜に変化させるクラス」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：名前
/// </summary>
public class SkyboxCollarController : MonoBehaviour {

    [SerializeField] Material _skyboxMaterial;                  //編集マテリアル

    /// <summary>
    /// 色の変化値
    /// </summary>
    [SerializeField, Range(0, 5)] float _AtmoshpereThickness = 3.00f;
    [SerializeField ,Range(0, 5)] float _AtmoshpereThicknessReset = 2.00f;
    [SerializeField] float _hourStepFromSeconds;				//時間が1秒にどれくらいの速さで進むか
    [SerializeField, Range(0, 23)] float _currentHour;          //現在の時間
    public float _totalHour = 0.0f;                             //総合計経過時間
    [SerializeField] float _AtmoshpereThicknessTime = 60.00f;
    [SerializeField] float _AtmoshpereThicknessPurposeValue = 0.62f;//
    float _AtmoshpereThicknessStartLog = 0.0f;

    //[SerializeField, Range(4, 8)] int dayStartHour;           //昼が始まる時間
    //[SerializeField, Range(18, 23)] int nightStartHour;       //夜が始まる時間
    //
    //[SerializeField] float sunDuration;                                      //昼の時間が進んだ割合
    //float moonDuration;										//夜の時間が進んだ割合
    //[SerializeField] float _cnt;


    readonly string skyColorName = "_AtmosphereThickness";				//MaterialのColorを設定するときのパラメータ名
    readonly string skyLiColorName = "_Exposure";				//MaterialのColorを設定するときのパラメータ名
    private bool _started = false;

    // Use this for initialization
    void Start () {
        _AtmoshpereThicknessStartLog = _AtmoshpereThicknessPurposeValue + _AtmoshpereThicknessPurposeValue - (_AtmoshpereThicknessPurposeValue * (_totalHour / _AtmoshpereThicknessTime));
    }
	
	// Update is called once per frame
	void Update () {
        _started = Application.isPlaying;
        TimeUpdate();
        SkyboxUpdate();
    }

    void TimeUpdate()
    {
        //現在時間を加算して更新する
        _currentHour += _hourStepFromSeconds * Time.deltaTime;

        //総合計経過時間を更新する
        _totalHour += _hourStepFromSeconds * Time.deltaTime;

        //24時を0時にする
        if (_currentHour >= 24.0f)
        {
            _currentHour = 0.0f;
        }
    }

    void SkyboxUpdate()
    {
        //指定の時間がたったら処理を実行しない
        if(_AtmoshpereThicknessTime < _totalHour)
        {
            return;
        }

        //　目的地の数値 + (目的地に行くまでの時間)
        // 目的地に行くまでの時間 = 目的地の数値 - 目的地の数値 * (0~1) //数値が反転する
        _AtmoshpereThickness = _AtmoshpereThicknessPurposeValue + _AtmoshpereThicknessPurposeValue - (_AtmoshpereThicknessPurposeValue * (_totalHour / _AtmoshpereThicknessTime));

        //指定の時間で、目的の数値まで計算し続ける式
        //_AtmoshpereThickness =  _AtmoshpereThicknessPurposeValue - (_AtmoshpereThicknessPurposeValue * (_totalHour / _AtmoshpereThicknessTime));

        //色の変化値の0チェック
        if (_AtmoshpereThickness <= 0.0f)
        {
            _AtmoshpereThickness = 0.0f;
            return;
        }

        //スカイボックスのマテリアル設定を変える
        _skyboxMaterial.SetFloat(skyColorName, _AtmoshpereThickness); 
        _skyboxMaterial.SetFloat(skyLiColorName, 0.3f - (0.3f * (_totalHour / _AtmoshpereThicknessTime)));


    }

    private bool started = false;

    void OnDestroy()
    {
        if (_started)
        {
            if (!Application.isPlaying)
            {
                // エディタ終了時の処理
                _skyboxMaterial.SetFloat(skyColorName, _AtmoshpereThicknessStartLog);
            }
            else
            {
                // 普通のプレイ終了時の処理
                _skyboxMaterial.SetFloat(skyColorName, _AtmoshpereThicknessStartLog);
            }
        }
    }
}
