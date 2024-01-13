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
        Collider[] colliders = Physics.OverlapSphere(bot.transform.position, checkRadius, bot.brickLayer);
        Vector3 nearestBrickPosition = Vector3.zero;
        float minDistance = float.MaxValue;
        for (int i = 0; i < colliders.Length; i++)
        {
            // Lấy Renderer từ Collider để truy cập material
            Renderer brickRenderer = colliders[i].GetComponent<Renderer>();
            if (brickRenderer != null)
            {
                Debug.Log("here");
                Debug.Log(bot.colorPlayer);
                Debug.Log(brickRenderer.material);
                if (brickRenderer.material == bot.colorPlayer)
                {

                    float distanceToBrick = Vector3.Distance(bot.transform.position, colliders[i].transform.position);
                    minDistance = Mathf.Min(minDistance, distanceToBrick);
                    if (distanceToBrick < minDistance)
                    {
                        minDistance = distanceToBrick;
                        nearestBrickPosition = colliders[i].transform.position;
                    }
                }
            }
            else
            {
                Debug.Log("null");
            }
        }

    }
}
