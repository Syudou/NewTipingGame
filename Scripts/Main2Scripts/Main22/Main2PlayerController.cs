using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main2PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;  // 弾のプレハブ
    public Transform bulletSpawnPoint;  // 弾の発射位置

    private string currentInput = "";  // ユーザーの現在の入力

    void Update()
    {
        // キーボード入力を受け取る
        foreach (char c in Input.inputString)
        {
            currentInput += c;

            // 現在の入力を持つ単語を探す
            Main2WordController[] words = FindObjectsOfType<Main2WordController>();
            foreach (Main2WordController word in words)
            {
                if (word.romaji.StartsWith(currentInput))
                {
                    if (word.romaji == currentInput)
                    {
                        // 単語を完成させる
                        ShootBullet(word);
                        currentInput = "";  // 入力をリセット
                    }
                    return;
                }
            }
        }
    }

    void ShootBullet(Main2WordController targetWord)
    {
        // 弾を生成して発射
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Main2BulletController>().SetTarget(targetWord.transform);
    }
}

