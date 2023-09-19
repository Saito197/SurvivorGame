using System;
using UnityEngine;

namespace SaitoGames.Utilities
{
    public enum CharacterAction
    {
        // Define a list of input action/commands 
        Dodge, 
        Attack,
        Special1,
        Special2
    }

    public delegate void StateChangeEventHandler(Type newState);

    public abstract class State 
    {
        public event StateChangeEventHandler StateChange;

        private StateMachine _stateMachine;

        // Base Constructor to get reference to the main StateMachine
        // Define a different constructor to pass in extra parameters when inheriting the state class.
        public State(StateMachine stateMachine)
        { 
            _stateMachine = stateMachine;
        }

        public void OnStateEnter() { }
        public void OnStateExit() { }
        public abstract void StateUpdate();


        public abstract void ActionCommand(CharacterAction action);
        public abstract void DirectionCommand(Vector2 direction);
    }
}