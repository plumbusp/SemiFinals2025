using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] float moveSpeed;
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


}
