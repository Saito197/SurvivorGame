using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Character
{
    [CreateAssetMenu]
    public class CharacterList : ScriptableObject
    {
        public CharacterData ActiveCharacter;
        [SerializeField] private List<CharacterData> _characterList;

        public void SetActiveCharacter(int index)
        {
            if (index >= _characterList.Count)
            {
                ActiveCharacter = _characterList[0];
                return;
            }

            ActiveCharacter = _characterList[index];
        }
    }
}