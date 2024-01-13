using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.target = GameObject.FindGameObjectWithTag("Finish");
    }

    public void OnExecute(Bot bot)
    {
        bot.navMesh.SetDestination(bot.target.transform.position);
    }

    public void OnExit(Bot bot)
    {

    }

}
