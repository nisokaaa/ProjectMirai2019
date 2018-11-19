using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttraction : MonoBehaviour {

    public Transform Target;

    private ParticleSystem system;

    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (system == null)
        {
            system = GetComponent<ParticleSystem>();
        }

        var count = system.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            var particle = particles[i];

            float distance = Vector3.Distance(particle.position, Target.position);

            Vector3 v1 = system.transform.TransformPoint(particle.position);
            Vector3 v2 = Target.transform.position;

            //パーティクル生成残り時間に応じて距離をつめる
            float lifeDelta = 1.0f - (particle.remainingLifetime / particle.startLifetime);

            Vector3 dist = system.transform.InverseTransformPoint(Vector3.Lerp(v1, v2, lifeDelta));
            particle.position = dist;
            particles[i] = particle;

        }

        system.SetParticles(particles, count);
    }
}
