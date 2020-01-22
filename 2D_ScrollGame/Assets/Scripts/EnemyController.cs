using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// アニメーターのコントローラー
    /// </summary>
    private CharacterAnimatorControl m_characterAnimatorControl;

    /// <summary>
    /// 敵がプレイヤーを探す範囲
    /// </summary>
    private Transform m_playerSearchRange;

    /// <summary>
    /// 移動量
    /// </summary>
    private float m_MoveSpeed = 0.0f;

    /// <summary>
    /// 敵の行動を決める時間
    /// </summary>
    private float m_enemyMoveTime = 5.0f;

    /// <summary>
    /// 右に動く判定
    /// </summary>
    private bool m_rightMove = true;

    // Start is called before the first frame update
    void Start()
    {
        m_characterAnimatorControl = GetComponent<CharacterAnimatorControl>();
        m_playerSearchRange = transform.Find("PlayerSearchRange");
    }

    // Update is called once per frame
    void Update()
    {
        m_enemyMoveTime -= Time.deltaTime;

        if (m_enemyMoveTime > 0)
        {
            if (m_rightMove)
            {
                m_MoveSpeed = 0.2f;
            }
            else
            {
                m_MoveSpeed = -0.2f;
            }
        }
        else
        {
            if (m_rightMove)
            {
                m_rightMove = false;
            }
            else
            {
                m_rightMove = true;
            }

            m_enemyMoveTime = 5.0f;
        }
    }

    PlayerController player = null;

    private void FixedUpdate()
    {
        //範囲の判定を円にしてキャストしたColliderがプレイヤーだったらそっちに行く
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_playerSearchRange.position, 5.0f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Player")
            {
                if (player == null)
                {
                    player = colliders[i].gameObject.GetComponent<PlayerController>();
                }

                if (!player.m_damaged)
                {

                    //自分よりPlayerが左にいた場合は
                    if (colliders[i].gameObject.transform.position.x < this.transform.position.x)
                    {
                        m_MoveSpeed = -0.4f;
                    }
                    else
                    {
                        m_MoveSpeed = 0.4f;
                    }
                }
            }
        }
        m_characterAnimatorControl.Move(m_MoveSpeed, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("GroundCheck"))
        {
            gameObject.SetActive(false);
        }
    }

}
