using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private float m_MoveSpeed = 0.0f;

    [SerializeField]
    private float maxHp = 3;

    [SerializeField]
    private float hp = 3f;

    [SerializeField]
    private Image m_HpGauge;

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

    /// <summary>
    /// 敵に当たったらダメージを受ける
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            hp--;
            m_HpGauge.fillAmount = hp / maxHp;
            // Hpが0を下回ったらGameObjectを消す
            if (hp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
