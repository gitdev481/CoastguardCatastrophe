using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatMechanics : MonoBehaviour {
    public GameObject cliff;
    public ScoreSystem scoreSystem;
    public HealthSystem healthSystem;
    public AudioSystem audioSystem;
    
    public float boatSpeed = 1000f;
    public float boatScore = 10f;
    public float boatDamage = 0f;
    public float currentBoatHealth = 0f;
    public float maxBoatHealth = 0f;
    public int boatType = 0;
    public AudioSource boatSound;
    public BoatArray boatArray;
    public Sprite boatImage;
    public Sprite boatSprite;
    public Vector2 boatRect;
    public Vector2 sideRect;
    private float fadeOutTime = 0f;
    private float fadeOutTimeThreshold = 0.5f;
    

    public bool startDestruction = false;

    
    private void Awake()
    {
        fadeOutTime = fadeOutTimeThreshold;
        audioSystem = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioSystem>();
        cliff = GameObject.FindGameObjectWithTag("CLIFF");
        scoreSystem = GameObject.FindGameObjectWithTag("SCORE").GetComponent<ScoreSystem>();
        healthSystem = GameObject.FindGameObjectWithTag("HEALTH").GetComponent<HealthSystem>();
        
       
    }
    // Use this for initialization
    void Start () {
        GetComponent<RectTransform>().sizeDelta = boatRect;
        boatArray = GameObject.FindGameObjectWithTag("SPAWNSYSTEM").GetComponent<SpawnSystem>().boatArray[boatType];
        gameObject.GetComponent<Image>().sprite = boatArray.boatArray[0];
        if (boatSound != null)
        {
            boatSound.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (startDestruction)
        {
            DestroyBoat();  
        }
    }
    void DestroyBoat()
    {
        fadeOutTime -= Time.deltaTime;
        Color newColor = GetComponent<Image>().color;
        newColor.a = (fadeOutTime/fadeOutTimeThreshold);
        GetComponent<Image>().color = newColor;
        if (fadeOutTime <= 0)
        {
            startDestruction = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //BOAT CRASH
        if (collider.gameObject == cliff)
        {
            healthSystem.TakeDamage(boatDamage);
            // audioSystem.PlaySound(audioSystem.boatCrashed);
            audioSystem.PlaySource(audioSystem.boatCrashedSource);
            startDestruction = true;
            GetComponent<Image>().sprite = boatArray.boatArray[3];
            GetComponent<RectTransform>().sizeDelta = sideRect;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.layer = 9;
        }
        if (collider.gameObject.tag == "SAFE")
        {
            scoreSystem.AddScore(boatScore);
            audioSystem.PlaySound(audioSystem.boatSaved);
            // audioSystem.PlaySource(audioSystem.boatCrashedSource);

              Destroy(gameObject);
        }  
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BEAM") {
            currentBoatHealth -= Time.deltaTime;
           // Debug.Log(currentBoatHealth);
        }

        if (currentBoatHealth <= 0)
        {
            //  stop colliding with everything by changing the physics layer
            gameObject.layer = 9;
            //stop moving
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            //check boat position to middle of screen, decide whether to go left or right.
            if (transform.localPosition.x <= 0)
            {
                // go left
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-boatSpeed, 0), ForceMode2D.Impulse);
                gameObject.GetComponent<Image>().sprite = boatArray.boatArray[1];
                GetComponent<RectTransform>().sizeDelta = sideRect;
                audioSystem.PlaySound(audioSystem.shipChangeDirection);
            }
            else
            {
                // go right
                GetComponent<Rigidbody2D>().AddForce(new Vector2(boatSpeed, 0), ForceMode2D.Impulse);
                gameObject.GetComponent<Image>().sprite = boatArray.boatArray[2];
                GetComponent<RectTransform>().sizeDelta = sideRect;
                audioSystem.PlaySound(audioSystem.shipChangeDirection);

            }
        }
    }


}
