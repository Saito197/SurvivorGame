using SaitoGames.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public class Character : StateMachine, IDamagable, IControlable
    {
        [SerializeField] private CharacterInfo _character;
        [SerializeField] private Transform _meshContainer;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private MovementParameters _movementParameters;

        public void ActionCommand(CharacterAction action)
        {
            switch (action)
            {
                case CharacterAction.Dodge:
                    break;
                case CharacterAction.Attack:
                    break;
                case CharacterAction.Special1:
                    break;
                case CharacterAction.Special2:
                    break;
                default:
                    break;
            }

        }

        public void DirectionCommand(Vector2 direction)
        {
            _movementParameters.Direction = direction;
        }

        public void TakeDamage()
        {
        }

        private void Awake()
        {
            var character = Instantiate(_character.CharacterPrefab, _meshContainer);
            var anim = character.GetComponent<Animator>();

            // Define states to be used
            var initialState = new CStandardControlState(this, _rigidBody, anim, _movementParameters);
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