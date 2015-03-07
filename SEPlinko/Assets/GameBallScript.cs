using UnityEngine;
using System.Collections;

public class GameBallScript : MonoBehaviour {

    private Vector3 _initialPosition;
	// Use this for initialization
	void Start () {
        _initialPosition = transform.position;
        //Start off with an initial downward velocity
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, -5, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = GetComponent<Rigidbody>();
	    if (transform.position.y > 0) {
            transform.position = _initialPosition;
            rb.velocity = new Vector3(0, -5, 0);
        }

        //Set minimum velocity
        if (rb.velocity.magnitude < 5) {
            rb.velocity.Normalize();
            rb.velocity = rb.velocity * 5;
        }

        //If the Y velocity is below a threshold, add a little "gravity" depending on the current direction

        if (Mathf.Abs (rb.velocity.y) < 2) {
            Vector3 newVelocity = new Vector3(
                rb.velocity.x,
                rb.velocity.y + ((rb.velocity.y<0) ? -0.1f : 0.1f),
                rb.velocity.z
                );
        }

	}

    
    void OnCollisionEnter (Collision col)
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

