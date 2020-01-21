using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シングルトンクラスの基底クラス
/// </summary>
/// <typeparam name="T">継承先はSingletonBaseを継承してなくてはならないと制限をかける</typeparam>
public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    /// <summary>
    /// シングルトンクラスのinstance
    /// </summary>
    private static T instance = null;

    /// <summary>
    /// シングルトンクラスを外部から取得する
    /// </summary>
    public static T Instance
    {
        get
        {
            // もしインスタンスがなかった場合
            if (instance == null)
            {
                // typeでgameobjectを探してinstanceを定義する
                instance = (T)FindObjectOfType(typeof(T));

                // インスタンスがAddComponentされているオブジェクトが見つからなかった場合
                if (instance == null)
                {
                    // 空のゲームオブジェクトを作成
                    GameObject singleton = new GameObject();
                    // シングルトンの名前は継承先の名前(例：SoundMnager等)
                    singleton.name = typeof(T).ToString();
                    // 空のオブジェクトに継承先をAddComponentする
                    instance = singleton.AddComponent<T>();
                    // シーンの変遷で壊れないように、DontDestroyOnLoadをかける
                    //DontDestroyOnLoad(singleton);
                }

            }

            return instance;
        }
    }

    /// 継承先クラスのAwakeにoverrideを許可する
    protected virtual void Awake()
    {
        CheckInstance();
    }

    /// <summary>
    /// 初回起動時にインスタンスが登録されていなければ自身を登録する
    /// </summary>
    /// <returns></returns>
    protected bool CheckInstance()
    {

        if (instance == null)
        {
            instance = (T)this;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }

        Destroy(this.gameObject);
        return false;
    }
}
