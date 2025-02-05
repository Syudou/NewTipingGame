using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action<int> OnPlayerTakeDamage; // プレイヤーダメージイベント

    public int maxHP = 100; // プレイヤーの最大HP
    private int currentHP;
    public AudioClip damageSound; // ダメージを受けたときの効果音
    private AudioSource audioSource;

    

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP; // HPを初期化
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Word"))
        {
            // ダメージイベントを発行
            OnPlayerTakeDamage?.Invoke(10); // ダメージ量を通知

            // 効果音を再生
            if (audioSource != null && damageSound != null)
            {
                audioSource.PlayOneShot(damageSound);
            }

            // 文字を破壊
            Destroy(collision.gameObject);
        }
    }

    
}
