using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSystem : MonoBehaviour {


    private Text scoreText;
    private Text boatsSavedText;
    private ScoreSystem scoreSystem;

    private void Awake()
    {
      //  if (GameObject.FindGameObjectWithTag("SCORE") != null)
      //  {
       //     scoreSystem = GameObject.FindGameObjectWithTag("SCORE").GetComponent<ScoreSystem>();
       // }
       // scoreText = GameObject.FindGameObjectWithTag("SCORETEXT").GetComponent<Text>();
      //  boatsSavedText = GameObject.FindGameObjectWithTag("BOATSSAVEDTEXT").GetComponent<Text>();
    }
    // Use this for initialization
    void Start () {

       // scoreText.text = "Score: " + scoreSystem.globalScore.ToString();
       // boatsSavedText.text = "Boats Saved: " +  scoreSystem.boatsSaved.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
