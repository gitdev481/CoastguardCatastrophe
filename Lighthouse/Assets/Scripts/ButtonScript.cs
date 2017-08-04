using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonScript : MonoBehaviour {

    public ScoreSystem scoresSystem;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("SCORE") != null)
        {
            scoresSystem = GameObject.FindGameObjectWithTag("SCORE").GetComponent<ScoreSystem>();
        }
    }
    public void RestartButton()
    {
        if (GameObject.FindGameObjectWithTag("SCORE") != null)
        {
            scoresSystem.SelfDestruct();
        }
        SceneManager.LoadScene(0);
    }
    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
    public void InstructionsButton()
    {
        SceneManager.LoadScene(1);
    }
    public void Playbutton()
    {
        SceneManager.LoadScene(2);
    }
	public void QuitButton() {
        Application.Quit();
    }


}
