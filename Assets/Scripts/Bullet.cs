using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public event Action<Bullet> OnUsed;
    [SerializeField] private GameObject _postCollisionEffectPrefab;
    private Rigidbody2D _body2D;
    public void Initialize()
    {
        _body2D = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Transform spawnTransform, Vector2 force)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = spawnTransform.position;
        gameObject.transform.rotation = spawnTransform.rotation;
        _body2D.AddForce(force, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(_postCollisionEffectPrefab, transform);
        gameObject.SetActive(false);
        OnUsed?.Invoke(this);
    }
}
