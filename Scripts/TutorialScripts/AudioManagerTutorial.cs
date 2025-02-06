using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTutorial : MonoBehaviour
{
    public AudioSource bgmAudioSource; // BGM用のAudioSource
    public AudioSource seAudioSource;  // SE用のAudioSource

    public AudioClip bgmClip;          // BGMの音源
    public AudioClip gameOverClip;     // ゲームオーバー時のSE
    public AudioClip correctSE;    // 正解時のSE
    public AudioClip wrongSE;      // 間違えた時のSE

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
    public void PlayCorrectSE()
    {
        PlaySE(correctSE);
    }

    public void PlayWrongSE()
    {
        PlaySE(wrongSE);
    }

    private void PlaySE(AudioClip clip)
    {
        if (seAudioSource.isPlaying)
        {
            seAudioSource.Stop(); // 前のSEを止める
        }
        seAudioSource.clip = clip;
        seAudioSource.Play();
    }
}
