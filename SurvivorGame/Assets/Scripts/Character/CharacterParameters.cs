using System;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Character
{
    [Serializable]
    public class CharacterParameters
    {
        public CharacterParameters(CharacterParameters parameters)
        {
            MoveSpeed = parameters.MoveSpeed;
            RotationSpeed = parameters.RotationSpeed;
            Health = parameters.Health;
            MaxHealth = parameters.MaxHealth;
            Defense = parameters.Defense;
            CritRate = parameters.CritRate;
        }

        // Movements
        public Vector2 MoveDirection;
        public Vector2 LookDirection;
        public bool Strafe;

        // Stats
        public float MoveSpeed;
        public float RotationSpeed;
        public float Health = 100f;
        public float MaxHealth = 100f;
        public float Defense = 1f;
        public float CritRate = 1f;

        // Actions
        public CharacterAction CurrentAction;
    }
}