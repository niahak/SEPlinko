using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState {
	GameRunning,
	GameOver,
	StageComplete
}

public class ScoreManager : MonoBehaviour {


	private static int currentStageIndex = 1;
	private static GameObject currentStage;
	private static GameObject player;

	readonly static string[] stages = new string[] {
		"Stage1",
		"Stage2",
		"Stage3",
		"Stage4"
	};

	//TODO: Refactor so this only manages score, or manage game state someplace more obvious + central.
	private static GameState gameState = GameState.GameRunning;
	public static bool BallInPlay;
	private static Transform ballInPlay;
	public static int score;
	public static int lives;
	Text text;

	// Use this for initialization
	void Start () {
		player = (GameObject)Instantiate(Resources.Load("Player"));
	}

	void Awake()
	{
		text = GetComponent<Text> ();
		BallInPlay = false;
		score = 0;
		lives = 3;
		gameState = GameState.GameRunning;

		currentStageIndex = 1;

		LoadStage (currentStageIndex);
	}
	
	// Update is called once per frame
	void Update () {

		switch (gameState) {

		case GameState.GameOver:
			text.text = "Game over!  Spin to restart.";

			break;
		case GameState.StageComplete:
			text.text = "Stage complete! Spin to continue.";

			break;
		case GameState.GameRunning:
			text.text = string.Format ("Score: {0} Lives: {1}", score, lives);
			var block = currentStage.GetComponentInChildren<BreakableBlock>();
			if(block == null)
			{
				//Presumably we're out of blocks
				gameState = GameState.StageComplete;
				BallInPlay = false;
				Destroy(ballInPlay.gameObject);
			}
			
			if (lives < 0) {
				gameState = GameState.GameOver;
			}
			break;

		}
	}

	public static void CircleAction()
	{
		switch (gameState) {
			case GameState.GameOver:
				//Game state is automatically reset
				Application.LoadLevel (0);
				break;
			case GameState.StageComplete:
				currentStageIndex++;
				LoadStage(currentStageIndex);
				break;
		case GameState.GameRunning:
			if(!BallInPlay)
			{
				BallInPlay = true;
				GameObject ballObject = (GameObject)Instantiate(Resources.Load("Ball"));
				ballInPlay = ballObject.transform;
				var location = new Vector3(player.transform.position.x, player.transform.position.y);
				ballInPlay.position = location;
				var rb = ballInPlay.GetComponent<Rigidbody2D>();
				rb.velocity = new Vector2(2, -4);
			}
			break;
		}
	}

	public static void ScreenTap()
	{
		if(ballInPlay != null)
		{
			Destroy(ballInPlay.gameObject);
			ScoreManager.BallInPlay = false;
			ScoreManager.lives--;
		}
	}

	private static void LoadStage(int stageNum)
	{
		gameState = GameState.GameRunning;
		BallInPlay = false;
		int stageIndex = Mathf.Min (stageNum, stages.Length) - 1;
		Destroy (currentStage);
		currentStage = (GameObject)Instantiate(Resources.Load(stages[stageIndex]));
	}
}
