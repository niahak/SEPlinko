using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour {


	private Vector2 _initialPosition;
	// Use this for initialization
	void Start () {
		_initialPosition = transform.position;
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = new Vector2(0, -5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		var block = col.gameObject.GetComponent<BreakableBlock> ();
		if (block != null) {
			block.hitsToDestroy--;
			if(block.hitsToDestroy <= 0)
			{
				Destroy(block.gameObject);
			}
		}
    }
}
