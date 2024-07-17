using UnityEngine;

namespace Game {

    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        #region Inspector

        [Header("Settings")]
        public float EnemySpeedMin = 1f;

        public float EnemySpeedMax = 5f;

        [Header("Game Objects")]
        [SerializeField] FinishLine _FinishLine;

        [SerializeField] EnemySpawner _EnemySpawner;

        #endregion

        public FinishLine FinishLine => _FinishLine;

        readonly public EnemyFactory EnemyFactory = new();

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _EnemySpawner.Spawn();
        }
    }

}
