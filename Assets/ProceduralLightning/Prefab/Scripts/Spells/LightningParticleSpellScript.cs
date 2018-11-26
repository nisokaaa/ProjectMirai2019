//
// Procedural Lightning for Unity
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9

#define UNITY_4

#endif

using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace DigitalRuby.ThunderAndLightning
{
    public class LightningParticleSpellScript : LightningSpellScript, ICollisionHandler
    {
        [Header("Particle system")]
        public ParticleSystem ParticleSystem;

        [Tooltip("Particle system collision interval. This time must elapse before another collision will be registered.")]
        public float CollisionInterval = 0.0f;
        protected float collisionTimer;

#if UNITY_4

		/// <summary>
		/// Particle system callback. Parameters are game object, collision events, and number of collision events
		/// </summary>
		[HideInInspector]
		public System.Action<GameObject, ParticleSystem.CollisionEvent[]> CollisionCallback;

#else

        /// <summary>
        /// Particle system callback. Parameters are game object, collision events, and number of collision events
        /// </summary>
        [HideInInspector]
        public System.Action<GameObject, List<ParticleCollisionEvent>, int> CollisionCallback;

#endif

        [Header("Particle Light Properties")]
        [Tooltip("Whether to enable point lights for the particles")]
        public bool EnableParticleLights = true;

        [SingleLineClamp("Possible range for particle lights", 0.001, 100.0f)]
        public RangeOfFloats ParticleLightRange = new RangeOfFloats { Minimum = 2.0f, Maximum = 5.0f };

        [SingleLineClamp("Possible range of intensity for particle lights", 0.01f, 8.0f)]
        public RangeOfFloats ParticleLightIntensity = new RangeOfFloats { Minimum = 0.2f, Maximum = 0.3f };

        [Tooltip("Possible range of colors for particle lights")]
        public Color ParticleLightColor1 = Color.white;

        [Tooltip("Possible range of colors for particle lights")]
        public Color ParticleLightColor2 = Color.white;

        [Tooltip("The culling mask for particle lights")]
        public LayerMask ParticleLightCullingMask = -1;

        private ParticleSystem.Particle[] particles = new ParticleSystem.Particle[512];
        private readonly List<GameObject> particleLights = new List<GameObject>();

        private void PopulateParticleLight(Light src)
        {

#if UNITY_4

#else

			src.bounceIntensity = 0.0f;

#endif

            src.type = LightType.Point;
            src.shadows = LightShadows.None;
            src.color = new Color
            (
                UnityEngine.Random.Range(ParticleLightColor1.r, ParticleLightColor2.r),
                UnityEngine.Random.Range(ParticleLightColor1.g, ParticleLightColor2.g),
                UnityEngine.Random.Range(ParticleLightColor1.b, ParticleLightColor2.b),
                1.0f
            );
            src.cullingMask = ParticleLightCullingMask;
            src.intensity = UnityEngine.Random.Range(ParticleLightIntensity.Minimum, ParticleLightIntensity.Maximum);
            src.range = UnityEngine.Random.Range(ParticleLightRange.Minimum, ParticleLightRange.Maximum);
        }

        private void UpdateParticleLights()
        {
            if (!EnableParticleLights)
            {
                return;
            }

            int count = ParticleSystem.GetParticles(particles);
            while (particleLights.Count < count)
            {
                GameObject lightObj = new GameObject("LightningParticleSpellLight");
                lightObj.hideFlags = HideFlags.HideAndDontSave;
                PopulateParticleLight(lightObj.AddComponent<Light>());
                particleLights.Add(lightObj);

            }
            while (particleLights.Count > count)
            {
                GameObject.Destroy(particleLights[particleLights.Count - 1]);
                particleLights.RemoveAt(particleLights.Count - 1);
            }
            for (int i = 0; i < count; i++)
            {
                particleLights[i].transform.position = particles[i].position;
            }
        }

        private void UpdateParticleSystems()
        {
            if (EmissionParticleSystem != null && EmissionParticleSystem.isPlaying)
            {
                EmissionParticleSystem.transform.position = SpellStart.transform.position;
                EmissionParticleSystem.transform.forward = Direction;
            }
            if (ParticleSystem != null)
            {
                if (ParticleSystem.isPlaying)
                {
                    ParticleSystem.transform.position = SpellStart.transform.position;
                    ParticleSystem.transform.forward = Direction;
                }
                UpdateParticleLights();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (GameObject l in particleLights)
            {
                GameObject.Destroy(l);
            }
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            UpdateParticleSystems();
            collisionTimer -= LightningBoltScript.DeltaTime;
        }

        protected override void OnCastSpell()
        {
            if (ParticleSystem != null)
            {
                ParticleSystem.Play();
                UpdateParticleSystems();
            }
        }

        protected override void OnStopSpell()
        {
            if (ParticleSystem != null)
            {
                ParticleSystem.Stop();
            }
        }

#if UNITY_4

		/// <summary>
		/// Handle a particle collision. Derived classes can override to provide custom logic.
		/// </summary>
		/// <param name="obj">Game Object</param>
		/// <param name="collisions">Collisions</param>
		/// <param name="numCollisions">Number of collisions</param>
		void ICollisionHandler.HandleCollision(GameObject obj, ParticleSystem.CollisionEvent[] collisions, int numCollisions)
		{
			if (collisionTimer <= 0.0f)
			{
				collisionTimer = CollisionInterval;
				PlayCollisionSound(collisions[0].intersection);
				ApplyCollisionForce(collisions[0].intersection);
				if (CollisionCallback != null)
				{
					CollisionCallback(obj, collisions);
				}
			}
		}

#else

        /// <summary>
        /// Handle a particle collision. Derived classes can override to provide custom logic.
        /// </summary>
        /// <param name="obj">Game Object</param>
        /// <param name="collisions">Collisions</param>
        /// <param name="collisionCount">Number of collisions</param>
        void ICollisionHandler.HandleCollision(GameObject obj, List<ParticleCollisionEvent> collisions, int collisionCount)
        {
            if (collisionTimer <= 0.0f)
            {
                collisionTimer = CollisionInterval;
                PlayCollisionSound(collisions[0].intersection);
                ApplyCollisionForce(collisions[0].intersection);
                if (CollisionCallback != null)
                {
                    CollisionCallback(obj, collisions, collisionCount);
                }
            }
        }

#endif

    }
}