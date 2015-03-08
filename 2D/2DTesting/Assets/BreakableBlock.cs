using UnityEngine;
using System.Collections;

public class BreakableBlock : MonoBehaviour {

    /// <summary>
    /// Number of hits from a game ball to destroy
    /// </summary>
    public int hitsToDestroy = 1;

	private static readonly Color[] colorsByHits = 
	{
		Color.red,
		Color.green,
		Color.blue,
		Color.yellow,
		Color.white
	};

	// Use this for initialization
	void Start () {
		SetColor ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void TakeHit() {
		hitsToDestroy--;
		ScoreManager.score += 10;
		if (hitsToDestroy <= 0) {
			Destroy (gameObject);
		} else {
			SetColor ();
		}
	}

	void SetColor()
	{
		//Pick starting color based on the number of hits
		int colorIndex = Mathf.Min (hitsToDestroy, colorsByHits.Length) - 1;
		
		var renderer = GetComponent<SpriteRenderer> ();
		if (renderer != null) {
			renderer.color = colorsByHits[colorIndex];
		}
	}
}
