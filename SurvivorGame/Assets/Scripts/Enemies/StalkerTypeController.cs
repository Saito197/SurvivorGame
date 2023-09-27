using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    public class StalkerTypeController : MonoBehaviour
    {
        [SerializeField] private Transform _playerCharacter;
        [SerializeField] private List<StalkerTypeEnemy> _stalkers;

        private void Update()
        {
            foreach (var s in _stalkers)
            {
                s.MovementParams.TargetPosition = _playerCharacter.transform.position;
                s.MovementParams.CalculatePosition(Time.deltaTime);
            }
        }
    }
}
