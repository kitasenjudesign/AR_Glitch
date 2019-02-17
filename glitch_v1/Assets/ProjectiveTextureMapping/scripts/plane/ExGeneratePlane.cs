using System;
using System.Collections.Generic;
using UnityEngine.XR.iOS;
using UnityEngine;
	public class ExGeneratePlane : MonoBehaviour
	{
		public ExARKitPlaneMeshRender planePrefab;
        //private UnityARAnchorManager unityARAnchorManager;
        private ExAnchorManager unityARAnchorManager;


		// Use this for initialization
		void Start () {
            unityARAnchorManager = new ExAnchorManager();
            unityARAnchorManager.Init();
            
			ExUnityARUtility.InitializePlanePrefab (planePrefab);
		}

        void OnDestroy()
        {
            unityARAnchorManager.Destroy ();
        }

        void OnGUI()
        {
			IEnumerable<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors ();
			//foreach(var planeAnchor in arpags)
			//{
            //    ARPlaneAnchor ap = planeAnchor;
            //    GUI.Box (new Rect (100, 100, 800, 60), string.Format ("Center: x:{0}, y:{1}, z:{2}", ap.center.x, ap.center.y, ap.center.z));
            //    GUI.Box(new Rect(100, 200, 800, 60), string.Format ("Extent: x:{0}, y:{1}, z:{2}", ap.extent.x, ap.extent.y, ap.extent.z));
            //}
        }
	}
