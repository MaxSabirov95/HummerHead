using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class SwarmHead : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 10f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private int swarmCapacity = 30;
    [SerializeField] private GameObject swarmMonster;

    private GameObject[] swarmMonsters;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GenerateSwarm();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce((BlackBoard.player.transform.position - transform.position).normalized * speedMultiplier);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void GenerateSwarm()
    {
        swarmMonsters = new GameObject[swarmCapacity];
        for (int i = 0; i < swarmCapacity; i++)
        {
            Vector3 randomCirclePos = (new Vector3(Random.Range(-1f,1f), Random.Range(-1f,1f))).normalized;
            GameObject _monster = Instantiate(swarmMonster, transform.position + randomCirclePos, Quaternion.identity, transform);
            swarmMonsters[i] = _monster;
        }
    }
}
