using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.DrawmFloor(Vector3.zero, 10, 10, 25);
        LevelManager.Instance.DrawmFloor(new Vector3(0, 1.9f, 32f), 10, 10, 25);
        LevelManager.Instance.SpawmPlayer(-4, 4, -4, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
