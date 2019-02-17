using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.iOS;
//using DG.Tweening;

public class ProjObj : MonoBehaviour {

	//[SerializeField] private UnityARVideo _arVideo;
	[SerializeField] private RenderTexture _srcTex;
	private MeshRenderer _renderer;
	private MaterialPropertyBlock _propertyBlock;

	private Vector3 _rotSpeed;
	private Vector3 _rot;
	private Matrix4x4 _modelMat;
	private Matrix4x4 _tMat;
	private Matrix4x4 _projMat;
	private Matrix4x4 _viewMat;
	private RenderTexture _renderTexture;
	
	private float _scaleY = 0f;
	// Use this for initialization
	void Awake () {

		_tMat = new Matrix4x4(
			new Vector4(0.5f,0,0,0),//m00,m10,m20,m30
			new Vector4(0,0.5f,0,0),//m01,m11,m21,m31
			new Vector4(0,0,1f,0),//m02,m12,m22,m32
			new Vector4(0.5f,0.5f,0,1f)//m03,m13,m23,m33
		);

		_rotSpeed = new Vector3(
			1.5f*(Random.value-0.5f),
			1.5f*(Random.value-0.5f),
			1.5f*(Random.value-0.5f)
		);

		_rot = transform.rotation.eulerAngles;

		_propertyBlock = new MaterialPropertyBlock();
		_renderer =GetComponent<MeshRenderer>();
		
		_renderTexture = new RenderTexture(
			Mathf.FloorToInt(Screen.width*0.5f),
			Mathf.FloorToInt(Screen.height*0.5f),0
		);

		gameObject.SetActive(false);

	}
	
	public void Capture(){
		//Debug.Log("Capture");
		var projMat = Camera.main.projectionMatrix;
		var viewMat = Camera.main.worldToCameraMatrix;
		Init(projMat,viewMat,_srcTex);
	}


	public void Init(Matrix4x4 projMat, Matrix4x4 viewMat, RenderTexture srcTex){
		
		//Debug.Log("SetMat");
		_projMat = projMat;
		_viewMat = viewMat;

		gameObject.SetActive(true);

		//_mat = _renderer.localToWorldMatrix;
		_modelMat = transform.localToWorldMatrix;
		if(_propertyBlock!=null){

			
			Graphics.Blit( srcTex,_renderTexture);

			_propertyBlock.SetMatrix("_modelMat", _modelMat );
			_propertyBlock.SetMatrix("_tMat", 		_tMat );
			_propertyBlock.SetMatrix("_projMat", 	_projMat);//_projectionCam.projectionMatrix );
			_propertyBlock.SetMatrix("_viewMat", 	_viewMat);
			_propertyBlock.SetTexture("_MainTex", _renderTexture );			
			//_propertyBlock.SetFloat("_ScaleY", 		0 );
		}

		if(_renderer!=null) _renderer.SetPropertyBlock(_propertyBlock);
		
		

		//_rot.y += 360f;
		//transform.DORotate(_rot,1f);
		//transform.dolo

	}

	// Update is called once per frame
	void Update () {

		if(_renderer!=null) _renderer.SetPropertyBlock(_propertyBlock);
		//transform.Rotate(_rotSpeed);

	}
}
