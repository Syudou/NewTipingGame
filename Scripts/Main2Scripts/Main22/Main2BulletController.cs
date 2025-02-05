using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main2BulletController : MonoBehaviour
{
    public float speed = 5.0f;  // 弾の速度
    private Transform targetWord;  // 目標の単語

    void Update()
    {
        if (targetWord != null)
        {
            // 目標に向かって弾を進める
            transform.position = Vector3.MoveTowards(transform.position, targetWord.position, speed * Time.deltaTime);

            // 目標に到達したら処理
            if (transform.position == targetWord.position)
            {
                targetWord.GetComponent<Main2WordController>().DestroyWord();
                Destroy(gameObject);  // 弾を削除
            }
        }
    }

    // 目標となる単語を設定
    public void SetTarget(Transform target)
    {
        targetWord = target;
    }
}

