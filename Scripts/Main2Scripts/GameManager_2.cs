using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager_2 : MonoBehaviour
{
    public int maxHP = 100; // プレイヤーの最大HP
    private int currentHP; // 現在のHP
    public int score = 0; // 現在のスコア

    public static int masterIndex = 1; //ターゲット番号

    public TextMeshProUGUI hpText; // HP 表示用の Text (UI)
    public TextMeshProUGUI scoreText; // スコア表示用の Text (UI)
    public TextMeshProUGUI gameOverText; // ゲームオーバー表示用
    public TextMeshProUGUI readyText; // 準備完了のテキスト
    public TextMeshProUGUI TypedText;

    public GameObject retryButton; // リトライボタン
    public GameObject titleButton; // タイトルボタン
    public AudioManager_2 audioManager; // AudioManagerを参照

    public AudioClip hitSound; // 効果音
    private AudioSource audioSource;

    public bool isGameOver = false; // ゲームオーバー状態の管理
    private bool isGameStarted = false; // ゲーム開始状態の管理

    // Start is called before the first frame update
    void Start()
    {
        masterIndex = 1;

        audioSource = GetComponent<AudioSource>(); // AudioSource コンポーネントを取得

        // 初期設定
        currentHP = maxHP;
        

        // UI要素の初期化
        UpdateHPText();
        UpdateScoreText();
        gameOverText.gameObject.SetActive(false); // ゲームオーバー非表示
        retryButton.SetActive(false);
        titleButton.SetActive(false);
        readyText.gameObject.SetActive(true); // 準備完了テキスト表示
        TypedText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム開始フラグが立っていない場合、同時押しを検知
        if (!isGameStarted)
        {
            CheckForGameStart();
        }
    }

    private void CheckForGameStart()
    {
        // 「F」と「J」の同時押しを検知
        if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.J))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        isGameStarted = true;
        readyText.gameObject.SetActive(false); // 準備完了テキストを非表示
        Debug.Log("Game Started!");
    }

    private void OnEnable()
    {
        //ProjectileController_2.OnProjectileHitWord += HandleProjectileHitWord;
        PlayerController_2.OnPlayerTakeDamage += HandlePlayerTakeDamage;


    }

    

    private void OnDisable()
    {
        //ProjectileController_2.OnProjectileHitWord -= HandleProjectileHitWord;
        PlayerController_2.OnPlayerTakeDamage -= HandlePlayerTakeDamage;
    }

    private void HandleProjectileHitWord(WordController_2 word, ProjectileController_2 projectile)
    {
        if (isGameOver) return;

        string wordText = word.textDisplay.text;
        Debug.Log($"Word '{wordText}' was hit by a projectile!");

        // スコアを加算
        score += 10;
        UpdateScoreText();

        // 効果音を再生
        if (audioSource != null && hitSound != null)
        {
            Debug.Log("ひっと！効果音を再生します。");
            audioSource.PlayOneShot(hitSound);
        }
        else
        {
            Debug.LogError("AudioSource または hitSound が null です。");
        }
    }

    // プレイヤーがダメージを受けたときの処理
    private void HandlePlayerTakeDamage(int damage)
    {
        if (isGameOver) return;

        currentHP -= damage;
        UpdateHPText();

        // HPが0以下になった場合の処理
        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    // HPテキストを更新
    private void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP;
        }
    }

    // スコアテキストを更新
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // ゲームオーバー時の処理
    public void GameOver()
    {

        isGameOver = true;
        Debug.Log("Game Over!");

        // WordManagerのスポーンを停止し、文字を全て消去
        WordManager_2 wordManager = FindObjectOfType<WordManager_2>();
        if (wordManager != null)
        {
            wordManager.enabled = false;
            wordManager.ClearAllWords(); // すべての文字を消去
        }

        // プレイヤーの操作を停止
        PlayerController_2 player = FindObjectOfType<PlayerController_2>();
        if (player != null)
        {
            player.enabled = false;
        }

        gameOverText.gameObject.SetActive(true); // ゲームオーバー非表示
        retryButton.SetActive(true);
        titleButton.SetActive(true);

        // ゲームオーバー時にSEを再生
        audioManager.PlayGameOverSE();

        TypedText.gameObject.SetActive(false); //タイピングは消す
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    // リトライボタン用の処理
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 現在のシーンを再読み込み
    }

    // タイトルボタン用の処理
    public void GoToTitle()
    {
        // タイトルシーンをロードする（"TitleScene"がシーン名の場合）
        SceneManager.LoadScene("TitleScene");
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // 単語と弾の衝突を処理する関数
    public void OnWordHit(WordController_2 hitWord)
    {
        if (isGameOver) return;

        Debug.Log($"Word '{hitWord.textDisplay.text}' was hit!");

        // スコアを加算
        //score += 10;
        //UpdateScoreText();

        // 効果音を再生
        if (audioSource != null && hitSound != null)
        {
            Debug.Log("効果音を再生します");
            audioSource.PlayOneShot(hitSound);
        }
        else
        {
            Debug.LogError("AudioSource または hitSound が null です。");
        }
    }
}
