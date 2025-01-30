using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageble
{
    [Header("Movement")]
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D rb;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;


    //[SerializeField] private float moveSpeed;

    //Vector2 movement;
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");
        _movementInput.Normalize();
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
                    _smoothedMovementInput,
                    _movementInput,
                    ref _movementInputSmoothVelocity,
                    0.1f);

        rb.linearVelocity = _smoothedMovementInput * _speed;
    }

    private void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            rb.MoveRotation(rotation);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    //  void Update()
    //  {
    //      // Input
    //      movement.x = Input.GetAxisRaw("Horizontal");
    //      movement.y = Input.GetAxisRaw("Vertical");
    //      movement.Normalize();

    //      //animator.SetFloat("x", movement.x);
    //      //animator.SetFloat("y", movement.y);

    //      /*animator.SetFloat("Horizontal",movement.x);
    //animator.SetFloat("Vertical",movement.y);
    //animator.SetFloat("Speed",movement.sqrMagnitude);*/
    //  }

    //  // Melhor para trabalhar com f�sica	
    //  void FixedUpdate()
    //  {
    //      // Movement
    //      //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    //      rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    //      if (movement.sqrMagnitude > 0)
    //      {
    //          float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
    //          float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, Time.fixedDeltaTime * 10f); // Adjust speed here
    //          rb.rotation = smoothAngle;
    //      }
    //  }

    public void Damage(float howMuch)
    {
        Health -= howMuch;
    }
}
