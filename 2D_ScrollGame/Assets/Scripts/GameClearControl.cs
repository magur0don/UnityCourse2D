using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("aaaaaaaaaa");
        InGameStateManager.Instance.GameOver = false;
        InGameStateManager.Instance.StateMachine.SetState(InGameStateManager.GameStateProcessor.RESULT);
    }
}
