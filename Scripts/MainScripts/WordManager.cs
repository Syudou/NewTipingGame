﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordManager : MonoBehaviour
{
    public GameObject wordPrefab; // 文字のプレハブ (TextMeshPro)
    public float initialSpawnInterval = 2f; // 文字の最初の出現間隔
    public float spawnAcceleration = 0.98f; // 出現間隔の加速率（1未満の値）
    private float spawnInterval; // 現在の出現間隔

    public float initialWordSpeed = 2f; // 文字の移動速度
    public float speedIncreaseRate = 0.1f; // 速度の増加率
    private float wordSpeed; // 現在の移動速度

    private float timer;

    private List<GameObject> spawnedWords = new List<GameObject>();//リスト宣言

    void Start()
    {
        spawnInterval = initialSpawnInterval; // 出現間隔を初期化
        wordSpeed = initialWordSpeed; // 移動速度を初期化
    }

    void Update()
    {
        // ゲームが開始されていない、またはゲームオーバーの場合は動作しない
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (!gameManager.IsGameStarted() || gameManager.isGameOver) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnWord();
            timer = 0f;

            // 出現間隔を短縮し、最小値を設定
            spawnInterval = Mathf.Max(spawnInterval * spawnAcceleration, 0.5f);

            // 文字の移動速度を増加
            wordSpeed += speedIncreaseRate;
        }
    }

    void SpawnWord()
    {
        Camera cam = Camera.main;

        float x, y;

        // どの辺からスポーンするかをランダムに決める（0=左, 1=右, 2=上, 3=下）
        int edge = Random.Range(0, 4);

        if (edge == 0) // 左（Xがマイナス側に外れる）
        {
            x = -0.1f;
            y = Random.Range(-0.1f, 1.1f); // 上下のどこからでも出現
        }
        else if (edge == 1) // 右（Xがプラス側に外れる）
        {
            x = 1.1f;
            y = Random.Range(-0.1f, 1.1f);
        }
        else if (edge == 2) // 上（Yがプラス側に外れる）
        {
            y = 1.1f;
            x = Random.Range(-0.1f, 1.1f); // 左右のどこからでも出現
        }
        else // 下（Yがマイナス側に外れる）
        {
            y = -0.1f;
            x = Random.Range(-0.1f, 1.1f);
        }

        Vector3 spawnPosition = cam.ViewportToWorldPoint(new Vector3(x, y, cam.nearClipPlane));
        spawnPosition.z = 0; // Z位置を0に設定

        // ランダムな位置でWordPrefabを生成
        GameObject newWord = Instantiate(wordPrefab, spawnPosition, Quaternion.identity);
        spawnedWords.Add(newWord); // リストに追加

        // WordControllerを使って文字を設定
        newWord.GetComponent<WordController>().Initialize(GetRandomCharacter());

        // プレイヤーに向かう動きを設定
        newWord.GetComponent<WordController>().SetTarget(Vector3.zero, wordSpeed);
    }

    //void SpawnWord()
    //{
    //    Camera cam = Camera.main;

    //    // カメラ枠のすぐ外側に文字を生成
    //    float x = Random.Range(-0.1f, 1.1f); // カメラ枠の外（左右）
    //    float y = Random.Range(-0.1f, 1.1f); // カメラ枠の外（上下）

    //    // 確実に枠外にするための調整
    //    if (x > 0f && x < 1f) x = x > 0.5f ? 1.1f : -0.1f;
    //    if (y > 0f && y < 1f) y = y > 0.5f ? 1.1f : -0.1f;

    //    Vector3 spawnPosition = cam.ViewportToWorldPoint(new Vector3(x, y, cam.nearClipPlane));
    //    spawnPosition.z = 0; // Z位置を0に設定

    //    // ランダムな位置でWordPrefabを生成
    //    GameObject newWord = Instantiate(wordPrefab, spawnPosition, Quaternion.identity);
    //    spawnedWords.Add(newWord); // リストに追加

    //    // WordControllerを使って文字を設定
    //    newWord.GetComponent<WordController>().Initialize(GetRandomCharacter());

    //    // プレイヤーに向かう動きを設定
    //    newWord.GetComponent<WordController>().SetTarget(Vector3.zero, wordSpeed);
    //}

    char GetRandomCharacter()
    {
        // 大文字と小文字をランダムで生成
        if (Random.Range(0, 2) == 0) // 50%の確率で大文字
        {
            return (char)Random.Range(65, 91); // A〜Z
        }
        else // 50%の確率で小文字
        {
            return (char)Random.Range(97, 123); // a〜z
        }
    }

    // 指定された文字の中で最もプレイヤーに近いものを取得する
    public GameObject GetClosestWord(string targetCharacter)
    {
        GameObject closestWord = null;
        float minDistance = float.MaxValue;

        foreach (GameObject word in spawnedWords)
        {
            if (word != null)
            {
                WordController wordController = word.GetComponent<WordController>();

                // 指定の文字と一致するか確認
                if (wordController.character == targetCharacter)
                {
                    // プレイヤー(原点)からの距離を計算
                    float distance = Vector3.Distance(word.transform.position, Vector3.zero);

                    // 一番近いものを選択
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestWord = word;
                    }
                }
            }
        }
        return closestWord;
    }

    // すべての文字を消去
    public void ClearAllWords()
    {
        foreach (GameObject word in spawnedWords)
        {
            if (word != null)
            {
                Destroy(word);
            }
        }
        spawnedWords.Clear(); // リストをクリア
    }
}