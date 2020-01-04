using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// ジャンプ判定
    /// </summary>
    private bool m_Jump = false;

    /// <summary>
    /// アニメーターのコントローラー
    /// </summary>
    private CharacterAnimatorControl m_characterAnimatorControl;

    float m_MoveSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_characterAnimatorControl = GetComponent<CharacterAnimatorControl>();
    }

    // Update is called once per frame
    void Update()
    {
        m_MoveSpeed = Input.GetAxis("Horizontal");
        m_Jump = Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        m_characterAnimatorControl.Move(m_MoveSpeed, m_Jump);
        m_Jump = false;
    }
}
