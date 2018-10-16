using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkydomeController : MonoBehaviour {

    readonly string skyLiColorName = "Offset";               //MaterialのColorを設定するときのパラメータ名
    [SerializeField] Material _skyboxMaterial;                  //編集マテリアル
    float cnt = 0.0f;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cnt += 0.01f;
        if(cnt >= 1.0f)
        {
            cnt = 0.0f;
        }
        _skyboxMaterial.SetFloat(skyLiColorName, cnt);
    }
}
