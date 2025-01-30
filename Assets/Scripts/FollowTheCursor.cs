using UnityEngine;

public class FollowTheCursor : MonoBehaviour
{
    [SerializeField] private Transform follower;
    [SerializeField] private float moveSpeed;
    private Camera mainCamera;
    private Vector2 _mousePosition;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;
        _mousePosition = mainCamera.ScreenToWorldPoint( _mousePosition );
        follower.position = Vector3.Lerp(follower.position, _mousePosition, Time.deltaTime * moveSpeed);
    }
}
