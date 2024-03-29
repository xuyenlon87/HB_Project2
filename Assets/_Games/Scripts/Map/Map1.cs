﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.DrawmFloor(Vector3.zero, 10, 10, 25, LevelManager.Instance.map1.transform);
        LevelManager.Instance.SpawmPlayer(-4, 4, -4, 4, LevelManager.Instance.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.wave2)
        {
            LevelManager.Instance.wave2 = false;
            Destroy(GameObject.FindGameObjectWithTag("Wave2"));
            LevelManager.Instance.DrawmFloor(new Vector3(0, 1.9f, 32f), 10, 10, 25, LevelManager.Instance.map1.transform);
        }
    }
}
