using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBrickState : IState<Bot>
{
    private float checkRadius = 10f;
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

    }

    public void OnExit(Bot bot)
    {

    }

    private void SetNextTarget(Bot bot)
    {
        //Collider[] colliders = Physics.OverlapSphere(bot.transform.position, checkRadius, bot.brickLayer);
        Vector3 nearestBrickPosition = Vector3.zero;
        float minDistance = float.MaxValue;
        for (int i = 0; i < LevelManager.Instance.listPositionBrick.Count; i++)
        {
            if (LevelManager.Instance.listPositionBrick != null && LevelManager.Instance.listPositionBrick.Count > 0)
            {
                float distanceToBrick = Vector3.Distance(bot.transform.position, LevelManager.Instance.listPositionBrick[i]);
                minDistance = Mathf.Min(minDistance, distanceToBrick);
                if (distanceToBrick > minDistance)
                {
                    minDistance = distanceToBrick;
                    nearestBrickPosition = LevelManager.Instance.listPositionBrick[i];
                }
            }
        }
        bot.navMesh.SetDestination(nearestBrickPosition);
    }
}
