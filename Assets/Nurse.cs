using UnityEngine;
using UnityEngine.EventSystems;

public class Nurse : MonoBehaviour, IDamageble
{
    [Header("Health")]
    [SerializeField] float health;

    [SerializeField] private GameObject calmDown;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float howSlowPlayerIs;
    private float normalPlayerSpeed;

    //Following
    private Rigidbody2D rb;
    private Animator animator;
    private Transform target;
    private Vector2 moveDirection;


    bool _canFollowPlayer;
    public bool CanFollowPlayer
    {
        get
        {
            return _canFollowPlayer;
        }
        set
        {
            _canFollowPlayer = value;
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
                Destroy(gameObject);
            }
        }

    }


    private void Start()
    {
        _canFollowPlayer = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (!_canFollowPlayer)
            return;

        moveDirection = (target.position - transform.position).normalized;

        //rotating Towards Player
        var direction = target.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //rotating Towards Player
    }

    private void FixedUpdate()
    {
        if (!_canFollowPlayer)
            return;

        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            normalPlayerSpeed = collision.gameObject.GetComponent<Player>().moveSpeed;
            collision.gameObject.GetComponent<Player>().moveSpeed = howSlowPlayerIs;
            animator.SetBool("IsAtPlayer", true);
            calmDown.SetActive(true);
            _canFollowPlayer = false;
            Debug.Log("(collision.gameObject.CompareTag(\"Player\"))");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().moveSpeed = normalPlayerSpeed;
            animator.SetBool("IsAtPlayer", false);
            calmDown.SetActive(false);
            _canFollowPlayer = true;
            Debug.Log(" EXIT (collision.gameObject.CompareTag(\"Player\"))");
        }
    }

    public void Damage(float howMuch)
    {
        Health -= howMuch;
    }


}
