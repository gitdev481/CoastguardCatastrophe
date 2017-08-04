using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlLightHouse : MonoBehaviour {

    public GameObject beam;
    private float rotationSpeed = 100.0f;
    private Vector3 rotationVector = new Vector3(0, 0, 1);
    public bool isLightHouseSelected = false;
    public bool isBeamOn  = false;
    private float maxbeamBattery = 100f;
    //drain rate = per second, higher number for faster drain
    private float drainRate = 10f;
    //private float liquidFillRate = 10f;

    //natural recharge rate = 2 seconds for full charge.
    private float rechargeRate = 0.0002f;
    private float currentbeamBattery = 0f;
    public bool fullyDrained = false;
    private bool beamCooldownStarted = false;
    //cooldown recharge = 5sec for 100%
    private float cooldownRechargeRate = 0.005f;

    public PolygonCollider2D beamCollider;
    public RectTransform beamRect;
    //max amount of beam to be shrunk = 40%
    //private float maxBeamShrinkage = 0.4f;

    //private float shrinkSpeed = 1f;

    //manually attached reference.
    public GameObject outline;
    public GameObject glow;

    public AudioSystem audioSystem;

    public Image chargebar;



    private void Awake()
    {
        chargebar = transform.FindChild("chargeparent").transform.FindChild("charge").GetComponent<Image>();
        audioSystem = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioSystem>();
        currentbeamBattery = maxbeamBattery;
        beam = this.transform.FindChild("beam").gameObject;
        beamCollider = beam.GetComponent<PolygonCollider2D>();
        beamRect = beam.GetComponent<RectTransform>();
    }
    void Start()
    {
        
    }
   
	void Update () {
        Control();
        ManageBattery();
        BeamCooldown();
        
        //makes sure the polygon collider always matches the size of the beam.
       // beamCollider.size = new Vector2(beamRect.sizeDelta.x, beamRect.sizeDelta.y);
    }




    public void Control()
    {
        if (isLightHouseSelected)
        {
            if (!fullyDrained)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    beam.transform.Rotate(rotationVector * (rotationSpeed * Time.deltaTime));
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    beam.transform.Rotate(rotationVector * -(rotationSpeed * Time.deltaTime));
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ToggleBeam();
                }
            }
        }
    }
    public void TurnBeamOn()
    {
        beam.SetActive(true);
       
    }
    public void TurnOutlineOn()
    {
        outline.SetActive(true);
    }
    public void TurnOutlineOff()
    {
        outline.SetActive(false);
    }
    public void TurnGlowOn()
    {
        glow.SetActive(true);
    }
    public void TurnGlowOff()
    {
        glow.SetActive(false);
    }
    void ToggleBeam()
    {
        audioSystem.PlaySound(audioSystem.toggleLight);
        if (isBeamOn)
        {
            beam.SetActive(false);
            isBeamOn = false;
            glow.SetActive(false);
        }
        else if (!isBeamOn)
        {
            beam.SetActive(true);
            isBeamOn = true;
            glow.SetActive(true);


        }
    }
    void ManageBattery()
    {
        //visual fill
        chargebar.fillAmount = (currentbeamBattery/maxbeamBattery);
        //glow opacity
        Color newColor = glow.GetComponent<Image>().color;
        newColor.a = (currentbeamBattery / maxbeamBattery);
        glow.GetComponent<Image>().color = newColor;

        //beam scale

       beam.transform.localScale = new Vector3(1, currentbeamBattery / maxbeamBattery, 1);



        if (!fullyDrained)
        {
            //drain whilst beam is on at 10% per second
            if (isBeamOn)
            {
                currentbeamBattery -= Time.deltaTime * drainRate;
               // beam.transform.localScale -= new Vector3(0,maxBeamShrinkage, 0) * Time.deltaTime / drainRate;


            }
            else if (!isBeamOn && currentbeamBattery < maxbeamBattery)
            {
                //natural recharge
               // Debug.Log("slowfill");
               // Debug.Log(fullyDrained);
                currentbeamBattery += (Time.deltaTime / (maxbeamBattery * rechargeRate));
                //beam.transform.localScale += new Vector3(0, maxBeamShrinkage, 0) * Time.deltaTime / liquidFillRate;

            }
            //set to 100% if it goes over a little bit.
            if (currentbeamBattery > maxbeamBattery)
            {
                currentbeamBattery = maxbeamBattery;
            }
        }
        if(currentbeamBattery <= 0 && !beamCooldownStarted)
        {
            //Battery fully drained. light turns off. cooldown begins. Power off sound.
            audioSystem.PlaySource(audioSystem.powerDownSource);
            beamCooldownStarted = true;
            StartBeamCooldown();
            //reset beam size
            beam.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void StartBeamCooldown()
    {
      //  Debug.Log("OUT OF BATTERY");
        fullyDrained = true;
        isBeamOn = false;
        beam.SetActive(false);
        glow.SetActive(false);
    }
    void BeamCooldown()
    {
        if (beamCooldownStarted)
        {
            //fast recharge          
            currentbeamBattery += (Time.deltaTime / (maxbeamBattery * cooldownRechargeRate));


            if (currentbeamBattery >= maxbeamBattery)
            {
                //fully charged.
                currentbeamBattery = maxbeamBattery;
                fullyDrained = false;
                beamCooldownStarted = false;
                audioSystem.PlaySource(audioSystem.powerUpSource);
            }

        }
       
      }
    }

