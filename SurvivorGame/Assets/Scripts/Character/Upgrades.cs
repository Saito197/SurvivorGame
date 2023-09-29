using SaitoGames.SurvivorGame.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.GameState
{
    [CreateAssetMenu]
    public class Upgrades : ScriptableObject
    {
        public List<Weapons> AvailableUpgrades;
    }
}