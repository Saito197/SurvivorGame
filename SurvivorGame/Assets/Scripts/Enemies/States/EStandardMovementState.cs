using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    public class EStandardMovementState : State
    {
        private EnemyParameters _enemyParams;
        private EnemyStats _stats;
        private float _currentHealth;
        private Animator _anim;
        private Collider _col;

        private readonly int MovementHash = Animator.StringToHash("Move");
        private readonly int DeathHash = Animator.StringToHash("Death");

        public EStandardMovementState(
            StateMachine stateMachine,
            EnemyParameters enemyParameters,
            Collider col
        ) : base(stateMachine)
        {
            _enemyParams = enemyParameters;
            _col = col;
        }

        public override void OnStateEnter()
        {
            _anim = _enemyParams.Animator;
            _stats = _enemyParams.Stats;

            _anim.SetTrigger(MovementHash);
            var pos = _stateMachine.transform.position;
            _enemyParams.CurrentPosition = pos;
            _enemyParams.CalculatedPosition = pos;
            _currentHealth = _stats.CurrentHealth;
            _col.enabled = true;
        }

        public override void OnStateExit()
        {
            _col.enabled = false;
        }

        public override void StateUpdate()
        {
            if (Damaged())
            {
                if (_stats.CurrentHealth <= 0)
                {
                    _anim.SetTrigger(DeathHash);
                    _stateMachine.ChangeState<EDeathState>();
                }
                else
                {
                    _stateMachine.ChangeState<EDamagedState>();
                }
                return;
            }

            PositionUpdate();
        }


        private void PositionUpdate()
        {
            var newPos = _enemyParams.CalculatedPosition;
            var dir = newPos - _enemyParams.CurrentPosition;
            dir.Normalize();

            var newRot = dir == Vector3.zero ?
                _stateMachine.transform.rotation : Quaternion.LookRotation(dir, Vector3.up);


            _stateMachine.transform.SetPositionAndRotation(newPos, newRot);


            _enemyParams.CurrentPosition = newPos;
        }

        private bool Damaged()
        {
            return _currentHealth != _stats.CurrentHealth;
        }
    }
}
