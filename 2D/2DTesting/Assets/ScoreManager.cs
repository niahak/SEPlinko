using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState {
	GameRunning,
	GameOver,
	StageComplete
}

public class ScoreManager : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}

	private static int currentStageIndex = 1;
	private static GameObject currentStage;

	readonly static string[] stages = new string[] {
		"Stage1",
		"Stage2",
		"Stage3",
		"Stage4"
	};

	//TODO: Refactor so this only manages score, or manage game state someplace more obvious + central.
	public static GameState gameState = GameState.GameRunning;
	public static bool BallInPlay;
	public static int score;
	public static int lives;
	Text text;

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
		}
	}

	private static void LoadStage(int stageNum)
	{
		gameState = GameState.GameRunning;
		int stageIndex = Mathf.Min (stageNum, stages.Length) - 1;
		Destroy (currentStage);
		currentStage = (GameObject)Instantiate(Resources.Load(stages[stageIndex]));
	}
}
