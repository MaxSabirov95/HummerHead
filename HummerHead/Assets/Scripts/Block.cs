using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hummer")
        {
            gameObject.SetActive(false);
        }
        Debug.Log(collision.gameObject.name);
    }
}
