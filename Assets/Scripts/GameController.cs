using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {

    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        #region Inspector

        [Header("Settings")]

        [Header("Enemy")]
        public float EnemySpeedMin = 1f;

        public float EnemySpeedMax = 5f;

        public float EnemySpawnTimeoutMin = 2f;

        public float EnemySpawnTimeoutMax = 4f;

        public int EnemyHealth = 2;

        public int EnemyCountMin = 3;

        public int EnemyCountMax = 7;

        [Header("Player")]
        public int PlayerHealth = 20;

        public float PlayerShootingSpeed = 5f;

        public float PlayerShootingRadius = 5f;

        public float ProjectileSpeed = 10f;

        public int ProjectileDamage = 2;

        [Header("Game Objects")]
        [SerializeField] FinishLine _FinishLine;

        [SerializeField] EnemySpawner _EnemySpawner;

        [SerializeField] GameUI _ui;

        #endregion

        public FinishLine FinishLine => _FinishLine;

        public EnemySpawner EnemySpawner => _EnemySpawner;

        readonly public EnemyFactory EnemyFactory = new();

        int _enemyCount;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _ui.HealthCounter = PlayerHealth;
            _ui.onRestart += OnRestart;
            _ui.HideAllScreens();

            _enemyCount = Random.Range(EnemyCountMin, EnemyCountMax + 1);
        }

        void OnRestart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void EnemyDied()
        {
            _enemyCount--;
            if (_enemyCount <= 0)
            {
                _ui.ShowWinScreen();
                Time.timeScale = 0f;
            }
        }

        public void EnemyCrossedFinishedLine()
        {
            _ui.HealthCounter -= 1;

            if (_ui.HealthCounter <= 0)
            {
                _ui.ShowLoseScreen();
                Time.timeScale = 0f;
            }
        }
    }

}
