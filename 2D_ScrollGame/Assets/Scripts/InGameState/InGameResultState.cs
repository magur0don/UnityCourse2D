using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameResultState : IState
{
    public void Enter()
    {
        Debug.Log("aaaaaaa");
        InGameStateManager.Instance.GameState = InGameStateManager.GameStateProcessor.RESULT;
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
        gameManager.GameOver = InGameStateManager.Instance.GameOver;

        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
