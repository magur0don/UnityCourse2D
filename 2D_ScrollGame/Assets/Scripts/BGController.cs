using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    /// <summary>
    /// カメラ
    /// </summary>
    [SerializeField]
    private Camera m_camera;

    /// <summary>
    /// 複製するBG
    /// </summary>
    [SerializeField]
    private SpriteRenderer bgSprite;

    /// <summary>
    /// オフセット
    /// </summary>
    Vector3 offset = Vector3.zero;

    /// <summary>
    /// BGのPosition
    /// </summary>
    Vector3 bgSpritePos = Vector3.zero;

    /// <summary>
    /// 動的に生成したBG
    /// </summary>
    private SpriteRenderer instantiateBgSprite;
    
    // Update is called once per frame
    void Update()
    {
        //カメラの位置がオフセットを右に越えた場合
        if (m_camera.transform.position.x > offset.x + (bgSprite.size.x / 8))
        {
            offset.x = bgSprite.size.x;
            bgSpritePos = bgSprite.gameObject.transform.position;

            if (instantiateBgSprite != null)
            {
                bgSpritePos = instantiateBgSprite.gameObject.transform.position;
            }

            bgSpritePos.x += (offset.x + bgSprite.size.x);
            offset.x = bgSpritePos.x;
            instantiateBgSprite = Instantiate(bgSprite, bgSpritePos, Quaternion.identity, this.gameObject.transform);

            if (this.gameObject.transform.childCount > 2)
            {
                Destroy(this.gameObject.transform.GetChild(0).gameObject);
            }
        }//カメラの位置がオフセットを左に越えた場合
        else if (m_camera.transform.position.x < offset.x - (bgSprite.size.x / 8))
        {
            offset.x = bgSprite.size.x;
            bgSpritePos = bgSprite.gameObject.transform.position;

            if (instantiateBgSprite != null)
            {
                bgSpritePos = instantiateBgSprite.gameObject.transform.position;
            }

            bgSpritePos.x -= (offset.x + bgSprite.size.x);
            offset.x = bgSpritePos.x;
            instantiateBgSprite = Instantiate(bgSprite, bgSpritePos, Quaternion.identity, this.gameObject.transform);

            if (this.gameObject.transform.childCount > 2)
            {
                Destroy(this.gameObject.transform.GetChild(0).gameObject);
            }
        }
    }
}
