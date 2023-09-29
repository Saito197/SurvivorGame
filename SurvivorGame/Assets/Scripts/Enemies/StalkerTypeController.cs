using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    public class StalkerTypeController : EnemyController
    {
        [SerializeField] private Transform _playerCharacter;
        [SerializeField] private List<StalkerTypeEnemy> _stalkers;

        public override void AddEnemy(Enemy enemy)
        {
            if (enemy.GetType() == typeof(StalkerTypeEnemy))
            {
                _stalkers.Add((StalkerTypeEnemy) enemy);
            }
        }

        public override void RemoveEnemy(Enemy enemy)
        {
            if (enemy.GetType() == typeof(StalkerTypeEnemy))
            {
                _stalkers.Remove((StalkerTypeEnemy)enemy);
            }
        }

        private void Update()
        {
            foreach (var s in _stalkers)
            {
                s.EnemyParams.TargetPosition = _playerCharacter.transform.position;
                s.EnemyParams.CalculatePosition(Time.deltaTime);
            }
        }
    }
}
