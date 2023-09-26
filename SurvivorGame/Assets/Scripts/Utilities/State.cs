using System;
using UnityEngine;

namespace SaitoGames.Utilities
{
    public abstract class State 
    {
        protected StateMachine _stateMachine;

        // Base Constructor to get reference to the main StateMachine
        // Define a different constructor to pass in extra parameters when inheriting the state class.
        // For example, to define a movement state that requires a rigidbody component:
        //
        // public MovementState(StateMachine stateMachine, Rigidbody rb) : base(stateMachine)
        //
        // Then pass the RB component to the constructor when Initializing the State Machine
        // Call _stateMachine.ChangeState to send a state changing request 

        public State(StateMachine stateMachine)
        { 
            _stateMachine = stateMachine;
        }

        public virtual void OnStateEnter() { }
        public virtual void OnStateExit() { }
        public virtual void StateUpdate() { }
        public virtual void StateFixedUpdate() { }
    }
}