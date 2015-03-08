using UnityEngine;
using System.Collections;

public class MoveOnRelease : MonoBehaviour, IPinchWatcher {

    private Vector3 originalPosition;
    private Vector3 Velocity = new Vector3(0,0,0);
    private Vector3 targetPosition = new Vector3(3.9f, 31.45f, 37.14f);
    private Vector3 movingTo = new Vector3();

    void Awake () {
        originalPosition = transform.position;
        movingTo = targetPosition;
    }

    public void Pinched(bool pinched)
    {
        if (!pinched) {
            Debug.Log("Unpinched");
            Velocity = new Vector3 ((transform.position.x - targetPosition.x) / 30, (transform.position.y - targetPosition.y) / 30, (transform.position.z - targetPosition.z) / 30);
            movingTo = targetPosition;
            Debug.Log("X: " + Velocity.x);
            Debug.Log("Y: " + Velocity.y);
            Debug.Log("Z: " + Velocity.z);
        }
    }

	// Use this for initialization
	void Start () {
	
	}

    public void Reset()
    {
        Debug.Log("Reset");
        Velocity = new Vector3 ((transform.position.x - originalPosition.x) / 20, (transform.position.y - originalPosition.y) / 20, (transform.position.z - originalPosition.z) / 20);
        movingTo = originalPosition;
        Debug.Log("X: " + Velocity.x);
        Debug.Log("Y: " + Velocity.y);
        Debug.Log("Z: " + Velocity.z);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = new Vector3 (transform.position.x - Velocity.x, transform.position.y - Velocity.y, transform.position.z - Velocity.z);
        transform.position = newPos;
        //Debug.Log("XDiff: " + Mathf.Abs(transform.position.x - movingTo.x));
        //Debug.Log("YDiff: " + Mathf.Abs(transform.position.y - movingTo.y));
        //Debug.Log("ZDiff: " + Mathf.Abs(transform.position.z - movingTo.z));
        if (Mathf.Abs(transform.position.y - movingTo.y) < Mathf.Abs(Velocity.y / 2) ) {
            Debug.Log("Stop");
            transform.position = movingTo;
            Velocity = new Vector3();
        }
	}
}
