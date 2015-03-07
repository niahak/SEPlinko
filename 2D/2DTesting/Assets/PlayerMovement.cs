using UnityEngine;
using System.Collections;
using Leap;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
			/*var rb = gameObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.velocity = leap_hand.PalmVelocity.ToUnityScaled() * scaleMovement;
            }*/
			float initialY = gameObject.transform.position.y;
			gameObject.transform.position = controller_.transform.TransformPoint (leap_hand.PalmPosition.ToUnityScaled ());
			//Ignore the Z component
			gameObject.transform.position = 
				new Vector3(gameObject.transform.position.x, initialY, 0);
		}
	}
}
