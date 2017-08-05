
/*
    - This script is for the player movement and player turn controls. 
    
    - Mouse Transform/LookAt code used from https://www.youtube.com/watch?v=-91SYkWezAM

    - This script also incorprates camera movement from: http://answers.unity3d.com/questions/804400/movement-based-on-camera-direction.html
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody RB;
    public float PlayerMovementSpeed;
    private float PosX;
    public CursorLockMode LockState;
    public Animator PlayerAnim;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        PlayerAnim = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        PlayerAnim.SetFloat("isRunningFwd", v);

        Movement(h, v);
        Rotate();
            

    }

    
    void Rotate()
    {
         PosX += Input.GetAxis("Mouse X");
       
        Quaternion PlayerRotation = Quaternion.Euler(0, PosX, 0);

        RB.rotation = PlayerRotation;
        
    }

     

    void Movement(float h, float v)
    {

        Vector3 targetDirection = new Vector3(h, 0, v);

        targetDirection = Camera.main.transform.TransformDirection(targetDirection);

        targetDirection.y = 0.0f;

        RB.velocity = targetDirection * PlayerMovementSpeed;

       
    }
}
