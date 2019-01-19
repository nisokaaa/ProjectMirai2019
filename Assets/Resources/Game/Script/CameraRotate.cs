using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    [SerializeField, Range(0.01f, 0.4f)]
    private float rotationSpeedX = 0.2f;

    [SerializeField, Range(0.01f, 0.4f)]
    private float rotationSpeedY = 0.2f;
    
    private Vector2 prePosition;
    private Vector2 newAngle;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    private Vector2 m_joyconAngle;
    public bool debug = false;
    
    

    // Use this for initialization
    void Start () {
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }

    // Update is called once per frame
    void Update()
    {
        if (debug == true) {
            if (!(m_joycons.Count <= 0 || m_joycons == null))
            {
                m_joyconAngle *= 0.88f;
                m_joyconAngle.x -= m_joyconR.GetStick()[1] * (rotationSpeedX * 10);
                m_joyconAngle.y += m_joyconR.GetStick()[0] * (rotationSpeedY * 10);
                transform.localEulerAngles = new Vector3(m_joyconAngle.x, m_joyconAngle.y, 0f);
            }
        }

        //if (m_joyconR.GetStick()[0] > 0.1f)
        //{
        //    m_joyconAngle.x += m_joyconR.GetStick()[0];
        //    // カメラに回転量代入
        //    transform.localEulerAngles = new Vector3(m_joyconAngle.x , m_joyconAngle.y, 0f);
        //}
        //if ()
        //{
        //    newAngle.x += m_joyconR.GetStick()[0];
        //    // カメラに回転量代入
        //    transform.localEulerAngles = new Vector3(m_joyconAngle.x, m_joyconAngle.y, 0f);
        //}

        // 右クリックした時
        if (Input.GetMouseButtonDown(1))
        {
            newAngle = transform.localEulerAngles;
            prePosition = Input.mousePosition;
        }
        // 右ドラッグしている時
        else if (Input.GetMouseButton(1))
        {
            // Y軸の回転量を求める
            newAngle.y += (Input.mousePosition.x - prePosition.x) * rotationSpeedY;

            // X軸の回転量を求める
            newAngle.x -= (Input.mousePosition.y - prePosition.y) * rotationSpeedX;

            // カメラに回転量代入
            transform.localEulerAngles = new Vector3(newAngle.x, newAngle.y, 0f);

            // 現フレームのマウス座標保存
            prePosition = Input.mousePosition;
        }
    }
}
