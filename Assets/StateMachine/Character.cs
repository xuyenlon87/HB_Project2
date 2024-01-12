using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public GameObject back;
    public Material colorPlayer;
    public List<GameObject> listBackBrick = new List<GameObject>();
    public Vector3 posYBack = Vector3.zero;
    public float speed = 5;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

  
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer.material.color == colorPlayer.color)
            {
                LevelManager.Instance.listSpawmBrickPosition.Add(other.transform.position);
                listBackBrick.Add(other.gameObject);
                other.transform.parent = back.transform;
                other.transform.localRotation = Quaternion.identity;
                other.transform.localPosition = new Vector3(0, posYBack.y, 0);
                posYBack.y += 0.1f;
                other.GetComponent<Renderer>().material = colorPlayer;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BridgeBrick"))
        {
            Renderer renderer = collision.collider.GetComponent<Renderer>();
            if (renderer.material != colorPlayer)
            {
                if (listBackBrick.Count > 0)
                {
                    renderer.material = colorPlayer;
                    collision.gameObject.layer = LayerMask.NameToLayer("Ground");
                    collision.gameObject.tag = "GroundBrick";
                    var del = listBackBrick.Count - 1;
                    Destroy(listBackBrick[del]);
                    listBackBrick.RemoveAt(del);
                    posYBack.y -= 0.1f;
                }
            }
        }
        if (collision.gameObject.CompareTag("Finish"))
        {

        }
    }
}
