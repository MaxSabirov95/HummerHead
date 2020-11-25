using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Explodable))]
public class ExplodeOnClick : MonoBehaviour {

	private Explodable _explodable;

	void Start()
	{
		_explodable = GetComponent<Explodable>();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.CompareTag("Player"))
        {
		    _explodable.explode(col.contacts[0].point);
		    //ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
		    //ef.doExplosion(transform.position);
        }
	}
}
