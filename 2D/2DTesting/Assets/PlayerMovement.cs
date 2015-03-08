using UnityEngine;
using System.Collections;
using Leap;

public class PlayerMovement : MonoBehaviour {

	private Leap.Controller controller;
	private Transform ballInPlay;

	//Designer variables
	public Transform ballPrefab;

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
			if(ScoreManager.gameState == GameState.GameOver)
			{
				if(g.Type == Gesture.GestureType.TYPECIRCLE)
				{
					ScoreManager.CircleAction();
				}
			}
			else if(ScoreManager.gameState == GameState.StageComplete)
			{
				if(g.Type == Gesture.GestureType.TYPECIRCLE)
				{
					ScoreManager.CircleAction();
				}
			}
			else {
				if(g.Type == Gesture.GestureType.TYPECIRCLE && !ScoreManager.BallInPlay)
				{
					//TODO: only "start" the game if it's not started
					ScoreManager.BallInPlay = true;
					ballInPlay = Instantiate(ballPrefab) as Transform;
					ballInPlay.position = new Vector3(transform.position.x, transform.position.y);
					var rb = ballInPlay.GetComponent<Rigidbody2D>();
					rb.velocity = new Vector2(2, -4);
				}
				else if(g.Type == Gesture.GestureType.TYPESCREENTAP && ScoreManager.BallInPlay)
				{
					if(ballInPlay != null)
					{
						Destroy(ballInPlay.gameObject);
						ScoreManager.BallInPlay = false;
						ScoreManager.lives--;
					}
			}
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
