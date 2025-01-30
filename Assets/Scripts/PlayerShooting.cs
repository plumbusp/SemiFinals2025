using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Transform _firePoint;
    [SerializeField] float _fireForce;
    Rigidbody2D _rb2D;

    private Bullet currentbullet;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        currentbullet = BulletPooler.Instance.GetPoolObject("PlayerBullets");
        currentbullet.Shoot(_firePoint, new Vector2(_firePoint.up.x, _firePoint.up.y) * _fireForce + _rb2D.linearVelocity);
    }
}
