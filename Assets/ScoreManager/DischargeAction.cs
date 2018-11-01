using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DischargeAction : MonoBehaviour {

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
		if(scoreManager == null)
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
        }else
        {
            mode = ELEC_MODE.NONE;
        }

        PowerSharing();
    }

    void PowerSharing()
    {

        if(mode == ELEC_MODE.NONE || mode == ELEC_MODE.EXECUTION)
        {
            return;
        }
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
