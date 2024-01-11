using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPool : MonoBehaviour
{
    public Transform player;
    public Bullet1 prefabBullet;
    MiniPool<Bullet1> miniPool;
    private float timer = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        miniPool = new MiniPool<Bullet1>();
        miniPool.OnInit(prefabBullet, 5);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            timer = 0;
            prefabBullet = miniPool.Spawn(player.position + Vector3.one, Quaternion.identity, null);
        }
        if (timer >= 3f)
        {
            miniPool.Despawn(prefabBullet);
        }
    }
}
