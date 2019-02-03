using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionObjs : MonoBehaviour {

	[SerializeField] private ProjObj _src;
	[SerializeField] private ProjDrawMeshes _src2;

	[SerializeField] private Camera _projectionCam;
	[SerializeField] private Camera _camera;
	[SerializeField] private RenderTexture _camTex;
	//[SerializeField] private RenderTexture _camTex2;

	private ProjDrawMeshes _current;
	private int _count = 0;

	void Start () {
		
	}
	


	// Update is called once per frame
	void Update () {
		

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)// || touch.phase==TouchPhase.Stationary)
            {
				
                float xx = touch.position.x / Screen.width - 0.5f;
                float yy = touch.position.y / Screen.height - 0.5f;

				_Cap(xx,yy);
			}
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			_Cap(
				Random.value - 0.5f,
				Random.value - 0.5f
			);
		}

		//_material.SetMatrix("_tMat", 		_tMat );
		//_material.SetMatrix("_projMat", 	_projMat);//_projectionCam.projectionMatrix );
		//_material.SetMatrix("_viewMat", 	_viewMat);
		//_material.SetTexture("_MainTex", _renderTexture );

	}

	//多すぎたら消すとか

	void _Cap(float ox, float oy, int num=1){


		var projMat = _projectionCam.projectionMatrix;
		var viewMat = _projectionCam.worldToCameraMatrix;

		if(_current==null){
			_current = Instantiate(_src2,transform,false);
		}

		_current.gameObject.SetActive(true);

		float r = 0.05f * Random.value;
		float s = 0.1f*Random.value;
		_current.transform.localScale = new Vector3(0.1f+s,0.1f+s,0.1f+s);


		_current.transform.position = _camera.transform.position + _camera.transform.forward*1f;
		//gen.transform.LookAt( _camera.transform );
		_current.transform.LookAt(_projectionCam.transform.position);
		
		_count++;
		if(_count % 2 == 0){
			_current.Init(projMat,viewMat,_camTex,0.5f);
		}else{
			_current.Init(projMat,viewMat,_camTex,0.1f);
		}

		/* 
		for(int i=0;i<num;i++){

			var projMat = _projectionCam.projectionMatrix;
			var viewMat = _projectionCam.worldToCameraMatrix;

			var gen = Instantiate(_src,transform,false);
			gen.gameObject.SetActive(true);

			float r = 0.05f * Random.value;
			float s = 0.1f*Random.value;
			gen.transform.localScale = new Vector3(0.1f+s,0.1f+s,0.1f+s);

			var offset = new Vector3(
				0,
				0,
				0
			);

			gen.transform.position = _camera.transform.position + _camera.transform.forward*0.7f + offset;
			//gen.transform.LookAt( _camera.transform );
			gen.transform.localRotation = Quaternion.Euler(
				360f*Random.value,
				360f*Random.value,
				360f*Random.value
			);
			gen.Init(projMat,viewMat,_camTex);

		}*/

	}
	


}
