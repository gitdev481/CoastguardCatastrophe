using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour {
    public AudioSource audioTemp;
    public AudioClip boatSaved;
    public AudioClip boatCrashed;
    public AudioClip shipChangeDirection;
    public AudioClip toggleLight;

    public AudioSource boatCrashedSource;
    public AudioSource switchSource;
    public AudioSource powerUpSource;
    public AudioSource powerDownSource;
    public AudioSource bigBoatSource;
    public AudioSource powerBoatSource;
    public AudioSource waveStartSource;


    private void Start()
    {
        audioTemp = GetComponent<AudioSource>();
      
    }

    public void PlaySound(AudioClip clip)
    {
        audioTemp.PlayOneShot(clip, 1f);
    }

   public void PlaySource(AudioSource source)
    {
        source.Play();
    }

}
