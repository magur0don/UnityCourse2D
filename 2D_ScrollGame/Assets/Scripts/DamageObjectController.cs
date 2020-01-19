using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObjectController : MonoBehaviour
{
    public enum DamageObjectType
    {
        Invalid = -1,
        Spike,
        Poison
    }

    /// <summary>
    /// ダメージオブジェクトタイプ
    /// </summary>
    public DamageObjectType DamageObject = DamageObjectType.Invalid;

    private PlayerController m_playerController;

    /// <summary>
    /// 接触系のダメージ床
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && DamageObject == DamageObjectType.Spike)
        {
            if (m_playerController == null)
            {
                m_playerController = collision.gameObject.GetComponent<PlayerController>();
            }
            m_playerController.Damage();
        }
    }

    /// <summary>
    /// 侵入系のダメージ床
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && DamageObject == DamageObjectType.Poison)
        {
            if (m_playerController == null)
            {
                m_playerController = collision.gameObject.GetComponent<PlayerController>();
            }
            m_playerController.Damage();
        }
    }
}
