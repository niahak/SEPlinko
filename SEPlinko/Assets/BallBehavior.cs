using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour {

    Vector3 _initialPosition;
	// Use this for initialization
	void Start () {
        _initialPosition = transform.position;
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
            transform.position = _initialPosition;

        }
	}
}
