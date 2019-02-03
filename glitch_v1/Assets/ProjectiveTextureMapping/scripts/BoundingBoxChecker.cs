using UnityEngine;
using System.Collections;

public class BoundingBoxChecker : MonoBehaviour
{

    [SerializeField] private Vector3 size;
    [SerializeField] private Vector3 center;

    void OnDrawGizmosSelected()
    {

        if (GetComponent<MeshFilter>()==null) return;

        var mesh = GetComponent<MeshFilter>().mesh;
        mesh.RecalculateBounds();

        var bounds = mesh.bounds;

        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(bounds.center, bounds.size);

        size = bounds.size;
        center = bounds.center;

    }
}