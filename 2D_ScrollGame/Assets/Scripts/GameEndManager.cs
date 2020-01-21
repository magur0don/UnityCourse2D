using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public TextMeshProUGUI ResultText;

    public bool GameOver = false;

    void Start()
    {
        AssetBundle.UnloadAllAssetBundles(true);

        if (GameOver)
        {
            ResultText.text = "GameOver";
        }
        else
        {
            ResultText.text = "GameClear";
        }
    }

    public void GameEndButton()
    {
        SceneManager.LoadScene("GameStart");
    }
}
