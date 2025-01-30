using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] bool _canFollowPlayer;
    Rigidbody2D rb;
    Transform target;
    private Vector2 moveDirection;
    public float Health { get; set; }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (!_canFollowPlayer)
            return;

        moveDirection = target.position - transform.position;

    }

    public void Damage(float howMuch)
    {
        Health -= howMuch;
    }


}
