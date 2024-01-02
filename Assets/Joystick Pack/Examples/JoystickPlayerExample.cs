using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    [SerializeField] LayerMask ground;
    public float speed;
    public FixedJoystick fixedJoystick;
    public Rigidbody rb;
    private RaycastHit hit;
    private Vector3 targetPos;
    private int i = 1;




    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        if (fixedJoystick.Vertical < 0 && fixedJoystick.Horizontal < 0)
        {
            i *= -1;
        }
        else
        {
            i *= 1;
        }
        targetPos = new Vector3(transform.position.x + 0.5f * i, transform.position.y, transform.position.z + 0.5f * i);
        if (Physics.Raycast(targetPos, Vector3.down, out hit, 10f, ground))
        {
            Debug.DrawRay(targetPos, Vector3.down * hit.distance, Color.black, 10f);
            Debug.Log("Did Hit");
        }
        rb.velocity = direction * speed;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
    
}