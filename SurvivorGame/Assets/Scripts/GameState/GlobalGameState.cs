using SaitoGames.SurvivorGame.Character;
using SaitoGames.Utilities;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaitoGames.SmasherGame.GameState
{

    public class GlobalGameState : StateMachine
    {
        [SerializeField] private MainMenuParameters _mainMenuParameters;
        [SerializeField] private CharacterList _characters;

        public void HandleCharacterSelect(int characterId)
        {
            _characters.SetActiveCharacter(characterId);
            SceneManager.LoadScene("GameplayScene");
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            var pausedState = new GGamePausedState(this);

            StateMachineInit(pausedState, new List<State>
            {
                pausedState,
                new GGameActiveState(this),
            });
        }
    }
}