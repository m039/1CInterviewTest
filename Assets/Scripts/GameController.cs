using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {

    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        #region Inspector

        [Header("Settings")]
        public float EnemySpeedMin = 1f;

        public float EnemySpeedMax = 5f;

        public int PlayerHealth = 20;

        [Header("Game Objects")]
        [SerializeField] FinishLine _FinishLine;

        [SerializeField] EnemySpawner _EnemySpawner;

        [SerializeField] GameUI _ui;

        #endregion

        public FinishLine FinishLine => _FinishLine;

        readonly public EnemyFactory EnemyFactory = new();

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _ui.HealthCounter = PlayerHealth;
            _ui.onRestart += OnRestart;
            _ui.HideAllScreens();

            _EnemySpawner.Spawn();
        }

        void OnRestart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
