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

    // Start is called before the first frame update
    void Start()
    {
        birbRB = GetComponent<Rigidbody2D>();
        birbSR = GetComponent<SpriteRenderer>();
        birbCol = GetComponent<Collider2D>();
        featherParticles = GetComponentInChildren<ParticleSystem>();
        flySpeedValue = flySpeed;
        RandomizeNewPos();
    }

    private void RandomizeNewPos()
    {
        side = Mathf.RoundToInt(Mathf.Sign(Random.Range(-2, 2)));
        birbSR.flipX = side == -1;
        Debug.Log(0.5f + 0.7f * side);
        Debug.Log(0.5f - 0.7f * side);
        Vector2 viewPortPos = new Vector2(0.5f + 0.7f * side, Random.Range(bottomHeightRange, topHeightRange));
        Vector2 worldPos = Camera.main.ViewportToWorldPoint(viewPortPos);
        transform.position = worldPos;
        flySpeed = flySpeedValue;
        birbSR.enabled = birbCol.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        birbRB.velocity = -side * Vector2.right * flySpeed * Time.fixedDeltaTime;

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
            StartCoroutine(DieWithEffect());
        }
    }

    IEnumerator DieWithEffect()
    {
        flySpeed = 0;
        birbSR.enabled = birbCol.enabled = false;
        featherParticles.Play();
        yield return new WaitForSeconds(featherParticles.main.startLifetimeMultiplier);
        RandomizeNewPos();
    }
}
