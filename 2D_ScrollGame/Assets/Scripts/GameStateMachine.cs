using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine<T>
{
    /// <summary>
    /// TにはEnum、IStateにはIStateを継承したclassが入っている
    /// </summary>
    Dictionary<T, IState> StateTable = new Dictionary<T, IState>();

    /// <summary>
    /// 現在のIStateを継承したclass
    /// </summary>
    private IState m_currentState;

    /// <summary>
    /// ステートを追加します
    /// </summary>
    public void Add(T key, IState stateValue)
    {
        StateTable.Add(key, stateValue);
    }

    /// <summary>
    /// 現在のStateをセットする
    /// </summary>
    /// <param name="key"></param>
    public void SetState(T key)
    {
        if (m_currentState != null)
        {
            m_currentState.Exit();
        }
        m_currentState = StateTable[key];

        m_currentState.Enter();
    }

    /// <summary>
    /// 現在のステートを更新します
    /// </summary>
    public void Update()
    {
        if (m_currentState == null)
        {
            return;
        }
        m_currentState.Update();
    }

    /// <summary>
    /// StateTableをクリアし、初期化する
    /// </summary>
    public void Clear()
    {
        StateTable.Clear();
        m_currentState = null;
    }
}
