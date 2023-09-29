using SaitoGames.Utilities;
using TMPro;
using UnityEngine;

namespace SaitoGames.SurvivorGame.GameState
{
    public class UIHandler : MonoBehaviour 
    {
        // UI object references
        [Header("UI Objects")]
        [SerializeField] private TMP_Text _gameTimer;
        [SerializeField] private HealthBar _healthBar;

        // Data variable references
        [Header("Data References")]
        [SerializeField] private FloatVariableAsset _gametime;
        [SerializeField] private FloatVariableAsset _health;
        [SerializeField] private FloatVariableAsset _maxHealth;

        private void Awake()
        {
            _gametime.OnValueChanged += UpdateGameTimer;
            _health.OnValueChanged += UpdateHealthBar;
        }

        private void OnDestroy()
        {
            _gametime.OnValueChanged -= UpdateGameTimer;
            _health.OnValueChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            var percentage = _health.Value / _maxHealth.Value;
            _healthBar.SetFill(percentage);
        }


        private void UpdateGameTimer()
        {
            var minute = ((int)_gametime.Value / 60).ToString("00");
            var seconds = (_gametime.Value % 60f).ToString("00");
            _gameTimer.text = $"{minute}:{seconds}";
        }
    }
}