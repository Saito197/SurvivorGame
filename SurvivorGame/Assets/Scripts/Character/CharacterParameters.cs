using SaitoGames.Utilities;
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
            CurrentExp = parameters.CurrentExp;
            Health = parameters.Health;
            MaxHealth = parameters.MaxHealth;
            MaxHealthDefault = parameters.MaxHealthDefault;
            Defense = parameters.Defense;
            CritRate = parameters.CritRate;

            Health.Value = MaxHealthDefault;
            MaxHealth.Value = MaxHealthDefault;
            CurrentExp.Value = 0f;
        }

        // Movements
        public Vector2 MoveDirection;
        public Vector2 LookDirection;
        public bool Strafe;

        // Stats
        public float MoveSpeed;
        public float RotationSpeed;
        public FloatVariableAsset CurrentExp;
        public FloatVariableAsset Health;
        public FloatVariableAsset MaxHealth;
        public float MaxHealthDefault;
        public float Defense = 1f;
        public float CritRate = 1f;

        // Actions
        public CharacterAction CurrentAction;
    }
}