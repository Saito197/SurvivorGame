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
            _currentState?.StateUpdate();
        }

        protected void CheckForStateRequest(Type newState)
        {

            // Function to check if the current State is requesting a state change. 
            if (newState != typeof(State)) return;

            foreach (var state in _states)
            {
                if (state.GetType() == newState)
                {
                    _currentState = state;
                    return;
                }
            }

        }
    }
}