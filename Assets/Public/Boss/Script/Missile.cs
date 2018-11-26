using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public Transform target;
    public float torqueRatio;
    public float speed;

    void FixedUpdate()
    {
        if (target == null)
            return;

        //向きをターゲットの方に向ける
        var diff = target.transform.position - transform.position;
        var target_rot = Quaternion.LookRotation(diff);
        var rot = target_rot * Quaternion.Inverse(transform.rotation);
        if (rot.w < 0f)
        {
            rot.x = -rot.x;
            rot.y = -rot.y;
            rot.z = -rot.z;
            rot.w = -rot.w;
        }
        var torque = new Vector3(rot.x, rot.y, rot.z) * torqueRatio;
        GetComponent<Rigidbody>().AddTorque(torque);

        //まっすぐ進む
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
