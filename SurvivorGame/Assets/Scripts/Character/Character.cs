using SaitoGames.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public class Character : StateMachine, IDamagable, IControlable
    {
        [SerializeField] private Transform MeshContainer;
        [SerializeField] private Rigidbody _rigidBody;

        public void ActionCommand(CharacterAction action)
        {
        }

        public void DirectionCommand(Vector2 direction)
        {
        }

        public void TakeDamage()
        {
        }

        private void Awake()
        {
            // Define states to be used
            var initialState = new CFreeMovementState(this, _rigidBody);
            var states = new List<State>
            {
                initialState,
                new CAttackingState(this),
                new CDamagedState(this),
                new CDodgingState(this)
            };

            StateMachineInit(initialState, states);
        }
    }
}