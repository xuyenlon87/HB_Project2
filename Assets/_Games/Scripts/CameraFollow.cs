using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LevelManager.Instance.playerClone != null)
        {
            transform.position = Vector3.Lerp(transform.position, LevelManager.Instance.playerClone.transform.position + offset, Time.deltaTime * speed);
        }
    }
}
