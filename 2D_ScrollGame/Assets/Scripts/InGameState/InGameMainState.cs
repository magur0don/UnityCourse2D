using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMainState : IState
{
    public void Enter()
    {

        InGameStateManager.Instance.GameState = InGameStateManager.GameStateProcessor.GAMEMAIN;
    }

    public void Exit()
    {
    }

    public void Update()
    {
        if (InGameStateManager.Instance.GameTime <= 0)
        {
            InGameStateManager.Instance.GameOver = true;
            InGameStateManager.Instance.StateMachine.SetState(InGameStateManager.GameStateProcessor.RESULT);
        }
    }
}
