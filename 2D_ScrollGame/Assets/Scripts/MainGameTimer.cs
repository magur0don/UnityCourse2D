using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainGameTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TimerText;

    [SerializeField]
    private float m_TimeLimit = 60f;
    
    // Update is called once per frame
    void Update()
    {
        m_TimerText.text = string.Format("{0:00}", Mathf.Clamp(m_TimeLimit -= Time.deltaTime, 0, m_TimeLimit));
    }
}
