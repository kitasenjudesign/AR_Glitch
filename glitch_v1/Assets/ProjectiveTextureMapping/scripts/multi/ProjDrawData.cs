using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjDrawData {

    public Vector3 pos = new Vector3(
        5f * ( Random.value-0.5f ),
        5f * ( Random.value-0.5f ),
        5f * ( Random.value-0.5f )       
    );
    public Quaternion rot =Quaternion.Euler(0,0,0);
    public Vector3 scale = new Vector3(0.1f,0.1f,0.1f);
    public Vector4 uv = new Vector4();

    public Matrix4x4 modelMat;
    public Matrix4x4 projMat;
    public Matrix4x4 viewMat;

    public void Init(){
        
        //modelMat = Matrix4x4.identity;


    }

    /*
		_propertyBlock.SetTexture("_MainTex", _renderTexture);
		_propertyBlock.SetMatrix("_tMat", 		_tMat );
		_propertyBlock.SetMatrix("_projMat", 	_projMat);//_projectionCam.projectionMatrix );

        _propertyBlock.SetMatrix("_modelMat", _modelMat );
		
		_propertyBlock.SetMatrix("_viewMat", 	_viewMat);
		_propertyBlock.SetTexture("_MainTex", _renderTexture );		
    */


    /* 
    public Matrix4x4 UpdateModelMatrix(Transform t){

        modelMat = Matrix4x4.identity;
        modelMat.SetTRS(pos,rot,scale);
        modelMat = t.localToWorldMatrix * modelMat;

    }*/

}