using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSystem : MonoBehaviour
{
    public AudioSystem audioSystem;
    public GameObject spawnSystem;
    public GameObject boatPrefab;
    public ScoreSystem scoreSystem;

    public float globalTimer;
    public float waveTimer;
    public float resettableTimer;
    private float timeBetweenBoatSpawns = 1.0f;
    private float boatThrust = 1000f;

    public BoatArray[] boatArray;
    public BoatArray boat1SpriteArray;
    public BoatArray boat2SpriteArray;
    public BoatArray boat3SpriteArray;

    public int waveNumber = 0;
    private float waveThrust = 0f;
    private float waveSpawnRate = 0f;
    private float waveLength = 0f;
    private Text waveText;
    private bool shouldWaveTextFadeOut = false;
    private float fadeOutTime = 0f;
    private float fadeOutTimeThreshold = 2f;

    private void Awake()
    {
        boatArray[0] = boat1SpriteArray;
        boatArray[1] = boat2SpriteArray;
        boatArray[2] = boat3SpriteArray;

    
    }

    void Start()
    {

        waveText = GameObject.FindGameObjectWithTag("WAVETEXT").GetComponent<Text>();
        spawnSystem = GameObject.FindGameObjectWithTag("SPAWN");
        scoreSystem = GameObject.FindGameObjectWithTag("SCORE").GetComponent<ScoreSystem>();
        audioSystem = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioSystem>();

        waveNumber = 1;
         waveThrust = 0.25f;
         waveSpawnRate = 5f;
         waveLength = 20.26f;
         waveText.text = "WAVE "+ waveNumber;
         //Color newColor = waveText.color;
         //newColor.a = 1;
        // waveText.color = newColor;
         shouldWaveTextFadeOut = true;
        fadeOutTime = fadeOutTimeThreshold;
        audioSystem.PlaySource(audioSystem.waveStartSource);

    }

    void Update()
    {
        globalTimer += Time.deltaTime;
        waveTimer += Time.deltaTime;
        ResetTimerBoatSystem();
        FadeOutWaveText();
 
        UpdateWaves();


    }
    void FadeOutWaveText()
    {
        if (shouldWaveTextFadeOut)
        {
            fadeOutTime -= Time.deltaTime;
            Color newColor = waveText.color;
            newColor.a = (fadeOutTime / fadeOutTimeThreshold);
            waveText.color = newColor;
            if (fadeOutTime <= 0)
            {
                shouldWaveTextFadeOut = false;
            }
        }
    }
    void UpdateWaves()
    {
        //Debug.Log("WAVETIMER : " + waveTimer);
        if (waveTimer >= waveLength)
        {
            waveTimer = 0;
            waveNumber++;
            scoreSystem.AddWave();
            InitiateNextWave(waveNumber);
            shouldWaveTextFadeOut = true;
            fadeOutTime = fadeOutTimeThreshold;
            audioSystem.PlaySource(audioSystem.waveStartSource);
        }
    }
    void InitiateNextWave(int waveNumber)
    {
        if(waveNumber == 2)
        {
            waveThrust = 0.50f;
            waveSpawnRate = 3f;
            waveLength = 23.14f;
            waveText.text = "WAVE  " + waveNumber;
        }
        else if(waveNumber == 3)
        {
            waveThrust = 0.75f;
            waveSpawnRate = 2f;
            waveLength = 44.36f;
            waveText.text = "WAVE  " + waveNumber;
        }
        else if (waveNumber == 4)
        {
            waveThrust = 1f;
            waveSpawnRate = 1f;
            waveLength = 63.62f;
            waveText.text = "WAVE  " + waveNumber;
        }
        else if (waveNumber == 5)
        {
            waveThrust = 1.25f;
            waveSpawnRate = 1.25f;
            waveLength = 500f;
            waveText.text = "WAVE  " + waveNumber;
        }
    }

    void ResetTimerBoatSystem()
    {
        resettableTimer += Time.deltaTime;

        if (resettableTimer >= timeBetweenBoatSpawns * waveSpawnRate)
        {
            resettableTimer = 0;
            SpawnBoat();
           


        }
    }

    void SpawnBoat()
    {
        GameObject boatInstance = Instantiate(boatPrefab, new Vector3(Random.Range(0, Screen.width), spawnSystem.transform.position.y, spawnSystem.transform.position.z), Quaternion.Euler(0, 0, 0), spawnSystem.transform);
        SetBoatProperties(boatInstance, Random.Range(0, 3));
        boatInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-boatThrust * waveThrust), ForceMode2D.Impulse);
        //add boat spawn sound here.
    }
    void SetBoatProperties(GameObject boat, int boatType)
    {
      //  Debug.Log(boatType);
        BoatMechanics boatMechanics = boat.GetComponent<BoatMechanics>();
        if(boatType == 0)
        {
            //large boat
            boatThrust = 300f;
            boatMechanics.boatScore = 100f;
            boatMechanics.boatDamage = 50f;
            boatMechanics.maxBoatHealth = 0.7f;
            boatMechanics.currentBoatHealth = boatMechanics.maxBoatHealth;
          
            boatMechanics.boatRect = new Vector2(100, 300);
            boatMechanics.sideRect = new Vector2(300, 100);
            boatMechanics.boatSound = audioSystem.bigBoatSource;

        }
        else if (boatType == 1)
        {
            //sail boat
            boatThrust = 600f;
            boatMechanics.boatScore = 250f;
            boatMechanics.boatDamage = 25f;
            boatMechanics.maxBoatHealth = 0.25f;
            boatMechanics.currentBoatHealth = boatMechanics.maxBoatHealth;
            boatMechanics.boatType = boatType;
            boatMechanics.boatRect = new Vector2(75, 200);
            boatMechanics.sideRect = new Vector2(200, 75);

        }
        else if(boatType == 2)
        {
            //power boat
            boatThrust = 750f;
            boatMechanics.boatScore = 500f;
            boatMechanics.boatDamage = 10f;
            boatMechanics.maxBoatHealth = 0.1f;
            boatMechanics.currentBoatHealth = boatMechanics.maxBoatHealth;
            boatMechanics.boatType = boatType;
            boatMechanics.boatRect = new Vector2(100, 100);
            boatMechanics.sideRect = new Vector2(100, 100);
            boatMechanics.boatSound = audioSystem.powerBoatSource;



        }
    }
}
[System.Serializable]
public class BoatArray
{
    public Sprite[] boatArray;
}
