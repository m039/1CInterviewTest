using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameUI : MonoBehaviour
    {
        #region Inspector

        [SerializeField] TMPro.TMP_Text _HealthCounter;

        [SerializeField] RectTransform _LoseScreen;

        [SerializeField] RectTransform _WinScreen;

        #endregion

        public event System.Action onRestart;

        public int HealthCounter
        {
            get
            {
                if (int.TryParse(_HealthCounter.text, out var value))
                {
                    return value;
                }

                return 0;
            }

            set
            {
                _HealthCounter.text = value.ToString();
            }
        }

        List<RectTransform> _screens = new();

        void Awake()
        {
            Init();
        }

        void Init()
        {
            _screens.Add(_LoseScreen);
            _screens.Add(_WinScreen);
        }

        void ShowScreen(RectTransform screen)
        {
            foreach (var s in _screens)
            {
                s.gameObject.SetActive(s == screen);
            }
        }

        public  void HideAllScreens()
        {
            ShowScreen(null);
        }

        public void ShowLoseScreen()
        {
            ShowScreen(_LoseScreen);
        }

        public void ShowWinScreen()
        {
            ShowScreen(_WinScreen);
        }

        public void OnRestartClicked()
        {
            onRestart?.Invoke();
        }
    }
}
