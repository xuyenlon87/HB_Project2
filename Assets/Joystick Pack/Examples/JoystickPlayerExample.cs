using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;
    public Rigidbody rb;


    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        Debug.Log(fixedJoystick.Vertical + " " + fixedJoystick.Horizontal);
        rb.velocity = direction * speed ;
        transform.rotation = Quaternion.LookRotation(rb.velocity);

    }
}