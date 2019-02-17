using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Torec;

public class SubdivisionTest : MonoBehaviour {

	public CatmullClark.Options.BoundaryInterpolation m_boundaryInterpolation;
	[SerializeField] private MeshFilter _meshFilter;
	[SerializeField] private MeshFilter _meshFilter2;

	// Use this for initialization
	void Start () {
			int iter = 1;

			var aa = _meshFilter.sharedMesh;

		    Mesh newMesh = CatmullClark.Subdivide(aa, iter, new CatmullClark.Options {
                boundaryInterpolation = m_boundaryInterpolation,
            });

			_meshFilter2.sharedMesh = newMesh;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
