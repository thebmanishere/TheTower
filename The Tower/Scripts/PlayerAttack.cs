//Hitbox/Applying damage code used from: https://www.youtube.com/watch?v=mvVM1RB4HXk&t=1453s


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public Animator PlayerAnim;
    public Collider[] SwordHitbox;
    public PlayerMovement MovSpeed;


    [SerializeField]
    private float damage = 0;

    [HideInInspector]
    public float attackPauseTime, startSecondAttack, startThirdAttack, PlayerDamage;

    [HideInInspector]
    public float SecondAttackPauseTime, ThirdAttackPauseTime;
    

    public Rigidbody RB;
    public RigidbodyConstraints StopMovement;

    void Start()
    {
        MovSpeed = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {


            if (PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("Moving"))
            {
                StartCoroutine("FirstAttack");
            } else 

            if(PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("FirstAttack"))
            {
                StartCoroutine("SecondAttack");
            } else

            if (PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("SecondAttack"))
            {
                StartCoroutine("ThirdAttack");
            }
        }



    }

    IEnumerator FirstAttack()
    {
        //Debug.Log("First attack has started...");

        PlayerAnim.Play("FirstAttack");
        PlayerAnim.SetBool("FirstAttack", true);

        yield return new WaitForSeconds(attackPauseTime);

        PlayerDamage = 4;

        SwordAttack(SwordHitbox[0]);

        PlayerAnim.SetBool("FirstAttack", false);
    }

    IEnumerator SecondAttack()
    {

        yield return new WaitForSeconds(startSecondAttack);

        //Debug.Log("Second attack has started...");

        PlayerAnim.Play("SecondAttack");
        PlayerAnim.SetBool("SecondAttack", true);
        MovSpeed.PlayerMovementSpeed = 3f;

        yield return new WaitForSeconds(SecondAttackPauseTime);

        PlayerDamage = 6;

        SwordAttack(SwordHitbox[0]);

        PlayerAnim.SetBool("SecondAttack", false);
        MovSpeed.PlayerMovementSpeed = 5f;

    }

    IEnumerator ThirdAttack()
    {

        yield return new WaitForSeconds(startThirdAttack);

        //Debug.Log("Third attack has started...");

        PlayerAnim.Play("ThirdAttack");
        PlayerAnim.SetBool("ThirdAttack", true);
        MovSpeed.PlayerMovementSpeed = 1f;

        yield return new WaitForSeconds(ThirdAttackPauseTime);

        PlayerDamage = 8;

        SwordAttack(SwordHitbox[0]);

        PlayerAnim.SetBool("ThirdAttack", false);
        MovSpeed.PlayerMovementSpeed = 5f;

    }

    void SwordAttack(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("HitboxBoss"));

        foreach (Collider c in cols)
        {
           if(c.transform.root == transform)
            {
                continue;
            }

            switch(c.name)
            {
                case "BossHitbox":

                    damage = PlayerDamage;

                    c.SendMessageUpwards("TakeDamage_Boss", damage);

                    break;
            }

        }
    }

   
}
