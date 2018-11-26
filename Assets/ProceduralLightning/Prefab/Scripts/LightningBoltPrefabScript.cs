//
// Procedural Lightning for Unity
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

#define SHOW_MANUAL_WARNING

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DigitalRuby.ThunderAndLightning
{
    public abstract class LightningBoltPrefabScriptBase : LightningBoltScript
    {

#if DEBUG && SHOW_MANUAL_WARNING

        private static bool showedManualWarning;

#endif

        private readonly List<LightningBoltParameters> batchParameters = new List<LightningBoltParameters>();
        private readonly System.Random random = new System.Random();

        [Header("Lightning Spawn Properties")]
        [SingleLineClamp("How long to wait before creating another round of lightning bolts in seconds", 0.001, double.MaxValue)]
        public RangeOfFloats IntervalRange = new RangeOfFloats { Minimum = 0.05f, Maximum = 0.1f };

        [SingleLineClamp("How many lightning bolts to emit for each interval", 0.0, 100.0)]
        public RangeOfIntegers CountRange = new RangeOfIntegers { Minimum = 1, Maximum = 1 };

        [Tooltip("Reduces the probability that additional bolts from CountRange will actually happen (0 - 1).")]
        [Range(0.0f, 1.0f)]
        public float CountProbabilityModifier = 1.0f;

        [SingleLineClamp("Delay in seconds (range) before each additional lightning bolt in count range is emitted", 0.0f, 30.0f)]
        public RangeOfFloats DelayRange = new RangeOfFloats { Minimum = 0.0f, Maximum = 0.0f };

        [SingleLineClamp("For each bolt emitted, how long should it stay in seconds", 0.01, 10.0)]
        public RangeOfFloats DurationRange = new RangeOfFloats { Minimum = 0.06f, Maximum = 0.12f };

        [Header("Lightning Appearance Properties")]
        [SingleLineClamp("The trunk width range in unity units (x = min, y = max)", 0.0001, 100.0)]
        public RangeOfFloats TrunkWidthRange = new RangeOfFloats { Minimum = 0.1f, Maximum = 0.2f };

        [Tooltip("How long (in seconds) this game object should live before destroying itself. Leave as 0 for infinite.")]
        [Range(0.0f, 1000.0f)]
        public float LifeTime = 0.0f;

        [Tooltip("Generations (1 - 8, higher makes more detailed but more expensive lightning)")]
        [Range(1, 8)]
        public int Generations = 6;

        [Tooltip("The chaos factor that determines how far the lightning main trunk can spread out, higher numbers spread out more. 0 - 1.")]
        [Range(0.0f, 1.0f)]
        public float ChaosFactor = 0.075f;

        [Tooltip("The chaos factor that determines how far the forks of the lightning can spread out, higher numbers spread out more. 0 - 1.")]
        [Range(0.0f, 1.0f)]
        public float ChaosFactorForks = 0.095f;

        [Tooltip("Intensity of the lightning")]
        [Range(0.0f, 10.0f)]
        public float Intensity = 1.0f;

        [Tooltip("The intensity of the glow")]
        [Range(0.0f, 10.0f)]
        public float GlowIntensity = 0.1f;

        [Tooltip("The width multiplier for the glow, 0 - 64")]
        [Range(0.0f, 64.0f)]
        public float GlowWidthMultiplier = 4.0f;

        [Tooltip("What percent of time the lightning should fade in and out. For example, 0.15 fades in 15% of the time and fades out 15% of the time, with full visibility 70% of the time.")]
        [Range(0.0f, 0.5f)]
        public float FadePercent = 0.15f;

        [Tooltip("Modify the duration of lightning fade in.")]
        [Range(0.0f, 1.0f)]
        public float FadeInMultiplier = 1.0f;

        [Tooltip("Modify the duration of fully lit lightning.")]
        [Range(0.0f, 1.0f)]
        public float FadeFullyLitMultiplier = 1.0f;

        [Tooltip("Modify the duration of lightning fade out.")]
        [Range(0.0f, 1.0f)]
        public float FadeOutMultiplier = 1.0f;

        [Tooltip("0 - 1, how slowly the lightning should grow. 0 for instant, 1 for slow.")]
        [Range(0.0f, 1.0f)]
        public float GrowthMultiplier;

        [Tooltip("How much smaller the lightning should get as it goes towards the end of the bolt. For example, 0.5 will make the end 50% the width of the start.")]
        [Range(0.0f, 10.0f)]
        public float EndWidthMultiplier = 0.5f;

        [Tooltip("How forked should the lightning be? (0 - 1, 0 for none, 1 for lots of forks)")]
        [Range(0.0f, 1.0f)]
        public float Forkedness = 0.25f;

        [Range(0.0f, 10.0f)]
        [Tooltip("Minimum distance multiplier for forks")]
        public float ForkLengthMultiplier = 0.6f;

        [Range(0.0f, 10.0f)]
        [Tooltip("Fork distance multiplier variance. Random range of 0 to n that is added to Fork Length Multiplier.")]
        public float ForkLengthVariance = 0.2f;

        [Tooltip("Forks have their EndWidthMultiplier multiplied by this value")]
        [Range(0.0f, 10.0f)]
        public float ForkEndWidthMultiplier = 1.0f;

        [Header("Lightning Light Properties")]
        [Tooltip("Light parameters")]
        public LightningLightParameters LightParameters;

        [Tooltip("Maximum number of lights that can be created per batch of lightning")]
        [Range(0, 64)]
        public int MaximumLightsPerBatch = 8;

        [Header("Lightning Trigger Type")]
        [Tooltip("Manual or automatic mode. Manual requires that you call the Trigger method in script. Automatic uses the interval to create lightning continuously.")]
        public bool ManualMode;

        [Tooltip("Turns lightning into automatic mode for this number of seconds, then puts it into manual mode.")]
        [Range(0.0f, 120.0f)]
        public float AutomaticModeSeconds;

        [Header("Lightning custom transform handler")]
        [Tooltip("Custom handler to modify the transform of each lightning bolt, useful if it will be alive longer than a few frames and needs to scale and rotate based " +
            "on the position of other objects.")]
        public LightningCustomTransformDelegate CustomTransformHandler;

        /// <summary>
        /// Override the random generator for the bolts
        /// </summary>
        public System.Random RandomOverride { get; set; }

        private float nextLightningTimestamp;
        private float lifeTimeRemaining;

        private void CalculateNextLightningTimestamp(float offset)
        {
            nextLightningTimestamp = (IntervalRange.Minimum == IntervalRange.Maximum ? IntervalRange.Minimum : offset + IntervalRange.Random());
        }

        private void CustomTransform(LightningCustomTransformStateInfo state)
        {
            if (CustomTransformHandler != null)
            {
                CustomTransformHandler.Invoke(state);
            }
        }

        private void CallLightning()
        {
            CallLightning(null, null);
        }

        private void CallLightning(Vector3? start, Vector3? end)
        {
            System.Random r = (RandomOverride ?? random);
            int count = CountRange.Random(r);
            for (int i = 0; i < count; i++)
            {
                LightningBoltParameters p = CreateParameters();
                if (CountProbabilityModifier >= 0.9999f || i == 0 || (float)p.Random.NextDouble() <= CountProbabilityModifier)
                {
                    p.CustomTransform = (CustomTransformHandler == null ? (System.Action<LightningCustomTransformStateInfo>)null : CustomTransform);
                    CreateLightningBolt(p);
                    if (start != null)
                    {
                        p.Start = start.Value;
                    }
                    if (end != null)
                    {
                        p.End = end.Value;
                    }
                }
                else
                {
                    LightningBoltParameters.ReturnParametersToCache(p);
                }
            }
            CreateLightningBoltsNow();
        }

        protected void CreateLightningBoltsNow()
        {
            int tmp = LightningBolt.MaximumLightsPerBatch;
            LightningBolt.MaximumLightsPerBatch = MaximumLightsPerBatch;
            CreateLightningBolts(batchParameters);
            LightningBolt.MaximumLightsPerBatch = tmp;
            batchParameters.Clear();
        }

        protected override void PopulateParameters(LightningBoltParameters p)
        {
            base.PopulateParameters(p);

            p.RandomOverride = RandomOverride;
            float duration = DurationRange.Random(p.Random);
            float trunkWidth = TrunkWidthRange.Random(p.Random);

            p.Generations = Generations;
            p.LifeTime = duration;
            p.ChaosFactor = ChaosFactor;
            p.ChaosFactorForks = ChaosFactorForks;
            p.TrunkWidth = trunkWidth;
            p.Intensity = Intensity;
            p.GlowIntensity = GlowIntensity;
            p.GlowWidthMultiplier = GlowWidthMultiplier;
            p.Forkedness = Forkedness;
            p.ForkLengthMultiplier = ForkLengthMultiplier;
            p.ForkLengthVariance = ForkLengthVariance;
            p.FadePercent = FadePercent;
            p.FadeInMultiplier = FadeInMultiplier;
            p.FadeOutMultiplier = FadeOutMultiplier;
            p.FadeFullyLitMultiplier = FadeFullyLitMultiplier;
            p.GrowthMultiplier = GrowthMultiplier;
            p.EndWidthMultiplier = EndWidthMultiplier;
            p.ForkEndWidthMultiplier = ForkEndWidthMultiplier;
            p.DelayRange = DelayRange;
            p.LightParameters = LightParameters;
        }

        protected override void Start()
        {
            base.Start();
            CalculateNextLightningTimestamp(0.0f);
            lifeTimeRemaining = (LifeTime <= 0.0f ? float.MaxValue : LifeTime);
        }

        protected override void Update()
        {
            base.Update();

            if ((lifeTimeRemaining -= LightningBoltScript.DeltaTime) < 0.0f)
            {
                GameObject.Destroy(gameObject);
            }
            if ((nextLightningTimestamp -= LightningBoltScript.DeltaTime) <= 0.0f)
            {
                CalculateNextLightningTimestamp(nextLightningTimestamp);
                if (ManualMode)
                {

#if DEBUG && SHOW_MANUAL_WARNING

                    if (!showedManualWarning)
                    {
                        showedManualWarning = true;
                        Debug.LogWarning("Lightning bolt script is in manual mode. Trigger method must be called.");
                    }

#endif

                }
                else
                {
                    CallLightning();
                }
            }

            if (AutomaticModeSeconds > 0.0f)
            {
                AutomaticModeSeconds = Mathf.Max(0.0f, AutomaticModeSeconds - LightningBoltScript.DeltaTime);
                ManualMode = (AutomaticModeSeconds == 0.0f);
            }
        }

#if UNITY_EDITOR

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            UnityEditor.Handles.color = Color.white;
        }

#endif

        /// <summary>
        /// Derived classes can override and can call this base class method last to add the lightning bolt parameters to the list of batched lightning bolts
        /// </summary>
        /// <param name="p">Lightning bolt creation parameters</param>
        public override void CreateLightningBolt(LightningBoltParameters p)
        {
            batchParameters.Add(p);
            // do not call the base method, we batch up and use CreateLightningBolts
        }

        /// <summary>
        /// Manually trigger the lightning once
        /// </summary>
        public void Trigger()
        {
            Trigger(-1.0f);
        }

        /// <summary>
        /// Manually trigger lightning
        /// </summary>
        /// <param name="seconds">Number of seconds to turn on automatic lightning for (sets AutomaticModeSeconds).</param>
        public void Trigger(float seconds)
        {
            CallLightning();
            if (seconds >= 0.0f)
            {
                AutomaticModeSeconds = Mathf.Max(0.0f, seconds);
            }
        }

        /// <summary>
        /// Manually trigger lightning
        /// </summary>
        /// <param name="start">Start position</param>
        /// <param name="end">End position</param>
        public void Trigger(Vector3? start, Vector3? end)
        {
            CallLightning(start, end);
        }
    }

    public class LightningBoltPrefabScript : LightningBoltPrefabScriptBase
    {
        [Header("Start/end")]
        [Tooltip("The source game object, can be null")]
        public GameObject Source;

        [Tooltip("The destination game object, can be null")]
        public GameObject Destination;

        [Tooltip("X, Y and Z for variance from the start point. Use positive values.")]
        public Vector3 StartVariance;

        [Tooltip("X, Y and Z for variance from the end point. Use positive values.")]
        public Vector3 EndVariance;

#if UNITY_EDITOR

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            if (Source != null)
            {
                Gizmos.DrawIcon(Source.transform.position, "LightningPathStart.png");
            }
            if (Destination != null)
            {
                Gizmos.DrawIcon(Destination.transform.position, "LightningPathNext.png");
            }
            if (Source != null && Destination != null)
            {
                Gizmos.DrawLine(Source.transform.position, Destination.transform.position);
                Vector3 direction = (Destination.transform.position - Source.transform.position);
                Vector3 center = (Source.transform.position + Destination.transform.position) * 0.5f;
                float arrowSize = Mathf.Min(2.0f, direction.magnitude) * 2.0f;

#if UNITY_5_6_OR_NEWER

                UnityEditor.Handles.ArrowHandleCap(0, center, Quaternion.LookRotation(direction), arrowSize, EventType.Repaint);

#else

                UnityEditor.Handles.ArrowCap(0, center, Quaternion.LookRotation(direction), arrowSize);

#endif

            }
        }

#endif

        public override void CreateLightningBolt(LightningBoltParameters parameters)
        {
            parameters.Start = (Source == null ? parameters.Start : Source.transform.position);
            parameters.End = (Destination == null ? parameters.End : Destination.transform.position);
            parameters.StartVariance = StartVariance;
            parameters.EndVariance = EndVariance;

            base.CreateLightningBolt(parameters);
        }
    }
}

