using SaitoGames.Utilities;
using TMPro;
using UnityEngine;

namespace SaitoGames.SurvivorGame.GameState
{
    public class GGameActiveState : State
    {
        private FloatVariableAsset _playtime;
        private EnemySpawner _spawner;

        public GGameActiveState(
            StateMachine stateMachine,
            FloatVariableAsset playtime,
            EnemySpawner spawner
        ) : base(stateMachine)
        {
            _playtime = playtime;
            _spawner = spawner;
        }

        public override void StateUpdate()
        {
            var delta = Time.deltaTime;
            _playtime.Value += delta;

            if (_spawner != null)
            {
                _spawner.SpawnIntervalUpdate(delta);
                CalculateWave();
            }
        }

        private void CalculateWave()
        {
            var currentWave = (int)(_playtime.Value / 60);
            if (currentWave > _spawner.WaveIndex)
            {
                _spawner.SpawnNewWave(currentWave);
            }
        }


    }
}