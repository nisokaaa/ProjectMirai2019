using UnityEngine;
using System.Collections;

using DigitalRuby.ThunderAndLightning;

namespace DigitalRuby.ThunderAndLightning
{
    public class DemoScriptPrefabTutorial : MonoBehaviour
    {
        public LightningBoltPrefabScript LightningScript;

        private void Start()
        {

        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                LightningScript.Trigger();
            }
        }
    }
}
