using UnityEngine;
using System.Collections;

public class PuckControl : MonoBehaviour {

    public AudioClip pegSound;
    public AudioClip puckDropSound;

    private Vector3 originalPosition;
    private AudioSource source;

    void Awake () {
        source = GetComponent<AudioSource>();
        originalPosition = transform.position;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (gameObject.GetComponent<Rigidbody> ().position.y < 5) {
            gameObject.GetComponent<Rigidbody> ().position = originalPosition;
            gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(0,0,0);
        }
	}

    void OnCollisionEnter(Collision col) {
        if (col != null) {
            if (col.gameObject.tag == "PuckBucket")
            {
                source.PlayOneShot(puckDropSound, 1.0f);
                //transform.position = originalPosition;
            }
            source.PlayOneShot(pegSound, 1.0f);
        }
    }
}
