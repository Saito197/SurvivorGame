using SaitoGames.SurvivorGame.Character;
using SaitoGames.SurvivorGame.Weapon;
using SaitoGames.Utilities;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaitoGames.SurvivorGame.GameState
{

    public class GlobalGameState : StateMachine
    {
        [Header("Events")]
        [SerializeField] private GameEvent _upgradeSelectedEvent;


        [Header("Parameters")]
        //[SerializeField] private MainMenuParameters _mainMenuParameters;
        [SerializeField] private LevelUpHandler _levelUpPrefab;
        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private CharacterList _characters;
        [SerializeField] private FloatVariableAsset _playtime;
        [SerializeField] private FloatVariableAsset _level;
        [SerializeField] private FloatVariableAsset _currentExp;
        [SerializeField] private FloatVariableAsset _nextLevelExp;
        [SerializeField] private Upgrades _upgrades;


        private Canvas _canvas;
        private LevelUpHandler _levelUpHandler;

        private readonly string _gameplayScene = "GameplayScene";

        public void HandleCharacterSelect(int characterId)
        {
            _characters.SetActiveCharacter(characterId);
            var upgrades = _characters.ActiveCharacter.PossibleUpgrades.AvailableUpgrades;
            _upgrades.AvailableUpgrades = new List<Weapons>(upgrades);
            foreach (var w in _upgrades.AvailableUpgrades)
            {
                w.CurrentLevel = -1;
            }

            SceneManager.LoadScene(_gameplayScene);
            SceneManager.sceneLoaded += SceneLoadComplete;
        }

        private void SceneLoadComplete(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            if (scene.name == _gameplayScene && scene.isLoaded)
            {
                ChangeState<GGameActiveState>();
                _spawner.InitializeSpawner();
                _spawner.SpawnNewWave(0);
                _canvas = FindObjectOfType<Canvas>();
                _levelUpHandler = Instantiate(_levelUpPrefab, _canvas.transform);
                _levelUpHandler.gameObject.SetActive(false);
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            // Initialize default values
            _playtime.Value = 0f;
            _level.Value = 1f;
            _currentExp.Value = 0f;
            _nextLevelExp.Value = 5f;

            // Register for events
            _currentExp.OnValueChanged += HandleExpGained;
            _upgradeSelectedEvent.Response += HandleWeaponUpgrade;

            // Initialize state machine
            var pausedState = new GGamePausedState(this);
            StateMachineInit(pausedState, new List<State>
            {
                pausedState,
                new GGameActiveState(this, _playtime, _spawner),
            });
        }

        private void OnDestroy()
        {
            _currentExp.OnValueChanged -= HandleExpGained;
            _upgradeSelectedEvent.Response -= HandleWeaponUpgrade;
        }

        private void HandleWeaponUpgrade(object[] args)
        {
            _levelUpHandler.gameObject.SetActive(false);
            ChangeState<GGameActiveState>();
        }

        private void HandleExpGained()
        {
            // Handle leveling up
            if (_currentExp.Value > _nextLevelExp.Value)
            {
                _nextLevelExp.Value += 5 + _level.Value * 10;
                _level.Value++;
                _levelUpHandler.gameObject.SetActive(true);
                ChangeState<GGamePausedState>();
            }
        }
    }
}