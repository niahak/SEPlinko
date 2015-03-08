using UnityEngine;
using System.Collections;
using Leap;

public class PlayerMovement : MonoBehaviour {

	private Leap.Controller controller;


	// Use this for initialization
	void Start () {
		controller = new Controller ();
		controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
		controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
	}
	
	// Update is called once per frame
	void Update () {

		Frame frame = controller.Frame ();
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
            }*/
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
		}
	}
}
