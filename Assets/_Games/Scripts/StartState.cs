using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    private float timer = 0;
    public void OnEnter(GameManager gameManager)
    {

        LevelManager.Instance.DrawmFloor(25, Vector3.zero, 5, 7);
        LevelManager.Instance.DrawBridge(new Vector3(0f, 0.45f, 11f), 30);
        LevelManager.Instance.DrawBridge(new Vector3(10f, 0.45f, 11f), 30);
        //LevelManager.Instance.DrawmFloor(25, new Vector3(0f, 1.8f, 31f), 10, 10);
        LevelManager.Instance.SpawmPlayer(1, 8);
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
