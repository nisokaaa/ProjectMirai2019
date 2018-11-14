using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] string BGM;

	// Use this for initialization
	void Start () {
        Sound.LoadBgm("GAME_BGM", BGM);
        Sound.PlayBgm("GAME_BGM");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
