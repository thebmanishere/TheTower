using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAttack : MonoBehaviour
{
    //Animation
    public Animator BossAnim;
   
    //Number vaules
    private float damage = 0;

    [HideInInspector]
    public float BossDamage, AttackTime, animPause, waitTime, startDmgTime, PauseDmgTime;

    //Float timers for attack animations
    [HideInInspector]
    public float Attack1Time, Attack2Time, Attack3Time, DisableDmgTime;
    

    //Bools
    public bool StartAttack = false;

    public bool Phase1, Phase2, Phase3;

    //Other scripts
    public BossHealth BossHP;
    public TestBossMovement stopMovement;

    //NavMeshAgent
    public NavMeshAgent stopNavMeshAgent;

    //Colliders 
    public Collider[] BossHitBoxes;
    public Collider BossHurtBox;


   
    void Start()
    {
        //Getting a referecne to our nav mesh and our animator.
   
        BossAnim = GetComponent<Animator>();
        BossHP = GetComponent<BossHealth>();
        stopMovement = GetComponent<TestBossMovement>();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            BossHP.BosstHitPoints = 120f;
        }

        if(Input.GetKeyDown(KeyCode.F2))
        {
            BossHP.BosstHitPoints = 40f;
        }

        if(Input.GetKeyDown(KeyCode.F3))
        {
            BossHP.BosstHitPoints = 0f;
        }

         
           if(Phase1)
            {
                damage = 2;
                StartCoroutine("StartPhase1", AttackTime);
                
            } else 

            if(Phase2)
            {
                damage = 6;
                StartCoroutine("StartPhase2", AttackTime);
            } else 

            if (Phase3)
            {
                damage = 8;
                StartCoroutine("StartPhase3", AttackTime);
            } 
       

        if (BossHP.BosstHitPoints <= 0f)
        {
            StopAllCoroutines();

            StartCoroutine("KillBoss", waitTime);
        }
    } 
   
    IEnumerator StartPhase1(float AttackTime)
    {
       
        Phase1 = true;

        BossAnim.SetBool("Phase1Attack1", true);

        BossAnim.SetBool("Phase1Attack2", true);

        yield return new WaitForSeconds(Attack1Time);

        

        if (BossHP.BosstHitPoints <= 120f)
        {
            StartCoroutine("PlayHurtAnim", waitTime);

            BossAnim.SetBool("Phase1Attack1", false);

            BossAnim.SetBool("Phase1Attack2", false);


            Phase1 = false;
            Phase2 = true;
        }
    }


    IEnumerator StartPhase2(float AttackTime)
    {
        
        Phase2 = true;

        BossAnim.SetBool("Phase2Attack1", true);

        BossAnim.SetBool("Phase2Attack2", true);

        yield return new WaitForSeconds(Attack2Time);

        if (BossHP.BosstHitPoints <= 40f)
        {
            StartCoroutine("PlayHurtAnim", waitTime);

            BossAnim.SetBool("Phase2Attack1", false);

            BossAnim.SetBool("Phase2Attack2", false);

            Phase2 = false;
            Phase3 = true;
        }
    }

    IEnumerator StartPhase3(float AttackTime)
    {
        Phase3 = true;
       
        BossAnim.SetBool("Phase3Attack1", true);

        BossAnim.SetBool("Phase3Attack2", true);   

        yield return new WaitForSeconds(Attack3Time);

    }

    
    void EnableFirstAttackDmg()
    {
        TakeDamage(BossHitBoxes[0]);

    }

    void EnableSecondAttackDmg()
    {
        TakeDamage(BossHitBoxes[1]);
    }


    IEnumerator PlayHurtAnim(float waitTime)
    {
        StopCoroutine("StartPhase1");
        StopCoroutine("StartPhase2");
        StopCoroutine("StartPhase3");

        BossHurtBox.enabled = false;

        stopNavMeshAgent.enabled = false;
        stopMovement.enabled = false;

        BossAnim.Play("Hurt");

        yield return new WaitForSeconds(animPause);

        BossAnim.enabled = false;

        if(Phase2)
        {
            StartCoroutine("StartPhase2", AttackTime);
            stopNavMeshAgent.enabled = true;
            stopMovement.enabled = true;
            BossAnim.enabled = true;
            BossHurtBox.enabled = true;
        }

        if(Phase3)
        {
            StartCoroutine("StartPhase3", AttackTime);
            stopNavMeshAgent.enabled = true;
            stopMovement.enabled = true;
            BossAnim.enabled = true;
            BossHurtBox.enabled = true;
        }
    }


    IEnumerator KillBoss(float waitTime)
    {
        Phase1 = false;
        Phase2 = false;
        Phase3 = false;

        BossAnim.SetBool("Phase1Attack1", false);
        BossAnim.SetBool("Phase1Attack2", false);

        BossAnim.SetBool("Phase2Attack1", false);
        BossAnim.SetBool("Phase2Attack2", false);

        BossAnim.SetBool("Phase3Attack1", false);
        BossAnim.SetBool("Phase3Attack2", false);

        BossAnim.Play("Death");

        stopNavMeshAgent.enabled = false;
        stopMovement.enabled = false;

        yield return new WaitForSeconds(animPause);

        BossAnim.enabled = false;

        Phase3 = false;
    }

    

    void TakeDamage(Collider col)
    {
        //Here we are checking for colliders with the layser mask "PlayerHitbox", and we will apply damage to the player.


        //Collider[] PlayerCols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitboxPlayer"));
        Collider[] PlayerCols = Physics.OverlapSphere(col.bounds.center, 2f ,LayerMask.GetMask("HitboxPlayer"));

            foreach (Collider b in PlayerCols)
            {
                if (b.transform.root == transform)
                {
                    continue;
                }


                switch (b.name)
                {
                    case "PlayerHitbox":

                        damage = BossDamage;

                        b.SendMessageUpwards("TakeDamage_Player", damage);

                        break;
                }
            }

        }

    
}
