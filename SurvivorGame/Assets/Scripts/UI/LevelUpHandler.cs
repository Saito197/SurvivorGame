using SaitoGames.SurvivorGame.Weapon;
using SaitoGames.Utilities;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SaitoGames.SurvivorGame.GameState
{

    public class LevelUpHandler : MonoBehaviour
    {
        [SerializeField] private GameEvent _upgradeSelection;
        [SerializeField] private List<TMP_Text> _upgradesText;
        [SerializeField] private Upgrades _upgrades;

        [SerializeField] private Weapons[] _selections;

        public void WeaponSelected(int index)
        {
            var w = _selections[index];
            if (w != null)
                _upgradeSelection.Raise(new object[] { w });
        }

        private void OnEnable()
        {
            _selections = new Weapons[2];

            if (_upgrades.AvailableUpgrades.Count >= 2)
            {
                _upgrades.AvailableUpgrades.Shuffle();
                _selections[0] = _upgrades.AvailableUpgrades[0];
                _selections[1] = _upgrades.AvailableUpgrades[1];
                _upgradesText[0].text = $"{_selections[0].name} level {_selections[0].CurrentLevel + 1}";
                _upgradesText[1].text = $"{_selections[1].name} level {_selections[1].CurrentLevel + 1}";
            }

        }
    }
}