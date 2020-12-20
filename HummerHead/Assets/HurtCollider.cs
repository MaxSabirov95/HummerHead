using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("in");
        if (other.CompareTag("Player"))
        {
            BlackBoard.player.TakeDamage(10);
        }
    }
}
