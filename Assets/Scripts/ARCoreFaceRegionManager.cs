using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARCore;

[RequireComponent(typeof(ARFaceManager))]
[RequireComponent(typeof(ARSessionOrigin))]
public class ARCoreFaceRegionManager : MonoBehaviour
{
    private Quaternion[] poseRotations;

    ARFaceManager m_FaceManager;

    NativeArray<ARCoreFaceRegionData> m_FaceRegions;

    Dictionary<TrackableId, Dictionary<ARCoreFaceRegion, GameObject>> m_InstantiatedPrefabs;

    void Start()
    {
        m_FaceManager = GetComponent<ARFaceManager>();
        m_InstantiatedPrefabs = new Dictionary<TrackableId, Dictionary<ARCoreFaceRegion, GameObject>>();
    }

    void Update()
    {
        var subsystem = (ARCoreFaceSubsystem)m_FaceManager.subsystem;
        if (subsystem == null)
            return;

        foreach (var face in m_FaceManager.trackables)
        {
            Dictionary<ARCoreFaceRegion, GameObject> regionGos;
            if (!m_InstantiatedPrefabs.TryGetValue(face.trackableId, out regionGos))
            {
                regionGos = new Dictionary<ARCoreFaceRegion, GameObject>();
                m_InstantiatedPrefabs.Add(face.trackableId, regionGos);
            }

            subsystem.GetRegionPoses(face.trackableId, Allocator.Persistent, ref m_FaceRegions);
            if (poseRotations == null)
            {
                poseRotations = new Quaternion[m_FaceRegions.Length];
                for (int i = 0; i < m_FaceRegions.Length; ++i)
                {
                    poseRotations[i] = m_FaceRegions[i].pose.rotation;
                }
            }
            for (int i = 0; i < m_FaceRegions.Length; ++i)
            {
                FollowFace.isSmilling = CheckIfRotatedOnX(poseRotations, m_FaceRegions.ToArray());
            }
        }
    }

    private bool CheckIfRotatedOnX(Quaternion[] a, ARCoreFaceRegionData[] b)
	{
        return true;

        int counter = 0;
		for (int i = 0; i < a.Length; i++)
		{
            Debug.Log("Angle: " + Quaternion.Angle(b[i].pose.rotation, a[i]));
            if(Quaternion.Angle(b[i].pose.rotation, a[i]) > 4f)
			{
                counter++;
			}
		}
        return counter/a.Length > 0.5f;
	}
   
    void OnDestroy()
    {
        if (m_FaceRegions.IsCreated)
            m_FaceRegions.Dispose();
    }
}