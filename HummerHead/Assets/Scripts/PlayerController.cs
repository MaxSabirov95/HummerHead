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

    private bool invulnerable = false;

    bool landsOnGround;
    bool faceRight;
    public int HP { get; set; }
    public int maxHP;
    public Bar healthBar;
    public GameObject HPBar;

    private float horizontalMove;

    void Awake()
    {
        BlackBoard.player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        breakHummerHead = transform.GetChild(0).gameObject;
        healthBar.SetMaxHealth(maxHP);
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        HandleAnimation();

        if (playerRB.velocity.y <= 0 && breakHummerHead.activeInHierarchy)
        {
            breakHummerHead.SetActive(false);
        }

        Debug.Log(HP);
    }

    private void HandleInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(25);
        }
    }

    private void FixedUpdate()
    {
        playerRB.AddForce(horizontalMove * moveSpeed * Vector3.right);
        Vector2 vel = playerRB.velocity;
        vel.x = Mathf.Clamp(vel.x, -maxMoveSpeed, maxMoveSpeed);
        playerRB.velocity = vel;
    }

    private void HandleAnimation()
    {
        float a = Mathf.Abs(horizontalMove * moveSpeed);

        anim.SetFloat("Run", a);

        if (landsOnGround)
        {
            anim.SetBool("Jump", false);
            landsOnGround = false;
        }

        if (horizontalMove > 0 && faceRight)
        {
            Flip();
        }
        else if (horizontalMove < 0 && !faceRight)
        {
            Flip();
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

    public void TakeDamage(int damage)
    {
        if (invulnerable) return;
        invulnerable = true;
        HP -= damage;
        healthBar.SetHealth(HP);
        Invoke("StopInvulnerable", 3f);
    }

    void StopInvulnerable()
    {
        invulnerable = false;
    }
}
