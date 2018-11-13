/*
 http://baba-s.hatenablog.com/entry/2017/11/12/090000
 */
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Example : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    private void Start()
    {
        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }

    private void Update()
    {
        m_pressedButtonL = null;
        m_pressedButtonR = null;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        //ボタンの数だけ繰り返す
        foreach (var button in m_buttons)
        {
            //左側のボタンが押されたとき
            if (m_joyconL.GetButton(button))
            {//押されているボタン情報を格納する
                m_pressedButtonL = button;
            }
            //右側のボタンが押されたとき
            if (m_joyconR.GetButton(button))
            {//押されているボタン情報を格納する
                m_pressedButtonR = button;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_joyconL.SetRumble(160, 320, 0.6f, 200);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_joyconR.SetRumble(160, 320, 0.6f, 200);
        }
    }

    private void OnGUI()
    {
        var style = GUI.skin.GetStyle("label");
        style.fontSize = 24;

        if (m_joycons == null || m_joycons.Count <= 0)
        {
            GUILayout.Label("Joy-Con が接続されていません");
            return;
        }

        if (!m_joycons.Any(c => c.isLeft))
        {
            GUILayout.Label("Joy-Con (L) が接続されていません");
            return;
        }

        if (!m_joycons.Any(c => !c.isLeft))
        {
            GUILayout.Label("Joy-Con (R) が接続されていません");
            return;
        }

        GUILayout.BeginHorizontal(GUILayout.Width(960));

        foreach (var joycon in m_joycons)
        {
            var isLeft = joycon.isLeft;
            var name = isLeft ? "Joy-Con (L)" : "Joy-Con (R)";
            var key = isLeft ? "Z キー" : "X キー";
            var button = isLeft ? m_pressedButtonL : m_pressedButtonR;
            var stick = joycon.GetStick();
            var gyro = joycon.GetGyro();
            var accel = joycon.GetAccel();
            var orientation = joycon.GetVector();

            GUILayout.BeginVertical(GUILayout.Width(480));
            GUILayout.Label(name);
            GUILayout.Label(key + "：振動");
            GUILayout.Label("押されているボタン：" + button);
            GUILayout.Label(string.Format("スティック：({0}, {1})", stick[0], stick[1]));
            GUILayout.Label("ジャイロ：" + gyro);
            GUILayout.Label("加速度：" + accel);
            GUILayout.Label("傾き：" + orientation);
            GUILayout.EndVertical();
        }

        GUILayout.EndHorizontal();
    }

    //サンプルコード
    void SampleChord()
    {
        if (m_joyconL.GetButton(Joycon.Button.DPAD_RIGHT))
        {
            // 右ボタンが押されている
        }
        if (m_joyconL.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            // 右ボタンが押された
        }
        if (m_joyconL.GetButtonUp(Joycon.Button.DPAD_RIGHT))
        {
            // 右ボタンが離された
        }

        var stick = m_joyconL.GetStick();  // スティック
        var gyro = m_joyconL.GetGyro();   // ジャイロ
        var accel = m_joyconL.GetAccel();  // 加速度
        var orientation = m_joyconL.GetVector(); // 傾き

        m_joyconL.SetRumble(160, 320, 0.6f, 200); // 振動
    }
}