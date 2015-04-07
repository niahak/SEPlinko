using UnityEngine;
using System.Collections;
using Leap;

public class PlayerMovement : MonoBehaviour {

	private Leap.Controller controller;

	bool _lastEnter = false;
	bool _lastEsc = false;
	float highestVolumeSeen = 0f;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
		controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
		controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
		highestVolumeSeen = 2;
	}
	
	// Update is called once per frame
	void Update () {

		var loudness = GetComponent<MicrophoneInput> ().loudness;
		if (loudness > highestVolumeSeen) {
			loudness = highestVolumeSeen;
		}

		var adjustedLoudness = loudness - (highestVolumeSeen / 2);

		var moveSpeed = loudness * 0.08f;

		//Add a "gravity" factor
		moveSpeed -= 0.040f;

		//Used for debugging...
		//ScoreManager.score = (int) ( GetComponent<MicrophoneInput> ().loudness * 100);

		if((transform.position.x < -4.5 && moveSpeed < 0) ||
		   (transform.position.x > 4.5 && moveSpeed > 0))
		{
			moveSpeed = 0;
		}
		transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);

		if (Input.GetKeyDown (KeyCode.Return) && !_lastEnter) {
			_lastEnter = true;
			ScoreManager.CircleAction ();
		} else if (!Input.GetKeyDown (KeyCode.Return)) {
			_lastEnter = false;
		}

		if (Input.GetKeyDown (KeyCode.Escape) && !_lastEsc) {
			_lastEsc = true;
			ScoreManager.CircleAction ();
		} else if (!Input.GetKeyDown (KeyCode.Escape)) {
			_lastEsc = false;
		}

		/*Frame frame = controller.Frame ();
		HandList hands = frame.Hands;
		Hand leap_hand = hands[0];

		foreach (Gesture g in frame.Gestures()) {

			if(g.Type == Gesture.GestureType.TYPECIRCLE)
			{
				ScoreManager.CircleAction();
			}
			else if(g.Type == Gesture.GestureType.TYPESCREENTAP)
			{
				ScoreManager.ScreenTap();

			}
		}

		if (leap_hand == null || gameObject == null)
			return;
		else {
			var controller_ = GameObject.Find ("HandController");
			/*var rb = gameObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.velocity = leap_hand.PalmVelocity.ToUnityScaled() * scaleMovement;
            }
			float initialY = gameObject.transform.position.y;
			gameObject.transform.position = controller_.transform.TransformPoint (leap_hand.PalmPosition.ToUnityScaled ());
			//Ignore the Z component
			float newX = gameObject.transform.position.x;
			if(newX > 4.5)
			{
				newX = 4.5f;
			} else if (newX < -4.5)
			{
				newX = -4.5f;
			}

			gameObject.transform.position = 
				new Vector3(newX, initialY, 0);
		}*/
	}
}
