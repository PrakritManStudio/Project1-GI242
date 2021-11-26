using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class ScoreManager : MonoBehaviour
    {
        // [SerializeField] private TextMeshProUGUI scoreText;
        // [SerializeField] private Text finalScoreText;

        public static ScoreManager Instance { get; private set; }
        
        public int playerScore;
        public int highScore;

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

        private void Start()
        {
            playerScore = 0;
            // GameManager.Instance.OnRestarted += OnRestarted;
            
        }

        private void Update()
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            
            if (playerScore >= highScore)
            {
                highScore = playerScore;
                PlayerPrefs.SetInt("HighScore", playerScore);
            }
        }
        
        
        // public void Init()
        // {
        //     //finalScoreText.text = $"";
        //     //this.gameManager = gameManager;
        //     GameManager.Instance.OnRestarted += OnRestarted;
        //     HideScore(false);
        //     SetScore(0);
        // }

        public void SetScore(int score)
        {
            playerScore = score;
        }
        
        // private void OnRestarted()
        // {
        //     finalScoreText.text = $"Player Score: {playerScore}";
        //     
        //     GameManager.Instance.OnRestarted -= OnRestarted;
        //     HideScore(true);
        //     SetScore(0);
        // }

        // public void HideScore(bool hide)
        // {
        //     scoreText.gameObject.SetActive(!hide);
        // }
    }
}


