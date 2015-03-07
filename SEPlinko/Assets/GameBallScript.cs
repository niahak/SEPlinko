using UnityEngine;
using System.Collections;

public class GameBallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
    void OnCollisionEnter (Collision col)
    {
        var block = col.gameObject.GetComponent<BreakableBlock> ();
        if (block != null) {
            block.hitsToDestroy--;
            if(block.hitsToDestroy == 0)
            {
                Destroy (col.gameObject);
            }
        }


    }
}

