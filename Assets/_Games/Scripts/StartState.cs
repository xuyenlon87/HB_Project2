using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    private float timer = 0;
    public void OnEnter(GameManager gameManager)
    {

        LevelManager.Instance.DrawmF1(25, Vector3.zero, 10, 10);
        LevelManager.Instance.DrawG1(Vector3.zero, 10);
        LevelManager.Instance.DrawBridgeF1(new Vector3(0f, 0.45f, 11f), 30);
        LevelManager.Instance.SpawmPlayer(1, 8);
        LevelManager.Instance.playerClone.GetComponent<Player>().enabled = false;
    }

    public void OnExecute(GameManager gameManager)
    {
        timer += Time.deltaTime;
        if(timer >= 1f)
        {
            GameManager.Instance.ChangeState(new PlayState());
        }
    }
    public void OnExit(GameManager gameManager)
    {

    }
}
