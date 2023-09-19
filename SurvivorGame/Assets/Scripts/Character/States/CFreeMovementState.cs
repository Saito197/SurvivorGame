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

    }
}