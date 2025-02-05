using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main2WordController : MonoBehaviour
{
    public string word;  // 表示する単語
    public string romaji;  // タイピングで必要なローマ字
    public float speed = 2.0f;  // 単語の移動速度

    private Main2GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<Main2GameManager>();
    }

    void Update()
    {
        // 単語をカメラに向かって進ませる
        transform.position += Vector3.back * speed * Time.deltaTime;

        // プレイヤーに到達した場合ゲームオーバー（例）
        if (transform.position.z <= 0)
        {
            gameManager.GameOver();
            Destroy(gameObject);
        }
    }

    // 弾と衝突したら消す
    public void DestroyWord()
    {
        gameManager.AddScore(10);  // スコア加算
        Destroy(gameObject);  // 単語オブジェクト削除
    }
}
