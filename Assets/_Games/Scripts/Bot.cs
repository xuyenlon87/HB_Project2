using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent navMesh;
    public GameObject target;
    private IState<Bot> currentState;
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new IdleState());
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
