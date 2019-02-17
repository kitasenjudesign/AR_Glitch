using System;
using System.Collections.Generic;
using System.Linq;
using Collections.Hybrid.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ExAnchorManager 
{


    private LinkedListDictionary<string, ARPlaneAnchorGameObject> planeAnchorMap;


    public ExAnchorManager ()
    {
        planeAnchorMap = new LinkedListDictionary<string,ARPlaneAnchorGameObject> ();
    }

    public void Init(){
        //イベントを追加している
        UnityARSessionNativeInterface.ARAnchorAddedEvent += AddAnchor;
        UnityARSessionNativeInterface.ARAnchorUpdatedEvent += UpdateAnchor;
        UnityARSessionNativeInterface.ARAnchorRemovedEvent += RemoveAnchor;
    }


    public void AddAnchor(ARPlaneAnchor arPlaneAnchor)
    {

        Debug.LogWarning("AddAnchor!!!!");

        //Planeが追加されたら呼ばれる。
        ExARKitPlaneMeshRender go = ExUnityARUtility.CreatePlaneInScene (arPlaneAnchor);////////////生成

        go.gameObject.AddComponent<DontDestroyOnLoad> ();  //this is so these GOs persist across scene loads
        ARPlaneAnchorGameObject arpag = new ARPlaneAnchorGameObject ();
        arpag.planeAnchor = arPlaneAnchor;
        arpag.gameObject = go.gameObject;
        planeAnchorMap.Add (arPlaneAnchor.identifier, arpag);////////////ディクショナリーにAdd
    }

    public void RemoveAnchor(ARPlaneAnchor arPlaneAnchor)
    {
        if (planeAnchorMap.ContainsKey (arPlaneAnchor.identifier)) {
            ARPlaneAnchorGameObject arpag = planeAnchorMap [arPlaneAnchor.identifier];
            GameObject.Destroy (arpag.gameObject);
            planeAnchorMap.Remove (arPlaneAnchor.identifier);
        }
    }

    public void UpdateAnchor(ARPlaneAnchor arPlaneAnchor)
    {
            Debug.LogWarning("updateAnchor!!!!");
        //ancharの更新
        if (planeAnchorMap.ContainsKey (arPlaneAnchor.identifier)) {
            ARPlaneAnchorGameObject arpag = planeAnchorMap [arPlaneAnchor.identifier];
            //planeをアップデート
            ExUnityARUtility.UpdatePlaneWithAnchorTransform (
                arpag.gameObject.transform.GetComponent<ExARKitPlaneMeshRender>(), 
                arPlaneAnchor
            );
            arpag.planeAnchor = arPlaneAnchor;
            planeAnchorMap [arPlaneAnchor.identifier] = arpag;
        }
    }

    public void UnsubscribeEvents()
    {
        UnityARSessionNativeInterface.ARAnchorAddedEvent -= AddAnchor;
        UnityARSessionNativeInterface.ARAnchorUpdatedEvent -= UpdateAnchor;
        UnityARSessionNativeInterface.ARAnchorRemovedEvent -= RemoveAnchor;
    }

    public void Destroy()
    {
        foreach (ARPlaneAnchorGameObject arpag in GetCurrentPlaneAnchors()) {
            GameObject.Destroy (arpag.gameObject);
        }

        planeAnchorMap.Clear ();
        UnsubscribeEvents();
    }

    public LinkedList<ARPlaneAnchorGameObject> GetCurrentPlaneAnchors()
    {
        return planeAnchorMap.Values;
    }
}


