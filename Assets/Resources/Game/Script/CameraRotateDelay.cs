using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateDelay : MonoBehaviour {
   
    [SerializeField, Range(0f, 5f)]
    private float rotSpeed = 2f;

    private Transform player;
    private Transform cam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = this.transform;
    }

    void FixedUpdate () {
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, Time.deltaTime * rotSpeed);
    }
}
