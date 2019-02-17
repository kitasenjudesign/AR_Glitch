using UnityEngine;
using System.Collections;

public class CenterOfGravity
{

    public Vector3 GetCenter(Vector3[] verts){
        
        var center = new Vector3();
        
        for(int i=0; i<verts.Length; i++){
            center += verts[i];
        }
        center = center / (float)verts.Length;

        return center;
    }

    /*
    public Vector2[] GetDistance( Vector3[] verts ){

        Vector2[] uv = new Vector2[verts.Length];
        var center = GetCenter( verts );
        for(int i=0;i<uv.Length;i++){

        }
        return uv;        

    }*/


}