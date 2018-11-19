//
// Procedural Lightning for Unity
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
    public class DemoScriptMoveRandomly : MonoBehaviour
    {
        private float elapsed = float.MaxValue;
        private Vector3 startStartPos;
        private Vector3 startEndPos;
        private Vector3 endStartPos;
        private Vector3 endEndPos;

        [Tooltip("The transform to move randomly.")]
        public Transform Transform1;

        [Tooltip("The transform to move randomly.")]
        public Transform Transform2;

        [Tooltip("How long until the start and end objects change positions again. Set to 0 to not move the objects.")]
        [Range(0.0f, 5.0f)]
        public float MoveTimeSeconds = 1.0f;

        private void Start()
        {

        }

        private void Update()
        {
            if (MoveTimeSeconds <= 0.0f)
            {
                return;
            }
            else if (elapsed >= MoveTimeSeconds)
            {
                elapsed = 0.0f;
                Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 10.0f));
                Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 10.0f));
                startStartPos = Transform1.transform.position;
                endStartPos = Transform2.transform.position;
                startEndPos = new Vector3(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y), 0.0f);
                endEndPos = new Vector3(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y), 0.0f);
            }
            elapsed += LightningBoltScript.DeltaTime;
            Transform1.position = Vector3.Lerp(startStartPos, startEndPos, elapsed / MoveTimeSeconds);
            Transform2.position = Vector3.Lerp(endStartPos, endEndPos, elapsed / MoveTimeSeconds);
        }
    }
}
