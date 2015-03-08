using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if (rb.position.y > 4.5) {
			Destroy(rb.gameObject);
			ScoreManager.lives--;
			ScoreManager.BallInPlay = false;
		}
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		var block = col.gameObject.GetComponent<BreakableBlock> ();
		if (block != null) {
			block.TakeHit();
		}
    }
}
