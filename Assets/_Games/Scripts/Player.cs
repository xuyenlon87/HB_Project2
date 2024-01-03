using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject back;
    [SerializeField] List<GameObject> brickOnPlayer;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask bridge;
    [SerializeField] private Material ColorBrick;

    public float speed;
    public FixedJoystick fixedJoystick;
    public Rigidbody rb;

    private List<GameObject> listBackBrick = new List<GameObject>();
    private Renderer rend;
    private RaycastHit hit;
    private Vector3 targetPos;
    private int i = 1;


    private Vector3 posYBack = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        fixedJoystick = LevelManager.Instance.fixedJoystick;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f, bridge))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.black, 10f);
            if (hit.collider.GetComponent<Renderer>().material != ColorBrick)
            {
                if (listBackBrick.Count > 0)
                {
                    hit.collider.GetComponent<Renderer>().material = ColorBrick;
                    Debug.Log(hit.collider.GetComponent<Renderer>().material);
                }
                else
                {
                    rb.velocity = Vector3.zero;
                }
            }
            Debug.DrawRay(targetPos, Vector3.down * hit.distance, Color.black, 10f);
            Debug.Log("Did Hit");
        }
    }
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;       
        rb.velocity = direction * speed;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlueBrick"))
        {
            brickOnPlayer.Add(other.gameObject);
            LevelManager.Instance.listSpawmBrick.Add(other.transform.position);
            listBackBrick.Add(other.gameObject);
            other.transform.parent = back.transform;
            other.transform.localRotation = Quaternion.identity;
            other.transform.localPosition = new Vector3(0, posYBack.y , 0);
            posYBack.y += 0.1f;
            other.GetComponent<Renderer>().material = ColorBrick;
        }
    }
}
