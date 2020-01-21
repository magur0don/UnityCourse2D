using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainGameTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TimerText;

    // Update is called once per frame
    void Update()
    {
        if (InGameStateManager.Instance.GameState == InGameStateManager.GameStateProcessor.GAMEMAIN)
        {
            m_TimerText.text = string.Format("{0:00}", Mathf.Clamp(InGameStateManager.Instance.GameTime -= Time.deltaTime, 0, InGameStateManager.Instance.GameTime));
        }
    }
}
