using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTutorial : MonoBehaviour
{
    public AudioSource bgmAudioSource; // BGM�p��AudioSource
    public AudioSource seAudioSource;  // SE�p��AudioSource

    public AudioClip bgmClip;          // BGM�̉���
    public AudioClip gameOverClip;     // �Q�[���I�[�o�[����SE
    public AudioClip correctSE;    // ��������SE
    public AudioClip wrongSE;      // �ԈႦ������SE

    // Start is called before the first frame update
    void Start()
    {
        // BGM�����[�v�Đ�
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void PlayGameOverSE()
    {
        // �Q�[���I�[�o�[����SE���Đ�
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
            seAudioSource.Stop(); // �O��SE���~�߂�
        }
        seAudioSource.clip = clip;
        seAudioSource.Play();
    }
}
