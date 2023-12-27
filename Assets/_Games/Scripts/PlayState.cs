using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : IState
{
    public void OnEnter(GameManager gameManager)
    {
        LevelManager.Instance.playerClone.GetComponent<Player>().enabled = true;
    }

    public void OnExecute(GameManager gameManager)
    {

    }
    public void OnExit(GameManager gameManager)
    {

    }
}
