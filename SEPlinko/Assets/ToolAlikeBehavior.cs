using UnityEngine;
using System.Collections;
using Leap;

public class ToolAlikeBehavior : MonoBehaviour {

    private int updatesToIgnoreCollision = 0;
    private Collision _ball;

    public float scaleMovement = 20.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (updatesToIgnoreCollision > 0) {
            updatesToIgnoreCollision--;
            if(updatesToIgnoreCollision == 0)
            {
                var myCol = gameObject.GetComponent<Collider>();
                Physics.IgnoreCollision(myCol, _ball.collider, false);
            }
        }
        Controller controller = new Controller ();
        Frame frame = controller.Frame ();
        HandList hands = frame.Hands;
        Hand leap_hand = hands[0];
        PointableList pointables = frame.Pointables;
        FingerList fingers = frame.Fingers;
        ToolList tools = frame.Tools;
        
        if (leap_hand == null || gameObject == null)
            return;
        else {
            var controller_ = GameObject.Find ("HandController");
            var rb = gameObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.velocity = leap_hand.PalmVelocity.ToUnityScaled() * scaleMovement;
            }
            //gameObject.transform.position = controller_.transform.TransformPoint (leap_hand.PalmPosition.ToUnityScaled ());
            gameObject.transform.rotation = leap_hand.Basis.Rotation();
        }
	}

    void OnCollisionEnter (Collision col)
    {
        var ball = col.gameObject.GetComponent<GameBallScript> ();
        if (ball != null) {
            _ball = col;
            var myCol = gameObject.GetComponent<Collider>();
            //Ignore collision for the next half second or so, so we dont "phase" through it
          //Physics.IgnoreCollision(myCol, col.collider);
            updatesToIgnoreCollision = 100;
        }
    }
}
