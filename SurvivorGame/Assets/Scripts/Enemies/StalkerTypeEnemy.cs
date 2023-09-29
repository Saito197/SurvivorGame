using SaitoGames.Utilities;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    [Serializable]
    public class EnemyParameters
    {
        public Animator Animator;

        public Vector3 TargetPosition;
        public Vector3 CurrentPosition;
        public Vector3 CalculatedPosition;

        public EnemyStats Stats;

        public void CalculatePosition(float delta)
        {
            var dir = TargetPosition - CurrentPosition;
            dir.Normalize();
            
            CalculatedPosition = CurrentPosition + (delta * Stats.Speed * dir);
        }
    }

    public class StalkerTypeEnemy : Enemy
    {
        public EnemyParameters EnemyParams;

        protected override void OnEnable()
        {
            base.OnEnable();
            EnemyParams.Animator = _anim;
            EnemyParams.Stats = _stats;
            EnemyParams.CurrentPosition = transform.position;
            EnemyParams.CalculatedPosition = transform.position;


            if (_states != null && _states.Count > 0)
                ChangeState<EStandardMovementState>();

        }

        private void Start()
        {
            var movementState = new EStandardMovementState(this, EnemyParams, _collider);
            StateMachineInit(movementState, new List<State>
            {
                movementState,
                new EDamagedState(this, EnemyParams),
                new EDeathState(this, _defeatedEvent)
            });
        }
    }
}
