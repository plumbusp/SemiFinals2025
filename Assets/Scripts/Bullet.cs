using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public event Action<Bullet> OnUsed;
    [SerializeField] private GameObject _postCollisionEffectPrefab;
    private Rigidbody2D _body2D;
    private Transform parent;
    private int parentLayerMask;

    public void Initialize(Transform parent)
    {
        _body2D = GetComponent<Rigidbody2D>();
        this.parent = parent;
        gameObject.SetActive(false);
    }

    public void Shoot(Transform spawnTransform, Vector2 force)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = spawnTransform.position;
        gameObject.transform.rotation = spawnTransform.rotation;
        _body2D.AddForce(force, ForceMode2D.Impulse);
    }

    public void Shoot(Vector3 direction, Vector3 rotation, float force, Transform firePoint)
    {
        transform.position = firePoint.position;
        gameObject.SetActive(true);
        _body2D.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;

    }

    public void Shoot(Vector3 direction, Vector3 rotation, float force, Transform firePoint, int parentLatyer)
    {
        parentLayerMask = parentLatyer; 
        transform.position = firePoint.position;
        gameObject.SetActive(true);
        _body2D.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;

    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collision " + collision.collider.name);
    //    Instantiate(_postCollisionEffectPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, transform.up), parent);
    //    gameObject.SetActive(false);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("Player"))
    //        return;

    //    Debug.Log("Collision " + collision.name);
    //    Instantiate(_postCollisionEffectPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, transform.up), parent);
    //    gameObject.SetActive(false);
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(_postCollisionEffectPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, transform.up), parent);
        gameObject.SetActive(false);
        Debug.Log("Collided with " + collision.gameObject.name);
    }

}
