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

        public State(StateMachine stateMachine)
        { 
            _stateMachine = stateMachine;
        }

        public abstract void StateUpdate();
        public abstract void ActionCommand(CharacterAction action);
        public abstract void DirectionCommand(Vector2 direction);
    }
}