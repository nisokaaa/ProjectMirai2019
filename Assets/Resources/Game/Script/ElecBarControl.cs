using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElecBarControl : MonoBehaviour {

    Slider slider;

    [SerializeField, Range(0f,1f)]
    public float increase = 0.01f;

    private Image elecBarBackground;
    private Image elecBarFill;

    [SerializeField, Range(0f, 1f)]
    public float fadeDuration = 0.5f;

    [SerializeField]
    GameObject _particleSystem;

    bool _efect = false;
    void Start()
    {
        // スライダーを取得する
        slider = GameObject.Find("ElecBar").GetComponent<Slider>();

        // スライダー背景
        elecBarBackground = GameObject.Find("ElecBarBackground").GetComponent<Image>();

        // スライダーゲージ
        elecBarFill = GameObject.Find("ElecBarFill").GetComponent<Image>();

        _particleSystem.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            _particleSystem.SetActive(true);

            Increase();
            Increase();
            Increase();
            Increase();
            Increase();
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            _particleSystem.SetActive(false);
        }
            if (Input.GetKey(KeyCode.UpArrow))
        {
            Increase();
            Increase();
            Increase();
            _particleSystem.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _particleSystem.SetActive(false);
        }

            if (Input.GetKey(KeyCode.DownArrow))
        {
            Decrease();
        }
    }

    /// <summary>
    /// 増加する
    /// </summary>
    public void Increase()
    {
        if (slider.value < slider.maxValue)
        {
            slider.value += increase;
        }
    }

    /// <summary>
    /// 減少する
    /// </summary>
    public void Decrease()
    {
        if (slider.value > slider.minValue)
        {
            slider.value -= increase;
        }
    }

    /// <summary>
    /// 不透明化する
    /// </summary>
    public void FadeIn()
    {
        elecBarBackground.CrossFadeAlpha(1, fadeDuration, true);
        elecBarFill.CrossFadeAlpha(1, fadeDuration, true);
    }

    /// <summary>
    /// 透明化する
    /// </summary>
    public void FadeOut()
    {
        elecBarBackground.CrossFadeAlpha(0, fadeDuration, true);
        elecBarFill.CrossFadeAlpha(0, fadeDuration, true);
    }

    public float GetGageValue()
    {
        return slider.value;
    }
    public void SetEffectOn()
    {
        _particleSystem.SetActive(true);
    }
    public void SetEffectOff()
    {
        _particleSystem.SetActive(false);
    }
}