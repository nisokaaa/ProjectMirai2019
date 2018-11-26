using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile2 : MonoBehaviour {

    public Transform target;
    public Vector3 velocity;
    public Vector3 position;
    public float period;

    //最大加速度
    public float maxAcceleration = 100;

    //ランダムに加える力
    public float randomPower;
    public float randomPeriod;

    void Update()
    {
        if (target == null)
            return;

        var acceleration = Vector3.zero;
        var diff = target.position - position;

        //速度velocityの物体がperiod秒後にdiff進むための加速度
        acceleration += (diff - velocity * period) * 2f / (period * period);

        if (0 < randomPeriod)
        {
            var xr = Random.Range(-randomPower, randomPower);
            var yr = Random.Range(-randomPower, randomPower);
            var zr = Random.Range(-randomPower, randomPower);
            acceleration += new Vector3(xr, yr, zr);
        }

        if (acceleration.magnitude > maxAcceleration)
        {
            acceleration = acceleration.normalized * maxAcceleration;
        }

        period -= Time.deltaTime;
        randomPeriod -= Time.deltaTime;
        if (period < 0f)
            return;

        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        transform.position = position;
    }
}
