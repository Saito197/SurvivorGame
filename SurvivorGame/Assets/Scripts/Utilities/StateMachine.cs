using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.Utilities
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State _currentState;
        protected List<State> _states;

        protected void StateMachineInit(State initialState, List<State> states)
        {
            _currentState = initialState;
            _states = states;

            _currentState.StateChange += CheckForStateRequest;
            _currentState.OnStateEnter();
        }

        private void OnDestroy()
        {
            if (_currentState != null ) 
            { 
                _currentState.StateChange -= CheckForStateRequest;
            }
        }

        protected void Update()
        {
            if (_currentState == null) return;
            _currentState.StateUpdate();
        }

        protected void FixedUpdate()
        {
            if (_currentState == null) return;
            _currentState.StateFixedUpdate();
        }

        protected void CheckForStateRequest(Type newState)
        {
            // Handles when the current State requests a state change. 
            if (newState != typeof(State)) return;

            foreach (var state in _states)
            {
                if (state.GetType() == newState)
                {
                    // Unregistering the old state and call exit function
                    _currentState.StateChange -= CheckForStateRequest;
                    _currentState.OnStateExit();

                    // Registers the new state
                    _currentState = state;
                    _currentState.StateChange += CheckForStateRequest;
                    _currentState.OnStateEnter();
                    return;
                }
            }

        }
    }
}