using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_2 : MonoBehaviour
{
    public AudioSource bgmAudioSource; // BGM用のAudioSource
    public AudioSource seAudioSource;  // SE用のAudioSource
    public AudioClip bgmClip;          // BGMの音源
    public AudioClip gameOverClip;     // ゲームオーバー時のSE
    public AudioClip hitSound; // 効果音

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
    public void PlayhitSE()
    {
       
       seAudioSource.PlayOneShot(hitSound);
        
    }
}
