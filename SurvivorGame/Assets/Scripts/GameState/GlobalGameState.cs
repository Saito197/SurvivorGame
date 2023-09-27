using SaitoGames.SurvivorGame.Character;
using SaitoGames.Utilities;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaitoGames.SurvivorGame.GameState
{

    public class GlobalGameState : StateMachine
    {
        [SerializeField] private MainMenuParameters _mainMenuParameters;
        [SerializeField] private CharacterList _characters;
        [SerializeField] private FloatVariableAsset _playtime;

        public void HandleCharacterSelect(int characterId)
        {
            _characters.SetActiveCharacter(characterId);
            SceneManager.LoadScene("GameplayScene");
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _playtime.Value = 0f;
            var pausedState = new GGamePausedState(this);

            StateMachineInit(pausedState, new List<State>
            {
                pausedState,
                new GGameActiveState(this, _playtime),
            });
        }
    }
}