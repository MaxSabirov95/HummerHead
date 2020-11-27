using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerHead : MonoBehaviour
{
    private void Awake()
    {
        BlackBoard.hummerHead = this;
    }

    //public ParticleSystem effect;
    public float force = 500;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (GetComponentInParent<Rigidbody2D>().velocity.y <= 0) return;
        if (col.CompareTag("Breakable"))
        {
            col.GetComponent<Explodable>()?.explode(transform.position); // if the component is not null, call the method
            //effect.transform.position = col.transform.position;
            //ParticleSystem.ShapeModule sp = effect.shape;
            //sp.rotation = Vector2.SignedAngle(GetComponentInParent<Rigidbody2D>().velocity, Vector2.up) * Vector2.up;
            //effect.Play();
            Cam.camera.ShakeCamera();
            
        }
        gameObject.SetActive(false);
    }
}
