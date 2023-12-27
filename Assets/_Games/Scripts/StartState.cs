using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    private float timer = 0;
    public void OnEnter(GameManager gameManager)
    {
        LevelManager.Instance.DrawmF1();
        LevelManager.Instance.DrawmG1();
        LevelManager.Instance.SpawmPlayer();
        LevelManager.Instance.DrawBridgeF1();
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
