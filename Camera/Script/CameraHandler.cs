using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform camTrans;
    public Transform pivot;
    public Transform Status;
    public Transform mTransform;

    public Status status;
    public CameraConfiguration cameraconfiguration;
    public bool leftPivot;
    public float delta;

    public float mouseX;
    public float mouseY;
    public float smoothX;
    public float smoothY;
    public float smoothXVelocity;
    public float smoothYVelocity;
    public float lookAngle;
    public float titAngle;

    void FixedUpdate()
    {
        FixedTick();

    }


    void FixedTick()
    {
        delta = Time.deltaTime;

        HandlePosition();
        HandleRotation();

        Vector3 targetPosition = Vector3.Lerp(mTransform.position, Status.position,1);
        mTransform.position = targetPosition;


    }

    void HandlePosition()
    {
        float targetX = cameraconfiguration.normalX;
        float targetY = cameraconfiguration.normalY;
        float targetZ = cameraconfiguration.normalZ;


        if(status.isAiming)
        {
            targetX = cameraconfiguration.aimX;
            targetZ = cameraconfiguration.aimZ;
        }

        if(leftPivot)
        {
            targetX = -targetX;
        }

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = camTrans.localPosition;
        newCameraPosition.z = targetZ;
        float t = delta * cameraconfiguration.pivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, newCameraPosition, t);
    }

    void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if(cameraconfiguration.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX,mouseX, ref smoothXVelocity, cameraconfiguration.turnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY,mouseY, ref smoothYVelocity, cameraconfiguration.turnSmooth);
        }


        else
        {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        lookAngle += smoothX * cameraconfiguration.Y_rot_speed;
        Quaternion targetRot = Quaternion.Euler(0, lookAngle, 0);
        mTransform.rotation = targetRot;

        titAngle -= smoothY * cameraconfiguration.Y_rot_speed;
        titAngle = Mathf.Clamp(titAngle,cameraconfiguration.minAngle, cameraconfiguration.minAngle);
        pivot.localRotation = Quaternion.Euler(titAngle,0,0);

    }

}

