using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float RunSpeed = 5f;

    private PlayerMotor motor;
    private CamMoving camMoving;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        camMoving = GetComponent<CamMoving>();
    }

    private void Update()
    {
        float _movX = Input.GetAxisRaw("Horizontal");
        float _movY = Input.GetAxisRaw("Vertical");
        Vector3 _moveDir = transform.right * _movX + transform.forward * _movY;
        Vector3 _velocity;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _velocity = _moveDir.normalized * RunSpeed;
        }
        else
        {
            _velocity = _moveDir.normalized * speed;
        }
        motor.applyVelovity(_velocity);
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            camMoving.Stop();
        }
        if (Input.GetKey(KeyCode.LeftShift)&&_movY>float.Epsilon)
        {
            camMoving.Play();
        }
        else camMoving.Stop();
        motor.applyVelovity(_velocity);
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 PlayerRotation = new Vector3(0, _yRot, 0);
        motor.applyRotation(PlayerRotation);
    }
}
