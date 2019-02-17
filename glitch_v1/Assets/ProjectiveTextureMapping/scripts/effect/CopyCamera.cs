using UnityEngine;
using UnityEngine.XR.iOS;

public class CopyCamera : MonoBehaviour
{

    [SerializeField] private Camera _refCam;
    [SerializeField] private RenderTexture _tex;

    private Camera _myCam;
    private float _width;

    private float _size = 1f;

    void Start(){
        
        _myCam = GetComponent<Camera>();
        _tex.width = Mathf.FloorToInt( Screen.width*_size );
        _tex.height = Mathf.FloorToInt( Screen.height*_size );
        _width = _tex.width;
    }


    	void OnPreRender(){
            Shader.SetGlobalFloat("_GlobalInvert",1f);
		}

		void OnPostRender(){
            Shader.SetGlobalFloat("_GlobalInvert",0);
        }

        void Update(){

            if(_width!=Mathf.FloorToInt( Screen.width*_size )){
                _tex.width = Mathf.FloorToInt( Screen.width*_size );
                _tex.height = Mathf.FloorToInt( Screen.height*_size );
            }

            _myCam.projectionMatrix = _refCam.projectionMatrix;
        }

}