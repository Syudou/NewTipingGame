using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class WordController_2 : MonoBehaviour
{
    public TextMeshPro textDisplay; // 日本語とローマ字を表示するTextMeshPro
    public string romajiWord; // タイピングに必要なローマ字
    public string currentInput = ""; // 現在のプレイヤー入力

    private Vector3 targetPosition; // プレイヤーの位置（目標地点）
    private float speed; // 移動速度
                         
    public string character;// 文字を保持するフィールド
    public int index; //識別番号

    private Renderer wordRenderer;

    public WordManager_2 wordManager; // WordManager_2 の参照
    void Start()
    {
        wordManager = FindObjectOfType<WordManager_2>();
        if (wordManager == null)
        {
            Debug.LogError("wordManager が見つかりません！ Hierarchy に WordManager_2 があるか確認してください。");
        }
        else
        {
            Debug.Log("wordManager を正常に取得しました！");
        }
    }
    // 初期化（日本語とローマ字のセット）
    public void Initialize(string japaneseWord, string romaji)
    {
        romajiWord = romaji;
        textDisplay.text = $"{japaneseWord}\n<color=yellow>{romaji}</color>"; // 日本語とローマ字を表示
        wordRenderer = GetComponent<Renderer>(); // Rendererを取得
    }

    // 目標地点と速度を設定
    public void SetTarget(Vector3 target, float moveSpeed)
    {
        targetPosition = target;
        speed = moveSpeed;
    }

    void Update()
    {
        // 単語をプレイヤーに向かって移動させる
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // プレイヤーに到達した場合（ゲームオーバー）
        if (transform.position == targetPosition)
        {
            FindObjectOfType<GameManager_2>().GameOver();
            Destroy(gameObject);
        }

        // キーボード入力を処理（ローマ字入力を監視）
        ProcessKeyboardInput();

        
    }


    // プレイヤー入力を処理
    private void ProcessKeyboardInput()
    {
        if (index != GameManager_2.masterIndex) return; //該当キーワードでない場合は何も起こさない
       
        // キー入力を監視（ローマ字）
        foreach (char c in "abcdefghijklmnopqrstuvwxyz")
        {
            if (Input.GetKeyDown(c.ToString()))
            {
                bool isComplete = ProcessInput(c);
                if (isComplete)
                {
                    // 入力完了時に弾を発射
                    TypingController_2 typingController = FindObjectOfType<TypingController_2>();
                    if (typingController != null)
                    {
                        typingController.Shoot(this); // 弾を発射

                        GameManager_2.masterIndex++; //ターゲット番号を1進める　
                    }
                }
            }
        }
    }

    // ローマ字入力を処理する
    public bool ProcessInput(char input)
    {
        if (wordManager == null)
        {
            //Debug.LogError("wordManager が null です！");
            return false;
        }

        currentInput += input; // 入力された文字を追加
        //Debug.Log($"現在の入力: {currentInput}"); 

        //wordManager.UpdateTypedText(currentInput); //  変更: 1文字ではなく `currentInput` 全体を渡す！

        if (romajiWord.StartsWith(currentInput))
        {
            wordManager.UpdateTypedText(currentInput); //UIに入力中の文字を出す

            if (romajiWord == currentInput) // 正しく入力完了した場合
            {
                //Debug.Log("入力完了: " + currentInput);
                wordManager.ResetTypedText();
                currentInput = ""; //  入力リセット！
                return true;
            }
            return false;
        }
        else
        {
            

            Debug.Log("リセット");
            currentInput = ""; // 間違った場合はリセット
            wordManager.ResetTypedText(); //  UI もリセット
            //wordRenderer.material.color = Color.red;
            return false;
        }
    }


}
