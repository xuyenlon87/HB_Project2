using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private List<Material> listMaterial = new List<Material>();
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        int random = Random.Range(0, listMaterial.Count);
        renderer.material = listMaterial[random];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
