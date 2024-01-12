using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        int random = Random.Range(0, LevelManager.Instance.listMaterial.Count - 1);
        renderer.material = LevelManager.Instance.listMaterial[random];
        if (random == 0)
        {
            LevelManager.Instance.listRedBrick.Add(gameObject);
        }
        else if (random == 1)
        {
            LevelManager.Instance.listYellowBrick.Add(gameObject);
        }
        else if (random == 2)
        {
            LevelManager.Instance.listGreenBrick.Add(gameObject);
        }
        else if (random == 3)
        {
            LevelManager.Instance.listBlueBrick.Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
