using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionSeek : MonoBehaviour {

    [SerializeField] Transform SetPosition;
    [SerializeField] Vector3 SeekPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(SetPosition.position.x + SeekPosition.x, SetPosition.position.y + SeekPosition.y, SetPosition.position.z + SeekPosition.z);
	}
}
