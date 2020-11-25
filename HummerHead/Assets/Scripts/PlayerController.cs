using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float maxMoveSpeed;
    public float jumpForce;
    Rigidbody2D playerRB;
    private GameObject breakHummerHead;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        breakHummerHead = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (playerRB.velocity.y < 0 && breakHummerHead.activeInHierarchy)
        {
            breakHummerHead.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        playerRB.AddForce(horizontal * moveSpeed * Vector3.right);
        Vector2 vel = playerRB.velocity;
        vel.x = Mathf.Clamp(vel.x, -maxMoveSpeed, maxMoveSpeed);
        playerRB.velocity = vel;
    }
    void Jump()
    {
        playerRB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        breakHummerHead.SetActive(true);
    }
}
