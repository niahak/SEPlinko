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
    private bool finishedGame = false;
    private GameObject winText;
    private Material winTextMaterial;

    void Awake () {
        source = GetComponent<AudioSource>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        
        winText = GameObject.Find ("WinText");
        winTextMaterial = (Material)Resources.Load ("WinText", typeof(Material));
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
        Debug.Log ("Reset Puck");
        finishedGame = false;
        winText.GetComponent<MeshRenderer>().enabled = false;

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
                source.PlayOneShot(pegSound, 1.0f);
                //transform.position = originalPosition;
            }
            source.PlayOneShot(pegSound, 1.0f);

            if (col.gameObject.tag.StartsWith("PuckBucket"))
            {
                Debug.Log ("Puck Bucket");
                bucketCollisions++;
                if(bucketCollisions >3 && !finishedGame)
                {
                    Debug.Log ("End Game");
                    finishedGame = true;
                    if (col.gameObject.tag == "PuckBucketOrange") {
                        winTextMaterial.SetColor("_Color",new Color(.933f, .463f, .137f));
                    }
                    else if (col.gameObject.tag == "PuckBucketGreen") {
                        winTextMaterial.color = new Color(.278f, .675f, .282f);
                    }
                    else if (col.gameObject.tag == "PuckBucketPurple") {
                        winTextMaterial.SetColor("_Color",new Color(.29f, .145f, .408f));
                    }
                    winText.GetComponent<MeshRenderer>().enabled = true;
                    winText.GetComponent<Animator>().Play("Win",-1,0f);

                    source.PlayOneShot(puckDropSound, 1.0f);

                    Invoke ("ResetPuck", 3.0f);
                }

                //transform.position = originalPosition;
            }
        }
    }
}
