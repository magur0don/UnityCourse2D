using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStartState : IState
{
    public void Enter()
    {
        
        InGameStateManager.Instance.GameState = InGameStateManager.GameStateProcessor.START;
        InGameStateManager.Instance.StateMachine.SetState(InGameStateManager.GameStateProcessor.GAMEMAIN);
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}
