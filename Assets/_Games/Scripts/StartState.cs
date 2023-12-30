using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    private float timer = 0;
    public void OnEnter(GameManager gameManager)
    {

        LevelManager.Instance.DrawmFloor(new Vector3(12,0,10), 10, 10, 25); 
        LevelManager.Instance.DrawBridge(new Vector3(9f, 0.45f, 17f), 13);// x = x sàn 1 +- row/2; y cầu 1 = y sàn 1 - 0.5f; z cầu 1 = z sàn 1 + (col + 2) / 2 + 2;
        LevelManager.Instance.DrawBridge(new Vector3(16f, 0.45f, 17f), 13);
        LevelManager.Instance.DrawmFloor(new Vector3(12, 1.2f, 35f), 10, 10, 25);//y sàn 2 = (sizeBridge 1 lên 2) *0.1f - 0.1 + y sàn 1; z sàn 2 = size cầu 1 + z cầu 1 + col/2
        LevelManager.Instance.DrawBridge(new Vector3(13f, 1.65f, 42f), 13);//x = x sàn 2 +- row/2; y cầu 2 = y sàn 2 + y cầu 1;  z cầu 2 = z sàn 2 + (col + 2) / 2 + 2;
        LevelManager.Instance.DrawmFloor(new Vector3(12, 2.4f, 57.5f), 5, 5, 0);//y sàn 3 = (sizeBridge 2 lên 3) *0.1f - 0.1 + y sàn 2; z sàn 3 = size cầu 2 + z cầu 2 + col/2
        LevelManager.Instance.SpawmPlayer(8,17,6,14);//random trong khoảng x +- row/2 và z +- col/2 của sàn 1
        LevelManager.Instance.playerClone.GetComponent<Player>().enabled = false;
    }

    public void OnExecute(GameManager gameManager)
    {
        timer += Time.deltaTime;
        if(timer >= 2f)
        {
            GameManager.Instance.ChangeState(new PlayState());
        }
    }
    public void OnExit(GameManager gameManager)
    {

    }
}
