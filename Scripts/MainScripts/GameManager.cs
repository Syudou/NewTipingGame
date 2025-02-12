using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int maxHP = 100; // �v���C���[�̍ő�HP
    private int currentHP; // ���݂�HP
    public int score = 0; // ���݂̃X�R�A

    public TextMeshProUGUI hpText; // HP �\���p�� Text (UI)
    public TextMeshProUGUI scoreText; // �X�R�A�\���p�� Text (UI)
    public TextMeshProUGUI gameOverText; // �Q�[���I�[�o�[�\���p
    public TextMeshProUGUI readyText; // ���������̃e�L�X�g
    public TextMeshProUGUI highScoreText;

    public GameObject retryButton; // ���g���C�{�^��
    public GameObject titleButton; // �^�C�g���{�^��
    public AudioManager audioManager; // AudioManager���Q��

    public AudioClip hitSound; // ���ʉ�
    private AudioSource audioSource;

    public bool isGameOver = false; // �Q�[���I�[�o�[��Ԃ̊Ǘ�
    private bool isGameStarted = false; // �Q�[���J�n��Ԃ̊Ǘ�

    private string highScoreKey; //�Q�[�����Ƃ̃n�C�X�R�A�L�[

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource �R���|�[�l���g���擾

        // �����ݒ�
        currentHP = maxHP;
        

        // UI�v�f�̏�����
        UpdateHPText();
        UpdateScoreText();
        gameOverText.gameObject.SetActive(false); // �Q�[���I�[�o�[��\��
        retryButton.SetActive(false);
        titleButton.SetActive(false);
        readyText.gameObject.SetActive(true); // ���������e�L�X�g�\��

        //�Q�[�����Ƃ̃n�C�X�R�A�L�[���쐬
        highScoreKey = "HighScore_Main" + SceneManager.GetActiveScene().name;
        //�n�C�X�R�A�ǉ�
        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        if (highScoreText != null)
        {
            highScoreText.text = "HighScore: " + highScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���J�n�t���O�������Ă��Ȃ��ꍇ�A�������������m
        if (!isGameStarted)
        {
            CheckForGameStart();
        }
    }

    private void CheckForGameStart()
    {
        // �uF�v�ƁuJ�v�̓������������m
        if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.J))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        isGameStarted = true;
        readyText.gameObject.SetActive(false); // ���������e�L�X�g���\��
        Debug.Log("Game Started!");
    }

    private void OnEnable()
    {
        ProjectileController.OnProjectileHitWord += HandleProjectileHitWord;
        PlayerController.OnPlayerTakeDamage += HandlePlayerTakeDamage;

        
    }

    

    private void OnDisable()
    {
        ProjectileController.OnProjectileHitWord -= HandleProjectileHitWord;
        PlayerController.OnPlayerTakeDamage -= HandlePlayerTakeDamage;
    }

    // �C�x���g�����s���ꂽ�Ƃ��ɌĂяo�����
    private void HandleProjectileHitWord(WordController word, ProjectileController projectile)
    {
        Debug.Log($"Word '{word.character}' was hit by a projectile!");

        if (isGameOver) return;

        // �X�R�A�����Z
        score += 10; // �|�����Ƃ�10�|�C���g���Z
        UpdateScoreText();

        // ���ʉ����Đ�
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    // �v���C���[���_���[�W���󂯂��Ƃ��̏���
    private void HandlePlayerTakeDamage(int damage)
    {
        if (isGameOver) return;

        currentHP -= damage;
        UpdateHPText();

        // HP��0�ȉ��ɂȂ����ꍇ�̏���
        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    // HP�e�L�X�g���X�V
    private void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP;
        }
    }

    // �X�R�A�e�L�X�g���X�V
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            int highScore = PlayerPrefs.GetInt(highScoreKey, 0);
            highScoreText.text = "HighScore: " + highScore;
        }
    }

    // �Q�[���I�[�o�[���̏���
    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");

        // �n�C�X�R�A�̍X�V����
        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save(); // PlayerPrefs �̕ۑ�
        }

        // WordManager�̃X�|�[�����~���A������S�ď���
        WordManager wordManager = FindObjectOfType<WordManager>();
        if (wordManager != null)
        {
            wordManager.enabled = false;
            wordManager.ClearAllWords(); // ���ׂĂ̕���������
        }

        // �v���C���[�̑�����~
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.enabled = false;
        }

        gameOverText.gameObject.SetActive(true); // �Q�[���I�[�o�[��\��
        retryButton.SetActive(true);
        titleButton.SetActive(true);

        // �Q�[���I�[�o�[����SE���Đ�
        audioManager.PlayGameOverSE();

        // �X�R�A�X�V
        UpdateScoreText();
        UpdateHighScoreText(); // �n�C�X�R�A�\�����X�V
    }

    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    // ���g���C�{�^���p�̏���
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���݂̃V�[�����ēǂݍ���
    }

    // �^�C�g���{�^���p�̏���
    public void GoToTitle()
    {
        // �^�C�g���V�[�������[�h����i"TitleScene"���V�[�����̏ꍇ�j
        SceneManager.LoadScene("TitleScene");
    }


}
