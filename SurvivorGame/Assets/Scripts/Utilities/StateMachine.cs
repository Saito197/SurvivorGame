using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaitoGames.Utilities
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State _currentState;
        protected List<State> _states;

        public void ChangeState<T>() where T: State
        {
            // Handles when the current State requests a state change. 
            var targetState = _states.FirstOrDefault(s => s is T);
            if (targetState == null)
                return;

            _currentState.OnStateExit();
            _currentState = targetState;
            _currentState.OnStateEnter();
        }

        protected void StateMachineInit(State initialState, List<State> states)
        {
            _currentState = initialState;
            _states = states;

            _currentState.OnStateEnter();
        }

        protected virtual void Update()
        {
            if (_currentState == null) return;
            _currentState.StateUpdate();
        }

        protected virtual void FixedUpdate()
        {
            if (_currentState == null) return;
            _currentState.StateFixedUpdate();
        }

    }

}