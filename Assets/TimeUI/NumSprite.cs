/*
 http://madgenius.hateblo.jp/entry/2017/09/08/141830
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 数字のスプライト表示
public class NumSprite : MonoBehaviour
{
    [SerializeField]
    private GameObject showSprite;  // スプライト表示用オブジェクト(プレハブ)

    // 数字スプライト
    [SerializeField]
    private Sprite _0;
    [SerializeField]
    private Sprite _1;
    [SerializeField]
    private Sprite _2;
    [SerializeField]
    private Sprite _3;
    [SerializeField]
    private Sprite _4;
    [SerializeField]
    private Sprite _5;
    [SerializeField]
    private Sprite _6;
    [SerializeField]
    private Sprite _7;
    [SerializeField]
    private Sprite _8;
    [SerializeField]
    private Sprite _9;
    [SerializeField]
    private Sprite _Minus;

    [SerializeField]
    float width;    // 数字の表示間隔

    private int showValue;  // 表示する値

    private GameObject[] numSpriteGird;         // 表示用スプライトオブジェクトの配列
    private Dictionary<char, Sprite> dicSprite; // スプライトディクショナリ

    // スプライトディクショナリを初期化する
    void Awake()
    {
        dicSprite = new Dictionary<char, Sprite>() {
            { '0', _0 },
            { '1', _1 },
            { '2', _2 },
            { '3', _3 },
            { '4', _4 },
            { '5', _5 },
            { '6', _6 },
            { '7', _7 },
            { '8', _8 },
            { '9', _9 },
            { '-', _Minus },
        };
    }

    private void Update()
    {
        var numSprite = GetComponent<NumSprite>();
        numSprite.Value = 1234; // ここで「1234」の値を指定
    }

    // 表示する値
    public int Value
    {
        get
        {
            return showValue;
        }
        set
        {
            showValue = value;

            // 表示文字列取得
            string strValue = value.ToString();

            // 現在表示中のオブジェクト削除
            if (numSpriteGird != null)
            {
                foreach (var numSprite in numSpriteGird)
                {
                    GameObject.Destroy(numSprite);
                }
            }

            // 表示桁数分だけオブジェクト作成
            numSpriteGird = new GameObject[strValue.Length];
            for (var i = 0; i < numSpriteGird.Length; ++i)
            {
                // オブジェクト作成
                numSpriteGird[i] = Instantiate(
                    showSprite,
                    transform.position + new Vector3((float)i * width, 0),
                    Quaternion.identity) as GameObject;

                // 表示する数値指定
                numSpriteGird[i].GetComponent<SpriteRenderer>().sprite = dicSprite[strValue[i]];

                // 自身の子階層に移動
                numSpriteGird[i].transform.parent = transform;
            }
        }
    }
}