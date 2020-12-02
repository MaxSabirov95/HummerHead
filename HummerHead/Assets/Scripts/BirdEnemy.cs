using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class BirdEnemy : MonoBehaviour
{
    [SerializeField] private float flySpeed = 5;
    [SerializeField] private float bottomHeightRange = 0.8f;
    [SerializeField] private float topHeightRange = 0.9f;
    private int side;
    private float flySpeedValue;

    private Rigidbody2D birbRB;
    private SpriteRenderer birbSR;
    private ParticleSystem featherParticles;
    private Collider2D birbCol;

    public float minTimerToAttack;
    public float maxTimerToAttack;
    private float countDown;
    private bool attack;
    public float attackSpeed;

    private bool isDead;
    
    IEnumerator StartAttack()
    {
        flySpeed = 0;
        attack = true;
        yield return new WaitForSeconds(0.5f);
        Vector3 position = transform.position;
        Vector3 playerPos = BlackBoard.player.transform.position;
        while (transform.position != playerPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, attackSpeed * Time.deltaTime);
            yield return null;
        }
        while (transform.position != position)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, (attackSpeed * Time.deltaTime) / 1.5f);
            yield return null;
        }
        flySpeed = flySpeedValue;
        countDown = Random.Range(minTimerToAttack, maxTimerToAttack);
    }

    void Start()
    {
        birbRB = GetComponent<Rigidbody2D>();
        birbSR = GetComponent<SpriteRenderer>();
        birbCol = GetComponent<Collider2D>();
        featherParticles = GetComponentInChildren<ParticleSystem>();
        flySpeedValue = flySpeed;
        countDown = Random.Range(minTimerToAttack, maxTimerToAttack);
        RandomizeNewPos();
    }

    private void RandomizeNewPos()
    {
        side = Mathf.RoundToInt(Mathf.Sign(Random.Range(-2, 2)));
        birbSR.flipX = side == -1;
        Vector2 viewPortPos = new Vector2(0.5f + 0.7f * side, Random.Range(bottomHeightRange, topHeightRange));
        Vector2 worldPos = Camera.main.ViewportToWorldPoint(viewPortPos);
        transform.position = worldPos;
        flySpeed = flySpeedValue;
        birbSR.enabled = birbCol.enabled = true;
        attack = false;
        isDead = false;
    }

    void Update()
    {
        if (isDead) return;
        if (!attack)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                StartCoroutine("StartAttack");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FlyToPosition();
    }

    private void FlyToPosition()
    {
        if (!attack)
        {
            birbRB.velocity = -side * Vector2.right * flySpeed * Time.fixedDeltaTime;
        }

        Vector2 viewPortPos = Camera.main.WorldToViewportPoint(transform.position);
        if (Mathf.Abs(viewPortPos.x - (0.5f - 0.7f * side)) <= 0.05f)
        {
            RandomizeNewPos();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Fragment>())
        {
            StopAllCoroutines();
            StartCoroutine(DieWithEffect());
        }

        if (other.CompareTag("Player"))
        {
            BlackBoard.player.HP -= 10;
            BlackBoard.player.healthBar.SetHealth(BlackBoard.player.HP);
        }
    }

    IEnumerator DieWithEffect()
    {
        isDead = true;
        countDown = Random.Range(minTimerToAttack, maxTimerToAttack);
        flySpeed = 0;
        birbSR.enabled = birbCol.enabled = false;
        featherParticles.Play();
        yield return new WaitForSeconds(featherParticles.main.startLifetimeMultiplier);
        RandomizeNewPos();
    }
}
