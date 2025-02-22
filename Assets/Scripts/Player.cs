using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent (typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageble
{
    [Header("Health")]
    [SerializeField] float health;
    public HealthBar healthBar;

    [Header("Movement")]
    public float moveSpeed;

    private Rigidbody2D rb;

    Vector2 movement;
    Vector2 direction;

    Animator animator;
    [SerializeField]private Transform _initialPosition;

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
            if (_health <= 0)
            {
                Reset();
                Debug.LogWarning("Game ended");
            }
        }
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _health = health;
        healthBar.SetHealth(health);
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
        healthBar.Decrease(howMuch);
        Debug.Log("Damaging " + name);
        Health -= howMuch;
    }

    private void Reset()
    {
        transform.position = _initialPosition.position;
        healthBar.SetHealth(health);
        moveSpeed = 7f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Nurse"))
        {
            moveSpeed = 7f;
        }
    }
}
