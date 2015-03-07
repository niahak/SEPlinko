using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    //Check if the sphere is outside the play area
	  if (transform.position.z > 25 ||
            transform.position.y > 25 ||
            transform.position.y < -25 ||
            transform.position.x > 25 ||
            transform.position.x < -25) {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(0.0f, 2.49f, 1.89f);

        }
	}
}
