using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    public class EDamagedState : State
    {
        private EnemyParameters _params;
        private float _timer;

        public EDamagedState(
            StateMachine stateMachine,
            EnemyParameters parameters
        ) : base(stateMachine)
        {
            _params = parameters;
        }

        public override void OnStateEnter()
        {
            _timer = _params.Stats.Knockback;
        }

        public override void StateUpdate()
        {
            if (_timer <= 0)
            {
                _stateMachine.ChangeState<EStandardMovementState>();
                return;
            }

            Knockback();
        }

        private void Knockback()
        {
            var delta = Time.deltaTime;
            var dir = -_stateMachine.transform.forward;
            _stateMachine.transform.position += dir * delta;
            _timer -= delta;
        }
    }
}
