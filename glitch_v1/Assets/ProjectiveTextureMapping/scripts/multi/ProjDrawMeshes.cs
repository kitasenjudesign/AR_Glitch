using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjDrawMeshes : DrawMeshInstancedBase {

    private ProjDrawData[] _data;
    public const int MAX = 1023;

    //[SerializeField] private Material _material;

    private Matrix4x4[] _modelMats;
    private Matrix4x4[] _viewMats;
    private Matrix4x4[] _projMats;

    private RenderTexture _renderTexture;

    void Awake(){
        gameObject.SetActive(false);

        _propertyBlock = new MaterialPropertyBlock();        
        _modelMats = new Matrix4x4[MAX];
        _viewMats = new Matrix4x4[MAX];
        _projMats = new Matrix4x4[MAX];
        _data = new ProjDrawData[MAX];
        _renderTexture = new RenderTexture(Screen.width,Screen.height,0);
    }

    public void Init(Matrix4x4 projMat, Matrix4x4 viewMat, RenderTexture srcTex, float baseScale){

        //_count = 300;
        for(int i=0;i<_count;i++){

            _modelMats[i] = Matrix4x4.identity;
            _viewMats[i] = viewMat;
            _projMats[i] = projMat;

            _data[i] = new ProjDrawData();

            _data[i].pos.x = 4.5f * (Random.value - 0.5f);
            _data[i].pos.y = 4.5f * (Random.value - 0.5f);
            _data[i].pos.z = 4.5f * (Random.value - 0.5f);

            var ss = 0.04f + baseScale*Random.value;
            _data[i].scale = new Vector3(
                ss,ss,ss
            );

            _data[i].rot = Quaternion.Euler(
                0,
                0,
                0
            );
            
        }

		gameObject.SetActive(true);

		//_mat = _renderer.localToWorldMatrix;
		
		Graphics.Blit( srcTex,_renderTexture);

		_mat.SetTexture("_MainTex", _renderTexture);

		_propertyBlock.SetMatrixArray("_ModelMat", _modelMats );
		_propertyBlock.SetMatrixArray("_ProjMat", 	_projMats);//_projectionCam.projectionMatrix );
		_propertyBlock.SetMatrixArray("_ViewMat", 	_viewMats);
        
    }








    void Update(){

        if(_modelMats==null)return;

        //Debug.Log(_modelMats.Length);

        for (int i = 0; i < _count; i++)
        {
            
            _modelMats[i].SetTRS( 
                _data[i].pos,
                _data[i].rot,
                _data[i].scale
            );
            _modelMats[i] = transform.localToWorldMatrix * _modelMats[i];
            
        }

        _propertyBlock.SetMatrixArray("_ModelMat", _modelMats);

        Graphics.DrawMeshInstanced(
                _mesh, 
                0, 
                _mat, 
                _modelMats, 
                _count, 
                _propertyBlock, 
                ShadowCastingMode.Off, 
                false, 
                gameObject.layer
        );

    }

}