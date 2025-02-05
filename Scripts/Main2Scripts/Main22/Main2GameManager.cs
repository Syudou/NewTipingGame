using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main2GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // スコア表示用
    private int score = 0;  // スコア

    public AudioSource bgmAudioSource;  // BGM用オーディオソース
    public AudioClip shootingSE;  // 弾を撃つSE
    public AudioClip wordDestroyedSE;  // 単語が消えるSE

    void Start()
    {
        bgmAudioSource.Play();  // BGMを再生
    }

    void Update()
    {
        // スコアを更新
        scoreText.text = "Score: " + score;
    }

    // スコアを加算
    public void AddScore(int points)
    {
        score += points;
        PlaySE(wordDestroyedSE);  // 単語消去のSEを再生
    }

    // ゲームオーバー処理（例）
    public void GameOver()
    {
        // ゲームオーバー処理（ここにゲームオーバーの処理を追加）
        Debug.Log("Game Over!");
    }

    // SE再生
    public void PlaySE(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    // 弾を撃つSE
    public void PlayShootingSE()
    {
        PlaySE(shootingSE);
    }
}

