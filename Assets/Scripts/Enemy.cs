using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageble
{
    Rigidbody2D rb;
    Transform target;
    public float Health { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

    }

    public void Damage(float howMuch)
    {
        Health -= howMuch;
    }


}
