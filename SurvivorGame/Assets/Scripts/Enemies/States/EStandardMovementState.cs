using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    public class EStandardMovementState : State
    {
        private Animator _anim;
        private EnemyMovementParameters _moveParams;

        public EStandardMovementState(
            StateMachine stateMachine,
            Animator anim,
            ref EnemyMovementParameters movementParameters
        ) : base(stateMachine)
        {
            _anim = anim;
            _moveParams = movementParameters;
        }

        public override void StateUpdate()
        {
        }
    }
}
