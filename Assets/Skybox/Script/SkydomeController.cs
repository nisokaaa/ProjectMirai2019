using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkydomeController : MonoBehaviour {

    [SerializeField] Material _skyboxMaterial;                  //編集マテリアル
    float cnt = 0.0f;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //メインカメラの座標を取得して、そこにスカイドームの位置を上書きする
        transform.position = Camera.main.transform.position;
    }
}
