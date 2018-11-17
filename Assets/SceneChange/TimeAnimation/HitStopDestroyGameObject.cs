using UnityEngine;
using System.Collections;
public class HitStopDestroyGameObject : MonoBehaviour
{
    private ParticleSystem particle;
    // Use this for initialization
    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        if (particle.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
