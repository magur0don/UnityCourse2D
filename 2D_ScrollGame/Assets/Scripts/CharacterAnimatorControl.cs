using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorControl : MonoBehaviour
{
    /// <summary>
    /// 最高速度
    /// </summary>
    [SerializeField]
    private float m_MaxSpeed = 10f;

    /// <summary>
    /// ジャンプする力
    /// </summary>
    [SerializeField]
    private float m_JumpForce = 400;

    /// <summary>
    /// 接地のチェック
    /// </summary>
    private Transform m_GroundCheck;

    /// <summary>
    /// 接地判定用の半径
    /// </summary>
    const float k_GroundedRadius = 0.2f;

    /// <summary>
    /// 地面にいるかどうか
    /// </summary>
    private bool m_Grounded;

    /// <summary>
    /// キャラクターのアニメーター
    /// </summary>
    private Animator m_Anim;

    /// <summary>
    /// キャラクターのRigidBody2D
    /// </summary>
    private Rigidbody2D m_Rigidbody2D;

    /// <summary>
    /// 右向きかどうか
    /// </summary>
    private bool m_FacingRight = true;

    private void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        //接地判定を円にしてキャストしたColliderが地面だったら接地とする
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != this.gameObject)
            {
                m_Grounded = true;
            }
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // 垂直方向に数値を適用する
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }

    /// <summary>
    /// 空中か否か
    /// </summary>
    private bool m_Aerial = false;

    public void Move(float move, bool jump)
    {
        if (m_Grounded)
        {
            //スピードは絶対値を取得する
            m_Anim.SetFloat("Speed", Mathf.Abs(move));

            m_Aerial = false;
            
            // キャラクターに速度を与える
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

            // 入力が右、キャラクターが左だった場合はフリップする
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            // 入力が左、キャラクターが右だった場合はフリップする
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }

        if (!m_Grounded && jump && !m_Aerial)
        {
            // 入力が右、キャラクターが左だった場合はフリップする
            if (move > 0 && !m_FacingRight)
            {
                m_Rigidbody2D.velocity = -m_Rigidbody2D.velocity;
                Flip();
            }
            // 入力が左、キャラクターが右だった場合はフリップする
            else if (move < 0 && m_FacingRight)
            {
                m_Rigidbody2D.velocity = -m_Rigidbody2D.velocity;
                Flip();
            }
            m_Rigidbody2D.AddForce(new Vector2((move * m_MaxSpeed), m_JumpForce));
            m_Aerial = true;
        }

        if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        {
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    private void Flip()
    {
        // 右向きを解除
        m_FacingRight = !m_FacingRight;

        // localScalのxに-1をかけて反転する
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
