using SaitoGames.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public class Character : StateMachine, IDamagable, IControlable
    {
        public Vector2 Direction { get; private set; }

        [SerializeField] private Transform MeshContainer;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private MovementParameters _movementParameters;

        public void ActionCommand(CharacterAction action)
        {
        }

        public void DirectionCommand(Vector2 direction)
        {
            Direction = direction;
        }

        public void TakeDamage()
        {
        }

        private void Awake()
        {
            // Define states to be used
            var initialState = new CFreeMovementState(this, _rigidBody, _movementParameters);
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