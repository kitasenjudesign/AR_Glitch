using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class YCbCrToRGB : MonoBehaviour {

    [SerializeField] private RenderTexture _rgbTexture;
    [SerializeField] private UnityARVideo _unityArVideo;
    [SerializeField] private Shader _yCbCrShader;
    private Material _mat;
    public static RenderTexture staticTex;

    //[SerializeField] private Material _faceMat;

    void Start(){
        
        _mat = new Material(_yCbCrShader);
        Init(_rgbTexture);

    }

    public static void Init(RenderTexture tex){
        
        staticTex = tex;
        
    }

    void Update(){

        //
		_mat.SetMatrix("_DisplayTransform", _unityArVideo._displayTransform);
        _mat.SetTexture("_textureY",    _unityArVideo._videoTextureY);
        _mat.SetTexture("_textureCbCr", _unityArVideo._videoTextureCbCr);

        Graphics.Blit(null,_rgbTexture,_mat);

        //_faceMat.SetMatrix("_DisplayTransform", _unityArVideo._displayTransform);
        //_faceMat.SetTexture("_MainTex",_rgbTexture );
    }

}