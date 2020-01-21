using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameInitState : IState
{
    private InGameStateManager stateManager = InGameStateManager.Instance;

    public void Enter()
    {
        InGameStateManager.Instance.GameState = InGameStateManager.GameStateProcessor.START;
        InGameStateManager.Instance.StateMachine.SetState(InGameStateManager.GameStateProcessor.START);
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}
