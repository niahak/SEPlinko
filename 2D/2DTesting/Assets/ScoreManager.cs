using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public static int score;
	Text text;

	void Awake()
	{
		text = GetComponent<Text> ();
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Score: " + score;
	
	}
}
