using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageble
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;

    [Header("Shooting")]
    [SerializeField] string poolTag;
    [SerializeField] Transform _firePoint;
    [SerializeField] float _fireForce;
    [SerializeField] float _timeBetweenBullets;
    WaitForSeconds _waitBetweenBullets;

    private Bullet currentbullet;

    Rigidbody2D rb;
    Transform target;
    private Vector2 moveDirection;

    [SerializeField] bool _canFollowPlayer;
    public bool CanFollowPlayer
    {
        get
        {
            return _canFollowPlayer;
        }
        set
        {
            _canFollowPlayer = value;
            if (_canFollowPlayer)
            {
                StartCoroutine(Shoot());
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }

    private float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health < 0)
            {
                Debug.LogWarning("ENEMY DIED" + name);
            }
        }

    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        _waitBetweenBullets = new WaitForSeconds(_timeBetweenBullets);
    }
    private void Update()
    {
        if (!_canFollowPlayer)
            return;

        moveDirection = (target.position - transform.position).normalized;
        
        var direction = target.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void FixedUpdate()
    {
        if (!_canFollowPlayer)
            return;

        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y)* moveSpeed;
    }

    public void Damage(float howMuch)
    {
        Health -= howMuch;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return _waitBetweenBullets;
            if (!_canFollowPlayer)
                break;

            currentbullet = BulletPooler.Instance.GetPoolObject(poolTag);
            currentbullet.Shoot(_firePoint, new Vector2(_firePoint.up.x, _firePoint.up.y) * _fireForce + rb.linearVelocity);
        } 
    }
}
