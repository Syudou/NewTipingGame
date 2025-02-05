using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main2AudioManager : MonoBehaviour
{
    public AudioSource bgmAudioSource;  // BGM用オーディオソース
    public AudioClip bgmClip;  // BGMのオーディオクリップ
    public AudioClip shootingSE;  // 弾を撃つSE
    public AudioClip wordDestroyedSE;  // 単語が消えるSE

    void Start()
    {
        // BGM再生
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    // 撃つSE
    public void PlayShootingSE()
    {
        AudioSource.PlayClipAtPoint(shootingSE, transform.position);
    }

    // 単語消去SE
    public void PlayWordDestroyedSE()
    {
        AudioSource.PlayClipAtPoint(wordDestroyedSE, transform.position);
    }
}

