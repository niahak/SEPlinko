using UnityEngine;
using System.Collections;

public class GameBallScript : MonoBehaviour {

    private Vector3 _initialPosition;
	// Use this for initialization
	void Start () {
        _initialPosition = transform.position;
        //Start off with an initial downward velocity
        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.velocity = new Vector3(0, -1, 0);
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.position.y > 0) {
            transform.position = _initialPosition;
            Rigidbody rb = GetComponent<Rigidbody>();
            //rb.velocity = new Vector3(0, -1, 0);
        }
	}

    
    void OnCollisionEnter (Collision col)
    {
        var block = col.gameObject.GetComponent<BreakableBlock> ();
        if (block != null) {
            block.hitsToDestroy--;
            if(block.hitsToDestroy == 0)
            {
                Destroy (col.gameObject);
            }
        }
    }
}

