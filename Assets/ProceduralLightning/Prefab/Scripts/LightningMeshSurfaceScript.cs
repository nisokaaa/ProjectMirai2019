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
    public class LightningMeshSurfaceScript : LightningBoltPrefabScriptBase
    {
        [Header("Lightning Mesh Properties")]
        [Tooltip("The mesh filter. You must assign a mesh filter in order to create lightning on the mesh.")]
        public MeshFilter MeshFilter;

        [Tooltip("The mesh collider. This is used to get random points on the mesh.")]
        public Collider MeshCollider;

        [SingleLine("Random range that the point will offset from the mesh, using the normal of the chosen point to offset")]
        public RangeOfFloats MeshOffsetRange = new RangeOfFloats { Minimum = 0.5f, Maximum = 1.0f };

        [Header("Lightning Path Properties")]
        [SingleLine("Range for points in the lightning path")]
        public RangeOfIntegers PathLengthCount = new RangeOfIntegers { Minimum = 3, Maximum = 6 };

        [SingleLine("Range for minimum distance between points in the lightning path")]
        public RangeOfFloats MinimumPathDistanceRange = new RangeOfFloats { Minimum = 0.5f, Maximum = 1.0f };

        [Tooltip("The maximum distance between mesh points. When walking the mesh, if a point is greater than this, the path direction is reversed. " +
            "This tries to avoid paths crossing between mesh points that are not actually physically touching.")]
        public float MaximumPathDistance = 2.0f;
        private float maximumPathDistanceSquared;

        [Tooltip("Whether to use spline interpolation between the path points. Paths must be at least 4 points long to be splined.")]
        public bool Spline = false;

        [Tooltip("For spline. the distance hint for each spline segment. Set to <= 0 to use the generations to determine how many spline segments to use. " +
            "If > 0, it will be divided by Generations before being applied. This value is a guideline and is approximate, and not uniform on the spline.")]
        public float DistancePerSegmentHint = 0.0f;

        private readonly List<Vector3> sourcePoints = new List<Vector3>();
        private Mesh previousMesh;
        private MeshHelper meshHelper;

        private void CheckMesh()
        {
            if (MeshFilter == null || MeshFilter.sharedMesh == null)
            {
                meshHelper = null;
            }
            else if (MeshFilter.sharedMesh != previousMesh)
            {
                previousMesh = MeshFilter.sharedMesh;
                meshHelper = new MeshHelper(previousMesh);

#if DEBUG

                if (previousMesh.GetTopology(0) != MeshTopology.Triangles)
                {
                    Debug.LogError("Mesh topology must be triangles");
                }

#endif

            }
        }

        /// <summary>
        /// Create lightning bolt path parameters
        /// </summary>
        /// <returns>Lightning bolt path parameters</returns>
        protected override LightningBoltParameters OnCreateParameters()
        {
            LightningBoltParameters p = base.OnCreateParameters();
            p.Generator = LightningGeneratorPath.PathGeneratorInstance;
            return p;
        }

        /// <summary>
        /// Populate the points for a lightning path. This implementation simply picks a random point and then spreads out in random directions along the mesh.
        /// </summary>
        /// <param name="points">Points for the path to be filled in. Does not need to be cleared.</param>
        protected virtual void PopulateSourcePoints(List<Vector3> points)
        {
            if (meshHelper != null)
            {
                CreateRandomLightningPath(sourcePoints);
            }
        }

        /// <summary>
        /// Gets a path for lightning starting at a random point on the mesh
        /// </summary>
        /// <param name="points">Points list to receive points for the path</param>
        public void CreateRandomLightningPath(List<Vector3> points)
        {
            if (meshHelper == null)
            {
                return;
            }

            // we want a path of at least 2 triangles
            RaycastHit hit = new RaycastHit();
            int triangleIndex;
            maximumPathDistanceSquared = MaximumPathDistance * MaximumPathDistance;
            meshHelper.GenerateRandomPoint(ref hit, out triangleIndex);
            hit.distance = UnityEngine.Random.Range(MeshOffsetRange.Minimum, MeshOffsetRange.Maximum);
            Vector3 prevPoint = hit.point + (hit.normal * hit.distance);
            float pathDistanceSquared = UnityEngine.Random.Range(MinimumPathDistanceRange.Minimum, MinimumPathDistanceRange.Maximum);
            pathDistanceSquared *= pathDistanceSquared;
            sourcePoints.Add(MeshFilter.transform.TransformPoint(prevPoint));
            int dir = (UnityEngine.Random.Range(0, 1) == 1 ? 3 : -3);
            int pathLength = UnityEngine.Random.Range(PathLengthCount.Minimum, PathLengthCount.Maximum);
            while (pathLength != 0)
            {
                triangleIndex += dir;
                if (triangleIndex >= 0 && triangleIndex < meshHelper.Triangles.Length)
                {
                    meshHelper.GetRaycastFromTriangleIndex(triangleIndex, ref hit);
                }
                else
                {
                    dir = -dir;
                    triangleIndex += dir;
                    pathLength--;
                    continue;
                }
                hit.distance = UnityEngine.Random.Range(MeshOffsetRange.Minimum, MeshOffsetRange.Maximum);
                Vector3 hitPoint = hit.point + (hit.normal * hit.distance);
                float distanceSquared = (hitPoint - prevPoint).sqrMagnitude;
                if (distanceSquared > maximumPathDistanceSquared)
                {
                    break;
                }
                else if (distanceSquared >= pathDistanceSquared)
                {
                    prevPoint = hitPoint;
                    sourcePoints.Add(MeshFilter.transform.TransformPoint(hitPoint));
                    pathLength--;
                    pathDistanceSquared = UnityEngine.Random.Range(MinimumPathDistanceRange.Minimum, MinimumPathDistanceRange.Maximum);
                    pathDistanceSquared *= pathDistanceSquared;
                }
            }
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            CheckMesh();

            base.Update();
        }

        public override void CreateLightningBolt(LightningBoltParameters parameters)
        {
            if (meshHelper == null)
            {
                return;
            }

            Generations = parameters.Generations = Mathf.Clamp(Generations, 1, LightningSplineScript.MaxSplineGenerations);
            sourcePoints.Clear();
            PopulateSourcePoints(sourcePoints);
            if (sourcePoints.Count > 1)
            {
                parameters.Points.Clear();
                if (Spline && sourcePoints.Count > 3)
                {
                    LightningSplineScript.PopulateSpline(parameters.Points, sourcePoints, Generations, DistancePerSegmentHint, Camera);
                    parameters.SmoothingFactor = (parameters.Points.Count - 1) / sourcePoints.Count;
                }
                else
                {
                    parameters.Points.AddRange(sourcePoints);
                    parameters.SmoothingFactor = 1;
                }
                base.CreateLightningBolt(parameters);
            }
        }
    }
}