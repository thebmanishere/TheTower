using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//The movement script for bosses. Probably should have had
// forsight to not called this 'test'. Oops. 

public class TestBossMovement : MonoBehaviour {

    NavMeshAgent BossNMV;

    public GameObject BossTarget;

    Animator BossAnim;

    public bool canMove = false;
  

    void Start ()
    {
        BossNMV = GetComponent<NavMeshAgent>();
        BossAnim = GetComponent<Animator>();
        
	}

    void Update()
    { 
        MoveTowardsPlayer();
    }

    public void MoveTowardsPlayer()
    {
        BossNMV.SetDestination(BossTarget.gameObject.transform.position);

        BossAnim.SetBool("isWalkingBool", true);
        //BossAnim.SetFloat("isWalkingFloat", 1);


        BossNMV.enabled = true;
    }
}
