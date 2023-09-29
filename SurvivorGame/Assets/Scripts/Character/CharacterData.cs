using SaitoGames.SurvivorGame.GameState;
using SaitoGames.SurvivorGame.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Character
{
    [CreateAssetMenu]
    public class CharacterData : ScriptableObject
    {
        public GameObject CharacterPrefab;
        public Weapons DefaultWeapon;
        public CharacterParameters DefaultParameters;
        public Upgrades PossibleUpgrades;
    }
}