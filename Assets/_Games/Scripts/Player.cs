using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask bridge;

    public FixedJoystick fixedJoystick;
    public Rigidbody rb;

    private RaycastHit hit;
    private Vector3 targetPos;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        fixedJoystick = UIManager.Ins.GetUI<GamePlay>().GetComponentInChildren<FixedJoystick>();
        colorPlayer = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void FixedUpdate()
    {


    }

    private void Move()
    {
        direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        targetPos = transform.position + direction * 0.5f;
        Physics.Raycast(targetPos, Vector3.down, out hit, 10f);
        if (hit.collider)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Bridge"))
            {
                if (listBackBrick.Count <= 0)
                {
                    rb.velocity = Vector3.zero;
                }
            }
            else
            {
                rb.velocity = direction * speed;
            }
        }
        if (!hit.collider)
        {
            rb.velocity = Vector3.zero;
            Debug.Log("no hit");
        }
        if (rb.velocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
   
}
