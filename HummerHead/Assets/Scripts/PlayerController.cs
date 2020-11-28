using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed;
    public float maxMoveSpeed;
    public float jumpForce;
    Rigidbody2D playerRB;
    private GameObject breakHummerHead;

    bool landsOnGround;
    bool faceRight;
    public int HP;
    public Bar healthBar;
    public GameObject HPBar;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        breakHummerHead = transform.GetChild(0).gameObject;
        healthBar.SetMaxHealth(HP);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }

        if (playerRB.velocity.y <= 0 && breakHummerHead.activeInHierarchy)
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
        float a = horizontal * moveSpeed;
        if (a < 0)
        {
            a *= -1;
        }
        anim.SetFloat("Run", a);

        if (landsOnGround)
        {
            anim.SetBool("Jump", false);
            landsOnGround = false;
        }

        if (horizontal > 0 && faceRight)
        {
            Flip();
        }
        else if (horizontal < 0 && !faceRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            HP -= 25;
            healthBar.SetHealth(HP);
        }
    }
    

    IEnumerator Jump()
    {
        anim.SetBool("Jump",true);
        yield return new WaitForSeconds(0.3f);
        playerRB.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        breakHummerHead.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("ground"))
        {
            landsOnGround = true;
        }
    }
    void Flip()
    {
        faceRight = !faceRight;
        HPBar.transform.forward = Camera.main.transform.forward;
        transform.Rotate(180 * Vector3.up);
    }
}
