using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour {

    private float playerHealth = 0f;
    private float playerHealthMax = 300f;
    private Image healthBar;

	// Use this for initialization
	void Start () {
        playerHealth = playerHealthMax;
        healthBar = GameObject.FindGameObjectWithTag("HEALTHBAR").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerHealth <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        ManageHealthBar();

    }
    public void ManageHealthBar()
    {
        healthBar.fillAmount = (playerHealth / playerHealthMax);
    }

    public void TakeDamage(float damage)
    {
        
        playerHealth -= damage;
      //  Debug.Log(playerHealth);
    }
}
