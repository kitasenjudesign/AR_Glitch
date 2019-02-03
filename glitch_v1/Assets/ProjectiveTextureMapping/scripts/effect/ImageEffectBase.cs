using UnityEngine;


public class ImageEffectBase : MonoBehaviour
{
    
    [SerializeField] private Shader _shader;
    [SerializeField] private Material _material;

    void Awake()
    {
        _material = new Material(_shader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _material);
    }

}