using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callBossAttack : MonoBehaviour {

    public BossAttack bossAttack;
    //public Collider BossHitBox1;
    //public float waitTime = 10f;
   
    void Awake()
    {
        bossAttack = GetComponent<BossAttack>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Player has entered attack trigger..");
            //bossAttack.BossAttacksAnim();
        }
        
    } 
}
