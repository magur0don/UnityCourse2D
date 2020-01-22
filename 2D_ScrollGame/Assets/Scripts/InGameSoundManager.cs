using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

    AssetBundle soundAssetBundle;

    private void Awake()
    {
        Instance = this;

        StartCoroutine(GetSound());

    }

    public string AssetName;
    
    string path = "https://relaxed-hamilton-bf0135.netlify.com/WebGL/sounds/se";
    IEnumerator GetSound()
    {
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle(path))
        {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                soundAssetBundle = DownloadHandlerAssetBundle.GetContent(uwr);
            }
        }

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
