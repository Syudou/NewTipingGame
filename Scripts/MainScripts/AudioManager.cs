using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmAudioSource; // BGM用のAudioSource
    public AudioSource seAudioSource;  // SE用のAudioSource
    public AudioClip bgmClip;          // BGMの音源
    public AudioClip gameOverClip;     // ゲームオーバー時のSE

    // Start is called before the first frame update
    void Start()
    {
        // BGMをループ再生
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void PlayGameOverSE()
    {
        // ゲームオーバー時のSEを再生
        seAudioSource.PlayOneShot(gameOverClip);
    }
}
