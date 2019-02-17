using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionObjs : MonoBehaviour {

	//[SerializeField] private ProjObj _src;
	[SerializeField] private ProjDrawMeshes _srcA;
	[SerializeField] private ProjDrawMeshes _srcB;

	[SerializeField] private Camera _projectionCam;
	[SerializeField] private Camera _camera;
	[SerializeField] private Camera _camera2;
	[SerializeField] private RenderTexture _camTex;
	//[SerializeField] private RenderTexture _camTex2;
	private RenderTexture _captureTestTex;

	private ProjDrawMeshes _current;
	private int _count = 0;

	void Start () {
		
		_srcA.gameObject.SetActive(false);
		_srcB.gameObject.SetActive(false);
		_captureTestTex = new RenderTexture(Screen.width,Screen.height,0);
	}
	
	private void OnGUI()
	{
		GUI.DrawTexture(
            new Rect(0, 0, Mathf.FloorToInt(Screen.width*0.2f),Mathf.FloorToInt(Screen.height*0.2f)), 
            _captureTestTex, 
            ScaleMode.StretchToFill,
            false
        ); 
		GUI.DrawTexture(
            new Rect(0, Mathf.FloorToInt(Screen.height*0.2f), Mathf.FloorToInt(Screen.width*0.2f),Mathf.FloorToInt(Screen.height*0.2f)), 
            _camTex, 
            ScaleMode.StretchToFill,
            false
        ); 

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

		_count++;

		if(_count % 2 == 0){
			_current = _srcA;
		}else{
			_current = _srcB;
		}
		
		
		_current.gameObject.SetActive(false);//自分は消す
		_current.Capture(_camTex);//相手と背景をキャプチャ

		Graphics.Blit(_camTex,_captureTestTex);//debugよう

		Invoke("_SetPos",0.05f);
	}

	void _SetPos(){

		var projMat = _projectionCam.projectionMatrix;
		var viewMat = _projectionCam.worldToCameraMatrix;
		
		_current.transform.localScale = new Vector3(0.15f,0.15f,0.15f);
		_current.transform.position = _camera.transform.position + _camera.transform.forward*1f;
		_current.transform.LookAt(_projectionCam.transform.position);

		_srcA.gameObject.SetActive(true);
		_srcB.gameObject.SetActive(true);

		if(_count % 2 == 0){
			
			_current.Init(projMat,viewMat,0.5f);

		}else{

			_current.Init(projMat,viewMat,0.1f);

		}

	}

}
