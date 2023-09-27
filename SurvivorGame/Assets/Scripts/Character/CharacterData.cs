using SaitoGames.SurvivorGame.Weapon;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Character
{
    [CreateAssetMenu]
    public class CharacterData : ScriptableObject
    {
        public GameObject CharacterPrefab;
        public Weapons DefaultWeapon;
        public CharacterParameters DefaultParameters;
    }
}