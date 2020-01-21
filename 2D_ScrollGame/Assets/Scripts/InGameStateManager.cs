using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InGameStateManager : SingletonBase<InGameStateManager>
{
    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum GameStateProcessor
    {
        INIT,       // 初期化
        START,      // スタート
        GAMEMAIN,   // ゲームメイン
        RESULT,     // ゲーム結果
    }

    /// ゲームの状態
    /// </summary>
    public GameStateProcessor GameState;

    /// <summary>
    /// ゲームオーバーフラグ
    /// </summary>
    public bool GameOver = false;

    /// <summary>
    /// ゲームの制限時間
    /// </summary>
    public float GameTime = 30f;
    
    public readonly GameStateMachine<GameStateProcessor> StateMachine = new GameStateMachine<GameStateProcessor>();

    protected override void Awake()
    {
        base.Awake();

        ///Statemachineの初期化
        StateMachine.Clear();
        StateMachine.Add(GameStateProcessor.INIT, new InGameInitState());
        StateMachine.Add(GameStateProcessor.START, new InGameStartState());
        StateMachine.Add(GameStateProcessor.GAMEMAIN, new InGameMainState());
        StateMachine.Add(GameStateProcessor.RESULT, new InGameResultState());
    }

    private void Start()
    {
        StateMachine.SetState(GameStateProcessor.INIT);
    }

    private void Update()
    {
        StateMachine.Update();
    }
}




