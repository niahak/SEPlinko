using UnityEngine;
using System.Collections;

public class PuckControl : MonoBehaviour {

    public Camera camera;
    public AudioClip pegSound;
    public AudioClip puckDropSound;
    int bucketCollisions = 0;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private AudioSource source;

    void Awake () {
        source = GetComponent<AudioSource>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (gameObject.GetComponent<Rigidbody> ().position.y < 5) {
            ResetPuck();
        }
	}

    private void ResetPuck()
    {
        gameObject.GetComponent<Rigidbody> ().position = originalPosition;
        gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(0,0,0);

        gameObject.GetComponent<Rigidbody> ().transform.rotation = originalRotation;
        
        camera.GetComponent<MoveOnRelease> ().Reset ();

        bucketCollisions = 0;
    }

    void OnCollisionEnter(Collision col) {
        if (col != null) {
            if (col.gameObject.tag == "Wall")
            {
                source.PlayOneShot(puckDropSound, 1.0f);
                //transform.position = originalPosition;
            }
            source.PlayOneShot(pegSound, 1.0f);

            if (col.gameObject.tag == "PuckBucket")
            {
                source.PlayOneShot(puckDropSound, 1.0f);
                bucketCollisions++;
                if(bucketCollisions >3)
                {
                    ResetPuck();
                }

                //transform.position = originalPosition;
            }
        }
    }
}
