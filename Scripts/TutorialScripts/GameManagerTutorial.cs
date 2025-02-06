using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManagerTutorial : MonoBehaviour
{
    
    public TextMeshProUGUI TimeText;  // 60秒カウントダウンのUI
    public TextMeshProUGUI targetLetterText;  // 目標の文字
    public TextMeshProUGUI scoreText;  // スコア表示用のUI
    public TextMeshProUGUI GameOverText;  // スコア表示用のUI
    public Button RetryButton;  // スコア表示用のUI
    public Button TitleButton;  // スコア表示用のUI
    public GameObject keyboardPanel; //  仮想キーボードのパネル
    public GameObject fingerPanel;   //  指のパネル


    public VirtualKeyboardTutorial virtualKeyboard;  // 仮想キーボードの参照
    public FingerIndicatorTutorial fingerIndicator;  // 指の表示スクリプト
    public AudioManagerTutorial audioManager;

    private float timeRemaining = 60f;
    private char currentLetter;
    private bool gameStarted = false;
    private bool pressedF = false;
    private bool pressedJ = false;
    private int score = 0;  // スコア

    void Start()
    {
        TimeText.text="";
        targetLetterText.gameObject.SetActive(true);
        keyboardPanel.SetActive(true); //  キーボードパネルを表示
        fingerPanel.SetActive(true);   //  指のパネルを表示
        GameOverText.gameObject.SetActive(false); // ゲームオーバーテキストを非表示
        RetryButton.gameObject.SetActive(false);  // リトライボタンを非表示
        TitleButton.gameObject.SetActive(false);  // タイトルボタンを非表示
        targetLetterText.text = "準備はOKですか？<br>「f」と「j」を同時押しで開始";
        scoreText.text = "Score: 0";
    }

    void Update()
    {
        if (!gameStarted)
        {
            CheckStartKeys();
        }
        else
        {
            CheckTyping();
        }
    }

    void CheckStartKeys()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            pressedF = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            pressedJ = true;
        }

        if (pressedF && pressedJ)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        StartCoroutine(CountdownTimer());
        GenerateRandomLetter();
    }

    IEnumerator CountdownTimer()
    {
        while (timeRemaining > 0)
        {
            TimeText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        GameOver();
    }

    void GenerateRandomLetter()
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        currentLetter = letters[Random.Range(0, letters.Length)];
        targetLetterText.text = currentLetter.ToString();

        virtualKeyboard.ResetKeyColors();

        Color fingerColor = fingerIndicator.GetFingerColor(currentLetter);
        virtualKeyboard.HighlightKeyWithColor(currentLetter, fingerColor);

        fingerIndicator.UpdateFingerDisplay(currentLetter);
    }

    void CheckTyping()
    {
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if (c == currentLetter)
                {
                    score++;  // スコア加算
                    scoreText.text = "Score: " + score;  // スコア表示更新

                    audioManager.PlayCorrectSE();
                    virtualKeyboard.ResetKeyColors();
                    GenerateRandomLetter();
                    break;
                }
                else
                {
                    audioManager.PlayWrongSE(); //不正解SEを鳴らす
                }
            }
        }
    }

    public void GameOver()
    {
        keyboardPanel.SetActive(false); //  キーボードパネルを非表示
        fingerPanel.SetActive(false);   //  指のパネルを非表示
        targetLetterText.text = "";
        TimeText.text = "";
        virtualKeyboard.ResetKeyColors();
        audioManager.PlayGameOverSE();
        targetLetterText.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(true); // ゲームオーバーテキストを非表示
        RetryButton.gameObject.SetActive(true);  // リトライボタンを非表示
        TitleButton.gameObject.SetActive(true);  // タイトルボタンを非表示



    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 現在のシーンを再読み込み
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene"); // タイトル画面のシーンを読み込む（シーン名は適宜変更）
    }
}
