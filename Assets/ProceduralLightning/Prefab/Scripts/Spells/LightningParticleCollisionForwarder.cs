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
using System.Collections.Generic;

namespace DigitalRuby.ThunderAndLightning
{

#if UNITY_4

	public interface ICollisionHandler
	{
		void HandleCollision(GameObject obj, ParticleSystem.CollisionEvent[] positions, int collisionCount);
	}

#else

    public interface ICollisionHandler
    {
        void HandleCollision(GameObject obj, List<ParticleCollisionEvent> collision, int collisionCount);
    }

#endif

    /// <summary>
    /// This script simply allows forwarding collision events for the objects that collide with something. This
    /// allows you to have a generic collision handler and attach a collision forwarder to your child objects.
    /// In addition, you also get access to the game object that is colliding, along with the object being
    /// collided into, which is helpful.
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class LightningParticleCollisionForwarder : MonoBehaviour
    {
        [Tooltip("The script to forward the collision to. Must implement ICollisionHandler.")]
        public MonoBehaviour CollisionHandler;

        private ParticleSystem _particleSystem;

#if UNITY_4

		private ParticleSystem.CollisionEvent[] collisionEvents = new ParticleSystem.CollisionEvent[16];

#elif !UNITY_5_3_OR_NEWER

        private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];

#else

        private readonly List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

#endif

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void OnParticleCollision(GameObject other)
        {
            ICollisionHandler i = CollisionHandler as ICollisionHandler;
            if (i != null)
            {

#if UNITY_4

				int numCollisionEvents = _particleSystem.GetCollisionEvents(other, collisionEvents);
				if (numCollisionEvents != 0)
				{
					i.HandleCollision(other, collisionEvents, numCollisionEvents);
				}

#elif UNITY_5_3_OR_NEWER

                int numCollisionEvents = _particleSystem.GetCollisionEvents(other, collisionEvents);
                if (numCollisionEvents != 0)
                {
                    i.HandleCollision(other, collisionEvents, numCollisionEvents);
                }

#else

                int numCollisionEvents = _particleSystem.GetCollisionEvents(other, collisionEvents);
                if (numCollisionEvents != 0)
                {
                    i.HandleCollision(other, new List<ParticleCollisionEvent>(collisionEvents), numCollisionEvents);
                }

#endif

            }
        }

        /*
        public void OnCollisionEnter(Collision col)
        {
            ICollisionHandler i = CollisionHandler as ICollisionHandler;
            if (i != null)
            {
                i.HandleCollision(gameObject, col);
            }
        }
        */
    }
}