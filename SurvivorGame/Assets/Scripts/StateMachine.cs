using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State _currentState;
        protected List<State> _states;

        protected void Update()
        {
            CheckForStateRequest();
            _currentState?.StateUpdate();
        }

        protected void CheckForStateRequest()
        {
/*
            // Function to check if the current State is requesting a state change. 
            var stateRequest = _currentState?.StateRequest;
            if (stateRequest == null) return;
            if (stateRequest != typeof(State)) return;

            foreach (var state in _states)
            {
                if (state.GetType() == stateRequest)
                {
                    _currentState = state;
                    return;
                }
            }
        */
        }
    }
}