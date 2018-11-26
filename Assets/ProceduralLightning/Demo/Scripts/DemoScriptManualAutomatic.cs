using UnityEngine;
using System.Collections;

namespace DigitalRuby.ThunderAndLightning
{
    public class DemoScriptManualAutomatic : MonoBehaviour
    {
        public GameObject LightningPrefab;
        public UnityEngine.UI.Toggle AutomaticToggle;
        public Transform a;
        public Transform b;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPos.z = 0.0f;
                // LightningPrefab.GetComponent<DigitalRuby.ThunderAndLightning.LightningBoltPrefabScriptBase>().Trigger(null, worldPos);
                LightningPrefab.GetComponent<DigitalRuby.ThunderAndLightning.LightningBoltPrefabScriptBase>().Trigger(a.position, b.position);
            }
        }

        public void AutomaticToggled()
        {
            LightningPrefab.GetComponent<DigitalRuby.ThunderAndLightning.LightningBoltPrefabScriptBase>().ManualMode = !AutomaticToggle.isOn;
        }

        public void ManualTriggerClicked()
        {
            LightningPrefab.GetComponent<DigitalRuby.ThunderAndLightning.LightningBoltPrefabScriptBase>().Trigger();
        }
    }
}