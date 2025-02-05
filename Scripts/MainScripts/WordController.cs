using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WordController : MonoBehaviour
{
    private TextMeshPro textComponent;

    public string character { get; private set; } // 外部から読み取り可能なプロパティ

    private Vector3 targetPosition; // プレイヤーの位置
    private float moveSpeed; // 移動速度

    void Awake()
    {
        // TextMeshPro コンポーネントを取得
        textComponent = GetComponent<TextMeshPro>();
    }

    

    public void Initialize(char charToSet)
    {
        character = charToSet.ToString(); // 入力文字をプロパティにセット
        // 表示する文字を設定
        textComponent.text = character; // 表示文字を更新
    }

    public void SetTarget(Vector3 target, float speed)
    {
        targetPosition = target;
        moveSpeed = speed;
    }

    void Update()
    {
        // ターゲットに向かって移動
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // ターゲットに到達したら自身を破壊（必要なら削除）
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
