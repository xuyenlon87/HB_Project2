using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject back;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask bridge;
    [SerializeField] private Material ColorBrick;
    [SerializeField] private List<GameObject> listBackBrick = new List<GameObject>();

    public float speed;
    public FixedJoystick fixedJoystick;
    public Rigidbody rb;

    private RaycastHit hit;
    private Vector3 posYBack = Vector3.zero;
    private Vector3 targetPos;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        fixedJoystick = LevelManager.Instance.fixedJoystick;
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
        targetPos = transform.position + transform.forward * 0.5f;
        Physics.Raycast(targetPos, Vector3.down, out hit, 10f);
        if (hit.collider)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Bridge"))
            {
                if (listBackBrick.Count <= 0)
                {
                    rb.velocity = Vector3.zero;
                    targetPos = transform.position - transform.forward * 0.5f;
                }
            }
            else
            {
                direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
                rb.velocity = direction * speed;
            }
        }
        if (!hit.collider)
        {
            rb.velocity = Vector3.zero;
            targetPos = transform.position - transform.forward * 0.5f;
        }
        if (rb.velocity.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        Debug.DrawRay(targetPos, Vector3.down * hit.distance, Color.black, 10f);
        Debug.Log(direction);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlueBrick"))
        {
            LevelManager.Instance.listSpawmBrick.Add(other.transform.position);
            listBackBrick.Add(other.gameObject);
            other.transform.parent = back.transform;
            other.transform.localRotation = Quaternion.identity;
            other.transform.localPosition = new Vector3(0, posYBack.y , 0);
            posYBack.y += 0.1f;
            other.GetComponent<Renderer>().material = ColorBrick;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BridgeBrick"))
        {
            Renderer renderer = collision.collider.GetComponent<Renderer>();
            if (renderer.material != ColorBrick)
            {
                if (listBackBrick.Count > 0)
                {
                    renderer.material = ColorBrick;
                    collision.gameObject.layer = LayerMask.NameToLayer("Ground");
                    collision.gameObject.tag = "GroundBrick";
                    var del = listBackBrick.Count - 1;
                    Destroy(listBackBrick[del]);
                    listBackBrick.RemoveAt(del);
                    posYBack.y -= 0.1f;
                }
            }
        }
    }
}
