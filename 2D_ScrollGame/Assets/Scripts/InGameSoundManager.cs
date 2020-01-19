using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSoundManager : MonoBehaviour
{
    /// <summary>
    /// SoundManagerのInstance
    /// </summary>
    public static InGameSoundManager Instance;

    /// <summary>
    /// AudioSource
    /// </summary>
    private List<AudioSource> m_AudioSources = new List<AudioSource>();

    /// <summary>
    /// SEとかBGMとかのDictionary
    /// </summary>
    private Dictionary<string, AudioClip> m_AudioDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        Instance = this;

        var soundAssetBundle = AssetBundle.LoadFromFile("AssetBundles/StandaloneWindows/sounds/se");

        var soundAssets = soundAssetBundle.LoadAllAssets<AudioClip>();

        foreach (var sound in soundAssets)
        {
            m_AudioDictionary.Add(sound.name, sound);
        }

        foreach (var audioSource in this.GetComponents<AudioSource>())
        {
            m_AudioSources.Add(audioSource);
        }
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="_seName"></param>
    public void PlaySE(string _seName)
    {
        // 0番目のAudioSourceはBGM再生用にするので1から
        for (int i = 1; i < m_AudioSources.Count; i++)
        {
            if (!m_AudioSources[i].isPlaying)
            {
                m_AudioSources[i].PlayOneShot(m_AudioDictionary[_seName]);
                break;
            }
        }
    }

    /// <summary>
    /// BGMの再生
    /// </summary>
    /// <param name="_bgmName"></param>
    public void PlayBGM(string _bgmName)
    {
        m_AudioSources[0].clip = m_AudioDictionary[_bgmName];
        m_AudioSources[0].Play();
    }
}
