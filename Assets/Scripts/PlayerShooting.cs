using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Transform _firePoint;
    [SerializeField] float _fireForce;

    private Bullet currentbullet;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentbullet = BulletPooler.Instance.GetPoolObject("PlayerBullets");
            currentbullet.Shoot(_firePoint, _firePoint.up * _fireForce);
        }
    }
}
