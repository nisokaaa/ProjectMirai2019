//
// Procedural Lightning for Unity
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using UnityEngine;
using System.Collections.Generic;

namespace DigitalRuby.ThunderAndLightning
{
    public class DemoScriptTriggerPath : MonoBehaviour
    {
        public LightningSplineScript Script;
        public UnityEngine.UI.Toggle SplineToggle;

        private readonly List<Vector3> points = new List<Vector3>();

        private void Start()
        {
            Script.ManualMode = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DemoScript.ReloadCurrentScene();
                return;
            }
            else if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Camera.main.orthographic)
                {
                    worldPos.z = 0.0f;
                }
                if (points.Count == 0 || (points[points.Count - 1] - worldPos).magnitude > 8.0f)
                {
                    points.Add(worldPos);
                    Script.Trigger(points, SplineToggle.isOn);
                }
            }
        }
    }
}