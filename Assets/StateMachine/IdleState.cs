using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    private float timer;
    public void OnEnter(Bot bot)
    {
        timer = 0;
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if(timer > 1f)
        {
            bot.ChangeState(new FindBrickState());
        }
    }

    public void OnExit(Bot bot)
    {

    }

}
