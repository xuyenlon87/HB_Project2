using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject back;
    [SerializeField] List<GameObject> brickOnPlayer;

    private Vector3 posYBack = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlueBrick"))
        {
            brickOnPlayer.Add(other.gameObject);
            LevelManager.Instance.listSpawmBrick.Add(other.transform.position);
            other.transform.parent = back.transform;
            other.transform.localPosition = new Vector3(0, posYBack.y , 0);
            posYBack.y += 0.1f;
        }
        if (other.CompareTag("BlackBrick"))
        {

        }
    }
}
