using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    [SerializeField, Range(1.0f, 50.0f)]
    private float zoomSpeed = 25.0f;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel != 0.0f)
        {
            transform.position += transform.forward * scrollWheel * zoomSpeed;
        }
    }
}
