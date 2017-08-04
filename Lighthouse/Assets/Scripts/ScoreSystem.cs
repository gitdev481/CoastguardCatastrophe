using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour {
    public float globalScore;
    public int boatsSaved;
    public Text scoreText;
    public float globalTimer;
    private float savedTime;
    private bool totalTimeSaved = false;
    public Text timeText;
    public Text boatsSavedText;
    private bool setText = false;
    private float overallScore = 0f;
    private int timeInt = 0;
    public Text baseScoreText;

    // Use this for initialization

    private Text mainGameScoreText;
    private int waveReached = 1;
    public Text waveReachedText;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
       
    }
 
	void Start () {
        mainGameScoreText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(3);
        }

        if (SceneManager.GetActiveScene().name == "Main")
        {
            mainGameScoreText.text = globalScore.ToString();
        }

        if (SceneManager.GetActiveScene().name == "GameOverScene" && !setText)
        {
            boatsSavedText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0).GetComponent<Text>();
            timeText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetComponent<Text>();
            scoreText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(2).GetComponent<Text>();
            baseScoreText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(3).GetComponent<Text>();
            waveReachedText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(4).GetComponent<Text>();
            setText = true;
        }

        globalTimer += Time.deltaTime;

        if(SceneManager.GetActiveScene().name == "GameOverScene" && !totalTimeSaved)
        {
           SaveTotalTime();
        }
        if (SceneManager.GetActiveScene().name == "GameOverScene")
        {
            scoreText.text = ("OVERALL SCORE: ") + overallScore.ToString();
            timeText.text = ("TIME ALIVE: ") + timeInt.ToString();
            boatsSavedText.text = ("BOATS SAVED: ") + boatsSaved.ToString();
            baseScoreText.text = ("BOAT SCORE: ") + globalScore.ToString();
            waveReachedText.text = ("WAVE REACHED: ") + waveReached.ToString();
            //Debug.Log(overallScore);
        }

    }

    public void SaveTotalTime()
    {
        savedTime += globalTimer;
        timeInt = Mathf.RoundToInt(savedTime);
        overallScore = (globalScore * (timeInt/10) * boatsSaved * waveReached)/10;
        totalTimeSaved = true;
    }
    public void AddScore(float boatValue)
    {
        boatsSaved++;
        globalScore += boatValue;
    }
    public void AddWave( )
    {
        waveReached++;
    }
    public void SelfDestruct()
    {
        DestroyImmediate(gameObject);
    }
}
