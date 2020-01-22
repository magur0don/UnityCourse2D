using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameResultState : IState
{
    private bool gameOverResult = false;

    public void Enter()
    {
        InGameStateManager.Instance.GameState = InGameStateManager.GameStateProcessor.RESULT;
        gameOverResult = InGameStateManager.Instance.GameOver;
        SceneManager.sceneLoaded += GameSceneLoaded;
        SceneManager.LoadScene("GameEnd");
    }

    public void Exit()
    {
    }

    public void Update()
    {

    }

    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var gameManager = GameObject.Find("GameEndManager").GetComponent<GameEndManager>();
        
        // データを渡す処理
        gameManager.GameOver = gameOverResult;

        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
