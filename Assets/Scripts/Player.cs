using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageble
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;

    Vector2 movement;
    Vector2 direction;

    Animator animator;

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
            if(_health < 0)
            {
                Debug.LogWarning("Game ended");
            }
        }
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        //Turn the player
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (direction.sqrMagnitude > 0.01f) // Ensure movement is happening
        {
            transform.right = direction.normalized; // Rotate to face movement direction
        }

        animator.SetFloat("Move", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));

        /*animator.SetFloat("Horizontal",movement.x);
		animator.SetFloat("Vertical",movement.y);
		animator.SetFloat("Speed",movement.sqrMagnitude);*/
    }

    // Melhor para trabalhar com física	
    void FixedUpdate()
    {
        // Movement
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Damage(float howMuch)
    {
        Health -= howMuch;
    }
}
