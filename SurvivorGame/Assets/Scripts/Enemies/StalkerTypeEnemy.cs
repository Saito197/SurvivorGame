using SaitoGames.Utilities;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    public struct EnemyMovementParameters
    {
        public float3 TargetPosition;
        public float3 CurrentPosition;
        public float3 CalculatedPosition;

        public float Speed;

        public void CalculatePosition(float delta)
        {
            var dir = math.normalize(TargetPosition - CurrentPosition);
            CalculatedPosition = CurrentPosition + (dir * Speed * delta);
        }
    }

    public class StalkerTypeEnemy : Enemy
    {
        public EnemyMovementParameters MovementParams;
        [SerializeField] private Animator _anim;

        public override void TakeDamage()
        {
        }

        private void Awake()
        {
            var movementState = new EStandardMovementState(this, _anim, MovementParams);
            StateMachineInit(movementState, new List<State>
            {
                movementState,
                new EDamagedState(this),
                new EDeathState(this)
            });
        }
    }
}
