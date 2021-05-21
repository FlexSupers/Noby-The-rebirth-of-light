using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterMovement : MonoBehaviour
{
    public Transform CameraTransform;
    public Status status;
    public Animator anim;

    public float vertical;
    public float horizontal;
    public float moveAmount;

    public Vector3 rotationDirection;
    public Vector3 moveDirection;
    

    public void MoveUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

        anim.SetFloat("vertical", vertical, 0.15f, Time.deltaTime);

        Vector3 moveDir = CameraTransform.forward * vertical;
        moveDir += CameraTransform.right * horizontal;
        moveDir.Normalize();
        moveDirection = moveDir;
        rotationDirection = CameraTransform.forward;

        
    }

    public void RotationNormal()
    {
        if(!status.isAiming)
        {
            rotationDirection = moveDirection;

        }
        
        Vector3 targetDir = rotationDirection;
        targetDir.y = 0;

        if(targetDir == Vector3.zero)
        targetDir = transform.forward;

        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, 1);
        transform.rotation = targetRot;
        
    }
}
