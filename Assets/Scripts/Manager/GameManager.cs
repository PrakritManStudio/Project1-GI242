using System;
using Enemy;
using Spaceship;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private RectTransform dialog;
        [SerializeField] private PlayerSpaceship playerSpaceship;
        [SerializeField] private EnemySpaceship enemySpaceship;
        //[SerializeField] private ScoreManager scoreManager;
        public event Action OnRestarted;
        [SerializeField] public int playerSpaceshipHp;
        [SerializeField] private int playerSpaceshipMoveSpeed;
        [SerializeField] public int enemySpaceshipHp;
        [SerializeField] private int enemySpaceshipMoveSpeed;

        [SerializeField] public double enemyFireRate;

        //Temp Level
        private int tempHpPlayer, tempHpEnemy;
        private double tempFireRateEnemy;
        
        [SerializeField] public RectTransform nextLevelPanel;

        //public static int HPEnemy;
        //public static int HPPlayer;
        
        //[SerializeField] private SoundManager soundManager;

        public static GameManager Instance { get; private set; }

        private PlayerSpaceship spawnedPlayerSpaceship;

        private void Awake()
        {
            Debug.Assert(startButton != null, "startButton cannot be null");
            Debug.Assert(dialog != null, "dialog cannot be null");
            Debug.Assert(playerSpaceship != null, "playerSpaceship cannot be null");
            Debug.Assert(enemySpaceship != null, "enemySpaceship cannot be null");
            Debug.Assert(ScoreManager.Instance != null, "scoreManager cannot be null");
            Debug.Assert(playerSpaceshipHp > 0, "playerSpaceship hp has to be more than zero");
            Debug.Assert(playerSpaceshipMoveSpeed > 0, "playerSpaceshipMoveSpeed has to be more than zero");
            Debug.Assert(enemySpaceshipHp > 0, "enemySpaceshipHp has to be more than zero");
            Debug.Assert(enemySpaceshipMoveSpeed > 0, "enemySpaceshipMoveSpeed has to be more than zero");
            
            //Debug.Assert(soundManager != null, "SoundManager is Null, need to setup");
            
            startButton.onClick.AddListener(OnStartButtonClicked);
            //soundManager.PlayBGMusic();
            //SoundManager.Instance.PlayBGMusic();
            //SoundManager.Instance.BGMusic();
            
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
            SetTempDefault();
        }

        private void OnStartButtonClicked()
        {
            dialog.gameObject.SetActive(false);
            
            StartGame();
        }

        private void StartGame()
        {
            // ScoreManager.Instance.Init();
            SpawnPlayerSpaceship();
            SpawnEnemySpaceship();
            
            UIManager.Instance.HideHP(false);
            UIManager.Instance.HighScoreStartText(true);
        }
        
        private void SpawnPlayerSpaceship()
        {
            spawnedPlayerSpaceship = Instantiate(playerSpaceship);
            spawnedPlayerSpaceship.Init(playerSpaceshipHp, playerSpaceshipMoveSpeed);
            spawnedPlayerSpaceship.OnExploded += OnPlayerSpaceshipExploded;
        }

        private void OnPlayerSpaceshipExploded()  // Player Exploded
        {
            Restart();
            
        }

        private void SpawnEnemySpaceship()
        {
            var spawnedEnemyShip = Instantiate(enemySpaceship);
            spawnedEnemyShip.Init(enemySpaceshipHp, enemySpaceshipMoveSpeed);
            spawnedEnemyShip.OnExploded += OnEnemySpaceshipExploded;

            var enemyController = spawnedEnemyShip.GetComponent<EnemyController>();
            enemyController.Init(spawnedPlayerSpaceship);
        }

        private void OnEnemySpaceshipExploded()    // Enemy Exploded
        {
            //ScoreManager.Instance.SetScore(1);
            //Next Level
            //Restart();
            NextLevelPanel();
        }

        void NextLevelPanel()
        {
            DestroyRemainingShips();
            UIManager.Instance.HideHP(true);
            UIManager.Instance.HighScoreStartText(false);
            nextLevelPanel.gameObject.SetActive(true);
            
            OnRestarted?.Invoke();
        }

        public void StartNextLevel()
        {
            //Setting Level
            enemySpaceshipHp += 15;
            playerSpaceshipHp += 2;
            enemyFireRate -= 0.035;
            
            //Start Game
            UIManager.Instance.level += 1;
            nextLevelPanel.gameObject.SetActive(false);
            
            StartGame();
        }

        public void Restart()
        {
            DestroyRemainingShips();
            UIManager.Instance.HideHP(true);
            UIManager.Instance.HighScoreStartText(false);
            nextLevelPanel.gameObject.SetActive(false);
            dialog.gameObject.SetActive(true);
            ResetToDefault();
            OnRestarted?.Invoke();
        }

        private void SetTempDefault()
        {
            tempHpPlayer = playerSpaceshipHp;
            tempHpEnemy = enemySpaceshipHp;
            tempFireRateEnemy = enemyFireRate;
        }
        private void ResetToDefault()
        {
            playerSpaceshipHp = tempHpPlayer;
            enemySpaceshipHp = tempHpEnemy;
            enemyFireRate = tempFireRateEnemy;
            UIManager.Instance.level = 1;
            ScoreManager.Instance.playerScore = 0;
        }

        private void DestroyRemainingShips()
        {
            var remainingEnimies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in remainingEnimies)
            {
                Destroy(enemy);
            }
            
            var remainingPlayer = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in remainingPlayer)
            {
                Destroy(player);
            }
        }

    }
}
