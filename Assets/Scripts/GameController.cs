using UnityEngine;

namespace Game {

    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        #region Inspector

        [Header("Settings")]
        public bool settings;

        [Header("Game Objects")]
        [SerializeField] FinishLine _FinishLine;

        #endregion

        public FinishLine FinishLine => _FinishLine;

        void Awake()
        {
            Instance = this;
        }
    }

}
