using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBrickState : IState<Bot>
{
    private int currentTargetIndex = 0;
    public void OnEnter(Bot bot)
    {
        SetNextTarget(bot);
    }

    public void OnExecute(Bot bot)
    {
        if (!bot.navMesh.pathPending && bot.navMesh.remainingDistance < 0.1f)
        {
            SetNextTarget(bot);
        }
        if(bot.listBackBrick.Count > 10)
        {
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {

    }

    private void SetNextTarget(Bot bot)
    {
        if (currentTargetIndex < LevelManager.Instance.listPositionBrick.Count)
        {
            bot.navMesh.SetDestination(LevelManager.Instance.listPositionBrick[currentTargetIndex]);
            currentTargetIndex++;
        }
    }
}
