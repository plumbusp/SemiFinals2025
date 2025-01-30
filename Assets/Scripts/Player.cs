using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageble
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;

    Vector2 movement;

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

        //animator.SetFloat("x", movement.x);
        //animator.SetFloat("y", movement.y);

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

}
