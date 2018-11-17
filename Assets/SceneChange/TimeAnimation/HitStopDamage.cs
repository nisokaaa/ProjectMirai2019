using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopDamage : MonoBehaviour {

    [SerializeField]
    private GameObject damagePrefab;

    [SerializeField]
    private TimeManager timeManager;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Obstacle")
        {
            var damageParticle = GameObject.Instantiate(damagePrefab, col.ClosestPointOnBounds(col.transform.position), Quaternion.identity) as GameObject;
            //　全体のタイムスケールを変更する
            timeManager.SlowDown();
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            var damageParticle = GameObject.Instantiate(damagePrefab, transform.position, Quaternion.identity) as GameObject;
            //　全体のタイムスケールを変更する
            timeManager.SlowDown();
        }
    }
}
