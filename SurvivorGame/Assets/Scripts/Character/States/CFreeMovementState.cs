using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public class CFreeMovementState : State
    {
        private Rigidbody _rb;

        public CFreeMovementState(StateMachine stateMachine, Rigidbody rb) : base(stateMachine)
        {
        }

        public override void ActionCommand(CharacterAction action)
        {
        }

        public override void DirectionCommand(Vector2 direction)
        {
        }

        public override void StateUpdate()
        {
        }
    }
}