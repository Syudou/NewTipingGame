using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingControllerTutorial : MonoBehaviour
{
    public GameObject projectilePrefab; // 弾のプレハブ
    public Transform shootPoint; // 弾を発射する位置

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ゲームが開始していなければ弾を発射しない
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null && !gameManager.IsGameStarted())
        {
            return;
        }

        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                // シフトキーが押されていない状態で大文字が入力された場合、無視
                bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

                // 大文字の入力かつShiftが押されていない場合は無視
                if (char.IsUpper(c) && !isShiftPressed)
                {
                    Debug.Log($"大文字入力だがシフトキーが押されていないため無視: {c}");
                    continue;
                }

                // 入力が有効ならば弾を発射
                CheckAndShoot(c.ToString());
            }
        }
    }

    void CheckAndShoot(string input)
    {
        WordController[] words = FindObjectsOfType<WordController>();

        foreach (var word in words)
        {
            // 入力された文字と word.character を比較（小文字・大文字問わず）
            if (word.character == input)
            {
                Shoot(word);
                break;
            }
        }
    }

    void Shoot(WordController targetWord)
    {
        // 弾を生成
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        projectile.GetComponent<ProjectileController>().Initialize(targetWord);
    }
}
