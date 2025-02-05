using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileController : MonoBehaviour
{
    public static event Action<WordController, ProjectileController> OnProjectileHitWord; // イベント定義

    private WordController targetWord; // ターゲットの文字
    public float speed = 10f; // 弾の移動速度

    //public AudioClip hitSound; // 弾が文字に当たったときの効果音
    //private AudioSource audioSource;

    //void Awake()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //}
    public void Initialize(WordController target)
    {
        targetWord = target; // ターゲットを設定
    }

    void Update()
    {
        if (targetWord == null)
        {
            // ターゲットが既に破壊されていたら弾も消す
            Destroy(gameObject);
            return;
        }

        // ターゲットに向かって移動
        Vector3 targetPosition = targetWord.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ターゲットに到達したら
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {

            // イベント発行
            OnProjectileHitWord?.Invoke(targetWord, this);

            // 衝突対象と弾を破壊
            Destroy(targetWord.gameObject);
            Destroy(gameObject);
        }



    }






}
