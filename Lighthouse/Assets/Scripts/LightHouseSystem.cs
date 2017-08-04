using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouseSystem : MonoBehaviour {
    public GameObject[] lightHouseList;
    public AudioSystem audioSystem;
   
    private void Awake()
    {
        audioSystem = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioSystem>();
        lightHouseList = GameObject.FindGameObjectsWithTag("LIGHTHOUSE");
    }
    
	void Start () {
        //turn middle light on to start
        lightHouseList[1].GetComponent<ControlLightHouse>().isLightHouseSelected = true;
        lightHouseList[1].GetComponent<ControlLightHouse>().TurnBeamOn();
        lightHouseList[1].GetComponent<ControlLightHouse>().TurnOutlineOn();
        lightHouseList[1].GetComponent<ControlLightHouse>().isBeamOn = true;
        lightHouseList[1].GetComponent<ControlLightHouse>().TurnGlowOn();

    }

    // Update is called once per frame
    void Update () {
        SelectLightHouses();



    }
    void SelectLightHouses()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetLighthouseSelection();
            TurnLightHouseOn(0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ResetLighthouseSelection();
            TurnLightHouseOn(1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ResetLighthouseSelection();
            TurnLightHouseOn(2);
        }
    }

    void TurnLightHouseOn(int lightHouseNumber)
    {
        lightHouseList[lightHouseNumber].GetComponent<ControlLightHouse>().isLightHouseSelected = true;
        lightHouseList[lightHouseNumber].GetComponent<ControlLightHouse>().TurnOutlineOn();
        audioSystem.PlaySource(audioSystem.switchSource);
        if (!lightHouseList[lightHouseNumber].GetComponent<ControlLightHouse>().isBeamOn)
        {
            audioSystem.PlaySound(audioSystem.toggleLight);

        }

        if (!lightHouseList[lightHouseNumber].GetComponent<ControlLightHouse>().fullyDrained){
            lightHouseList[lightHouseNumber].GetComponent<ControlLightHouse>().TurnBeamOn();
            lightHouseList[lightHouseNumber].GetComponent<ControlLightHouse>().isBeamOn = true;
            lightHouseList[lightHouseNumber].GetComponent<ControlLightHouse>().TurnGlowOn();
        }
    }

    void ResetLighthouseSelection()
    {
        foreach (GameObject lighthouse in lightHouseList)
        {
            lighthouse.GetComponent<ControlLightHouse>().isLightHouseSelected = false;
            lighthouse.GetComponent<ControlLightHouse>().TurnOutlineOff();
        }
    }
}
