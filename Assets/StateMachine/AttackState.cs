using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.target = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnExecute(Bot bot)
    {
        bot.navMesh.SetDestination(bot.target.transform.position);
        if (!bot.navMesh.pathPending && bot.navMesh.remainingDistance < 0.1f)
        {
            bot.ChangeState(new FindBrickState());
        }
    }

    public void OnExit(Bot bot)
    {

    }

}
