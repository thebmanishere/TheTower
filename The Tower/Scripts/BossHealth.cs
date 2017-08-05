using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossHealth : MonoBehaviour {

    public Animator FirstBossAnim;
    public Slider BossHealthBar;
    public float BosstHitPoints = 100;
    private float damage, waitTime;
    public TestBossMovement BossMov;
   
   void Start()
    {
        BossMov = GetComponent<TestBossMovement>();
    }

    public void TakeDamage_Boss(float damage)
    {

        /*Simple function that will decrease both boss hit points and,
         * the float value of the slider. 
        */
       
            BosstHitPoints -= damage;
            BossHealthBar.value -= damage;
   }

        IEnumerator DisableBossMovement(float waitTime)
        {
            BossMov.canMove = false;

            yield return new WaitForSeconds(waitTime);

            BossMov.canMove = true;

        }


    }



