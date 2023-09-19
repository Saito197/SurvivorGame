using System;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public abstract class State 
    {
        public Type StateRequest { get; set; } = null;
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