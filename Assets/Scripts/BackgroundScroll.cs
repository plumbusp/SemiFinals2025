using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class BackgroundScroll : MonoBehaviour
{
    [SerializeField, Range(-1,1)] private float _speedX;
    [SerializeField, Range(-1,1)] private float _speedY;
    private RawImage _rawImage;
    private Rect _initialUVPosition;

    private void OnDisable()
    {
        _rawImage.uvRect = _initialUVPosition; //Setting the UV Rectangle of the raw image to its initial offset
    }
    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
        _initialUVPosition =_rawImage.uvRect;
    }
    private void Update()
    {
       _rawImage.uvRect = new Rect(_rawImage.uvRect.position + new Vector2(_speedX * Time.deltaTime, _speedY * Time.deltaTime), _rawImage.uvRect.size);  // Changing the UV rectangle offset every frame to create a scrolling effect
    }
}
