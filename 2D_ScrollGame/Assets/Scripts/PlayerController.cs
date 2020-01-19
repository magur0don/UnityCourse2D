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
    private float maxHp = 10;

    [SerializeField]
    private float hp = 10f;

    [SerializeField]
    private Image m_HpGauge;

    private float m_incredibleTime = 1.0f;

    [SerializeField]
    public bool m_damaged = false;

    [SerializeField]
    private Transform m_StampNode;

    // Start is called before the first frame update
    void Start()
    {
        m_characterAnimatorControl = GetComponent<CharacterAnimatorControl>();

        m_StampNode = transform.Find("GroundCheck");

        m_characterAnimatorControl.m_Rigidbody2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
        m_MoveSpeed = Input.GetAxis("Horizontal");
        m_Jump = Input.GetKeyDown(KeyCode.Space);
        if (m_damaged)
        {
            m_incredibleTime -= Time.deltaTime;
            if (m_incredibleTime <= 0)
            {
                m_incredibleTime = 1.0f;
                m_damaged = false;
            }
        }
    }

    private void FixedUpdate()
    {
        m_characterAnimatorControl.Move(m_MoveSpeed, m_Jump);
        if (m_Jump)
        {
            InGameSoundManager.Instance.PlaySE("JumpSe");
        }
        m_Jump = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_StampNode.position, 0.1f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag.Equals("Enemy"))
            {
                InGameSoundManager.Instance.PlaySE("StampSe");
                Destroy(colliders[i].gameObject);
            }
        }
    }

    /// <summary>
    /// 敵に当たったらダメージを受ける
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Damage();
        }
    }

    public void Damage()
    {
        if (m_damaged)
        {
            return;
        }
        hp--;
        m_HpGauge.fillAmount = hp / maxHp;
        m_damaged = true;

        m_characterAnimatorControl.m_Anim.SetTrigger("Damage");

        // Hpが0を下回ったらGameObjectを消す
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
