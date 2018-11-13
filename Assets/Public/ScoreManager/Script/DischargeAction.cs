using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 常時放電
     */
public class DischargeAction : MonoBehaviour {

    [SerializeField]GameObject particleSystem;
    ScoreManager scoreManager;
    ElecBarControl elecBarControl;

    enum ELEC_MODE
    {
        NONE = 0,
        START,
        EXECUTION,
        END
    }
    [SerializeField] ELEC_MODE mode = ELEC_MODE.NONE;
    int addScoreCnt = 0;

    // Use this for initialization
    void Start () {
        particleSystem = Instantiate(particleSystem,transform.position,Quaternion.identity) as GameObject;
        particleSystem.SetActive(false);

        if (scoreManager == null)
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        }
        if (elecBarControl == null)
        {
            elecBarControl = GameObject.Find("ElecBarController").GetComponent<ElecBarControl>();
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (elecBarControl.GetGageValue() > 0.0f)
        {
            mode = ELEC_MODE.START;
        }else if(mode == ELEC_MODE.EXECUTION)
        {
            mode = ELEC_MODE.END;
        }

        if(mode == ELEC_MODE.END)
        {
            particleSystem.SetActive(false);
            mode = ELEC_MODE.NONE;
        }

        //常時放電
        PowerSharing();
    }

    //常時放電
    void PowerSharing()
    {

        if(mode == ELEC_MODE.NONE || mode == ELEC_MODE.EXECUTION)
        {
            return;
        }
        //エフェクト表示
        particleSystem.SetActive(true);
        particleSystem.transform.position = transform.position;

        //ゲージ減少処理
        elecBarControl.Decrease();

        //スコア上昇処理
        addScoreCnt++;
        if(addScoreCnt > 60)
        {
            addScoreCnt = 0;
            scoreManager.AddScoreValue(10);
        }
        mode = ELEC_MODE.EXECUTION;
    }
}
