using UnityEngine;
using System.Collections;

namespace DigitalRuby.ThunderAndLightning
{
    public class DemoScriptRotate : MonoBehaviour
    {
        public Vector3 Rotation;

        private void Update()
        {
            gameObject.transform.Rotate(Rotation * LightningBoltScript.DeltaTime);
        }
    }
}