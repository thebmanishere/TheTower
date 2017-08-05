// Tutorial used: https://www.youtube.com/watch?v=PO5_aqapZXY&index=7&list=PLKFvhfT4QOqlEReJ2lSZJk_APVq5sxZ-x

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float disAway, disUp, Smooth = 0;

    [SerializeField]
    private Transform followPlayer;

    [SerializeField]
    private Vector3 targetPosition;
    private Vector3 lookDir;
    

    private float PosX;

    void LateUpdate()
    {      
        targetPosition = followPlayer.position + followPlayer.up * disUp - followPlayer.forward * disAway;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * Smooth);

        transform.LookAt(followPlayer);

    }

}
