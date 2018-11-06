using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    [SerializeField] GameObject PlayerGameObject;

    enum STATE
    {
        NONE = 0,
        START,      //ゲームスタート演出
        BATTLE,     //バトル中の処理、追従
        END,        //退却処理
    };

    [SerializeField] STATE state;
    [SerializeField] Vector3 SeekPos;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(
            PlayerGameObject.transform.position.x + SeekPos.x,
            PlayerGameObject.transform.position.y + SeekPos.y,
            PlayerGameObject.transform.position.z + SeekPos.z);
	}
}
