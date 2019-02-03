using UnityEngine;
using UnityEngine.XR.iOS;

public class CopyCamera : MonoBehaviour
{

    [SerializeField] private Camera _refCam;
    [SerializeField] private RenderTexture _tex;

    private Camera _myCam;
    private float _width;

    void Start(){
        
        _myCam = GetComponent<Camera>();
        _tex.width = Mathf.FloorToInt( Screen.width*0.5f );
        _tex.height = Mathf.FloorToInt( Screen.height*0.5f );
        _width = _tex.width;
    }

    void Update(){

        if(_width!=Mathf.FloorToInt( Screen.width*0.5f )){
            _tex.width = Mathf.FloorToInt( Screen.width*0.5f );
            _tex.height = Mathf.FloorToInt( Screen.height*0.5f );
        }

        _myCam.projectionMatrix = _refCam.projectionMatrix;
    }

}