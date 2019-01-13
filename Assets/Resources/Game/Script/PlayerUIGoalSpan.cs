using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIGoalSpan : MonoBehaviour {
    
    Vector3 Apos;
    Vector3 Bpos;

    public GameObject objA;
    public GameObject objB;
    float span = 0;
    bool check = false;
    Text text;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        Apos = objA.transform.position;
        Bpos = objB.transform.position;
        span = Vector3.Distance(Apos, Bpos);
    }
	
	// Update is called once per frame
	void Update () {
        if(check == true)
        {
            text.text = "Game Clear !!";
            return;
        }
        if(objA.transform.position.z > objB.transform.position.z)
        {
            text.text = "Game Clear !!";
            check = true;
        }
        Apos = objA.transform.position;
        Bpos = objB.transform.position;
        
        float dis = Vector3.Distance(Apos, Bpos);
        dis = dis / span * 100;


        text.text = dis.ToString("f1");
    }
}
