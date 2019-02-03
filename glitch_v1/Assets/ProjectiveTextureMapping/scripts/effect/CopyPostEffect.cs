using UnityEngine;
using UnityEngine.XR.iOS;

public class CopyPostEffect : MonoBehaviour
{
    
    [SerializeField] private RenderTexture _renderTex;
    [SerializeField] private Shader _shader;
    [SerializeField] private UnityARVideo _unityArVideo;    
    private Material _mat;
    void Start()
    {
        _mat = new Material(_shader);
        _renderTex.width = Mathf.FloorToInt( Screen.width * 0.5f );
        _renderTex.height = Mathf.FloorToInt(Screen.height * 0.5f );

    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);

		//_mat.SetMatrix("_DisplayTransform", _unityArVideo._displayTransform);        
        //Graphics.Blit(source, _renderTex,_mat);


    }

}