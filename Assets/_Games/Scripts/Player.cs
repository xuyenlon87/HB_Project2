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
    // Start is called before the first frame update
    void Start()
    {
        fixedJoystick = LevelManager.Instance.fixedJoystick;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        Vector3 targetPos = transform.position + transform.forward * 0.5f;
        if (Physics.Raycast(targetPos, Vector3.down, out hit, 10f, bridge))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            Debug.DrawRay(targetPos, Vector3.down * hit.distance, Color.black, 10f);
            if (renderer.material != ColorBrick)
            {
                if (listBackBrick.Count > 0)
                {
                    renderer.material = ColorBrick;
                    Debug.Log(renderer);
                }
            }
        }

        rb.velocity = direction * speed;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
    public void FixedUpdate()
    {

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
            if (renderer != ColorBrick)
            {
                Destroy(listBackBrick[listBackBrick.Count - 1]);
                listBackBrick.RemoveAt(listBackBrick.Count - 1);
                posYBack.y -= 0.1f;
            }
        }
    }
}
