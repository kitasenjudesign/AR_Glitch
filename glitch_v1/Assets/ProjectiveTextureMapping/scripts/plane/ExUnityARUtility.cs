using System;
using System.Runtime.InteropServices;
using UnityEngine.XR.iOS;
using UnityEngine;

public class ExUnityARUtility
{
    private MeshCollider meshCollider; //declared to avoid code stripping of class
    private MeshFilter meshFilter; //declared to avoid code stripping of class
    private static ExARKitPlaneMeshRender planePrefab = null;

    public static void InitializePlanePrefab(ExARKitPlaneMeshRender go)
    {
        planePrefab = go;
    }
    
    public static ExARKitPlaneMeshRender CreatePlaneInScene(ARPlaneAnchor arPlaneAnchor)
    {
        Debug.Log("Gen");
        ExARKitPlaneMeshRender plane = GameObject.Instantiate(planePrefab);
        plane.gameObject.SetActive(true);
        plane.transform.name = arPlaneAnchor.identifier;

        //ARKitPlaneMeshRender apmr = plane.GetComponent<ARKitPlaneMeshRender> ();
        //if (apmr != null) {
            plane.InitiliazeMesh (arPlaneAnchor);
        //}

        return UpdatePlaneWithAnchorTransform(plane, arPlaneAnchor);

    }

    public static ExARKitPlaneMeshRender UpdatePlaneWithAnchorTransform(ExARKitPlaneMeshRender plane, ARPlaneAnchor arPlaneAnchor)
    {
        
        //do coordinate conversion from ARKit to Unity
        plane.transform.position = UnityARMatrixOps.GetPosition (arPlaneAnchor.transform);
        plane.transform.rotation = UnityARMatrixOps.GetRotation (arPlaneAnchor.transform);

        plane.UpdateMesh (arPlaneAnchor);
        
        MeshFilter mf = plane.GetComponentInChildren<MeshFilter> ();

        if (mf != null) {
            //if (apmr == null) {
                //since our plane mesh is actually 10mx10m in the world, we scale it here by 0.1f
                mf.gameObject.transform.localScale = new Vector3 (arPlaneAnchor.extent.x * 0.1f, arPlaneAnchor.extent.y * 0.1f, arPlaneAnchor.extent.z * 0.1f);

                //convert our center position to unity coords
                mf.gameObject.transform.localPosition = new Vector3(arPlaneAnchor.center.x,arPlaneAnchor.center.y, -arPlaneAnchor.center.z);
            //}
        }

        return plane;
    }



}


