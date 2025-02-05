using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileController_2 : MonoBehaviour
{

    //public static event Action<WordController_2, ProjectileController_2> OnProjectileHitWord; // イベント定義
    private WordController_2 target; // ターゲットの文字
    public float speed = 10f; // 弾の移動速度

    public AudioManager_2 audioManager; // AudioManagerを参照

    public void Initialize(WordController_2 targetWord)
    {
        target = targetWord; // ターゲットを設定
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            // ターゲットを破壊しスコアを加算
            GameManager_2 gameManager = FindObjectOfType<GameManager_2>();
            if (gameManager != null)
            {
                gameManager.IncreaseScore(10); // スコアを加算
            }

            Destroy(target.gameObject);
            Destroy(gameObject);
            AudioManager_2 audioManager = FindObjectOfType<AudioManager_2>();
            audioManager.PlayhitSE();

        }

        

}

   

   
        

     


    }
