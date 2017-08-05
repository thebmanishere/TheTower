using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public Slider PlayerHealthBar;
    public float PlayerHitPoints;
    private float damage;

    void Update()
    {
        if(PlayerHitPoints <= 0)
        {
            SceneManager.LoadScene("main");
        }
    }

    public void TakeDamage_Player(float damage)
    {

        /*Simple function that will decrease both boss hit points and,
         * the float value of the slider. Same as the boss health script but 
         * just wanted a different name for the player. 
        */
   
            PlayerHitPoints -= damage;
            PlayerHealthBar.value -= damage;
    }
}

