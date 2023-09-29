using SaitoGames.SurvivorGame.Enemies;
using SaitoGames.SurvivorGame.Weapon;
using SaitoGames.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Character
{
    public class Character : StateMachine, IDamagable, IControlable
    {
        [Header("Events")]
        [SerializeField] private GameEvent _upgradeSelectedEvent;


        [Header("Parameters")]
        [SerializeField] private CharacterList _characterList;
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

        public void TakeDamage(float damage)
        {
            var dmg = damage - _characterParams.Defense;
            // Ensure damage is at least 1 so that we don't heal the enemy
            dmg = (dmg >= 1) ? dmg : 1;
            dmg = Mathf.Floor(dmg);

            _characterParams.Health.Value -= dmg;
        }

        private void Awake()
        {
            // Load character mesh 
            var c = _characterList.ActiveCharacter;
            var character = Instantiate(c.CharacterPrefab, _meshContainer);
            var anim = character.GetComponent<Animator>();

            // Load default weapon
            _weapons.Add(c.DefaultWeapon);

            // Load parameters 
            _characterParams = new CharacterParameters(c.DefaultParameters);

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
                new CDodgingState(this, anim)
            };
            StateMachineInit(initialState, states);

            
            // Initialize weapons
            foreach (var w in _weapons)
            {
                w.InitializeWeapon();
            }

            // Event register 
            _upgradeSelectedEvent.Response += HandleWeaponUpgrade;
        }

        private void OnDestroy()
        {
            _upgradeSelectedEvent.Response -= HandleWeaponUpgrade;
        }

        private void HandleWeaponUpgrade(object[] args)
        {
            if (args == null || args.Length == 0) return;

            var w = (Weapons)args[0];
            if (_weapons.Contains(w))
            {
                w.CurrentLevel++;
            }
            else
            {
                _weapons.Add(w);
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

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Enemy":
                    var e = other.GetComponent<Enemy>();
                    var dmg = e.GetAttack();
                    TakeDamage(dmg);
                    break;
                    
                case "EXP":
                    _characterParams.CurrentExp.Value += 2f;
                    other.gameObject.SetActive(false);
                    break;

                default:
                    break;
            }
        }
    }
}