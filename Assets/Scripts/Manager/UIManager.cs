using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using Spaceship;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] public TextMeshProUGUI HPEnemyText;
    [SerializeField] public TextMeshProUGUI HPPlayerText;
    [SerializeField] private TextMeshProUGUI  LevelText;
    [SerializeField] private TextMeshProUGUI  inLevelBottomText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text highScoreInNextLevelText;
    public int level;
    
    public static int HPEnemy;
    public static int HPPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        HPEnemy = GameManager.Instance.enemySpaceshipHp;
        HPPlayer = GameManager.Instance.playerSpaceshipHp;
        level = 1;
        HideHP(true);
    }

    public void HideHP(bool hide)
    {
        HPEnemyText.gameObject.SetActive(!hide);
        HPPlayerText.gameObject.SetActive(!hide);
        LevelText.gameObject.SetActive(!hide);
        scoreText.gameObject.SetActive(!hide);
        
    }


    void Update()
    {
        //HPEnemyText.text = "HP Enemy: " + EnemySpaceship.HPEnemy;
        HPEnemyText.text = "HP Enemy: " + HPEnemy;
        HPPlayerText.text = "HP Player: " + HPPlayer;
        LevelText.text = "Level: " + level;
        inLevelBottomText.text = "NEXT LEVEL " + (level + 1);
        scoreText.text = "Score: " + ScoreManager.Instance.playerScore;
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        highScoreInNextLevelText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");

    }

    public void HighScoreStartText(bool hide)
    {
        highScoreText.gameObject.SetActive(!hide);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        HighScoreStartNextLevelText(!hide);
    }
    public void HighScoreStartNextLevelText(bool hide)
    {
        highScoreInNextLevelText.gameObject.SetActive(hide);
        highScoreInNextLevelText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    public void ResetScoreBottom()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }
    
    public void OnClickNextLevelBotton()
    {
        HideHP(false);
        UIManager.Instance.HighScoreStartText(true);
        GameManager.Instance.StartNextLevel();
    }

    public void RestartBottom()
    {
        GameManager.Instance.Restart();
        
    }
}
