using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public enum CharacterAction
    {
        Attack = 0,
        Dodge = 1
    }

    public class Character : StateMachine, IDamagable, IControlable
    {
        [SerializeField] private Transform MeshContainer;

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
            _states = new List<State>();
            _states.Add(new CFreeMovementState(this));
        }


    }
}