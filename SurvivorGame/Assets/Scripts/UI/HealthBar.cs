using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.GameState
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform _fill;

        public void SetFill(float value)
        {
            value = Mathf.Clamp01(value);
            _fill.localScale = _fill.localScale.With(x: value);
        }
    }
}