using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public class CDamagedState : State
    {
        public CDamagedState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void ActionCommand(CharacterAction action)
        {
            throw new System.NotImplementedException();
        }

        public override void DirectionCommand(Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        public override void StateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}