using SaitoGames.SurvivorGame.Weapon;
using SaitoGames.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Character
{
    public class Character : StateMachine, IDamagable, IControlable
    {
        [SerializeField] private CharacterInfo _character;
        [SerializeField] private Transform _meshContainer;
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private CharacterParameters _characterParams;
        [SerializeField] private List<Weapons> _weapons;

        public void ActionCommand(CharacterAction action)
        {
            _characterParams.CurrentAction = action;
        }

        public void ValueUpdateCommand(ControllerValue value, bool isPressed)
        {
            switch (value)
            {
                case ControllerValue.None:
                    break;
                case ControllerValue.Strafe:
                    _characterParams.Strafe = isPressed;
                    break;
                default:
                    break;
            }
        }

        public void MovementCommand(Vector2 direction)
        {
            _characterParams.MoveDirection = direction;
        }

        public void LookDirectionCommand(Vector2 direction)
        {
            _characterParams.LookDirection = direction;
        }

        public void TakeDamage()
        {
        }

        private void Awake()
        {
            // Load character mesh 
            var character = Instantiate(_character.CharacterPrefab, _meshContainer);
            var anim = character.GetComponent<Animator>();

            // Load default weapon
            _weapons.Add(_character.DefaultWeapon);

            // Load parameters 
            _characterParams = new CharacterParameters(_character.DefaultParameters);

            // Define states to be used and initializing state machines
            var initialState = new CStandardControlState(
                this, 
                _rigidBody, 
                anim, 
                _characterParams
            );
            var states = new List<State>
            {
                initialState,
                new CDamagedState(this),
                new CDodgingState(this)
            };
            StateMachineInit(initialState, states);

            
            // Initialize weapons
            foreach (var w in _weapons)
            {
                w.InitializeWeapon();
            }
        }

        protected override void Update()
        {
            foreach (var weapon in _weapons)
            {
                weapon.WeaponUpdate(transform, Time.deltaTime);
            }
            base.Update();
        }
    }
}