using System;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public delegate void StateChangeEventHandler(State newState);

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