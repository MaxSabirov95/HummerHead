using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmMonster : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 10f;
    [SerializeField] private float maxVelocity = 3f;
    private SwarmHead headToFollow;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        headToFollow = GetComponentInParent<SwarmHead>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (headToFollow.transform.position - transform.position).normalized;
        rb.AddForce(direction * speedMultiplier);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Fragment>()) //if the colliding object is a fragment
        {
            headToFollow.RegisterMonsterDeath(this);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            BlackBoard.player.TakeDamage(10);
        }
    }
}
