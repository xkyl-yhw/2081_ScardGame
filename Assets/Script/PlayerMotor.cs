using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Rigidbody PlayerRig;
    private Vector3 velocity = Vector3.zero;
    private Vector3 Rotation = Vector3.zero;

    private void Start()
    {
        PlayerRig = GetComponent<Rigidbody>();
    }

    public void applyVelovity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void applyRotation(Vector3 _Rotation)
    {
        Rotation = _Rotation;
    }

    private void FixedUpdate()
    {
        performMoving();
        performRotation();
    }
    
    private void performMoving()
    {
        if(velocity!=Vector3.zero)
        PlayerRig.MovePosition(transform.position+velocity*Time.deltaTime);
    }

    private void performRotation()
    {
        if (Rotation != Vector3.zero)
            PlayerRig.MoveRotation(transform.rotation*Quaternion.Euler(Rotation));
    }
}
