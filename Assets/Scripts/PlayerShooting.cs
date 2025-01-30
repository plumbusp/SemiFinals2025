using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform _firePoint;
    [SerializeField] float _fireForce;
    Camera _maincamera;

    private Bullet currentbullet;
    private Vector3 mousePosition;

    private void Awake()
    {
        _maincamera = Camera.main;
    }

    private void Update()
    {
        //Head Movement
        mousePosition = _maincamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rotation = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = rotation;
        // Head Movement

        //float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        currentbullet = BulletPooler.Instance.GetPoolObject("PlayerBullets");
        var direction = mousePosition - transform.position;
        var rotation = transform.position - mousePosition;
        currentbullet.Shoot(direction, rotation, _fireForce, _firePoint);
    }
}
