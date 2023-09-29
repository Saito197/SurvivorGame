using System;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    public enum EnemyType
    {
        Stalker,
        FixedDirection
    }

    [CreateAssetMenu]
    public class EnemyData : ScriptableObject
    {
        public EnemyType EnemyType;
        public GameObject Prefab;
        public float MaxHealth;
        public float Attack;
        public float Defense;
        public float Speed;
        public float Knockback;
    }

    [Serializable]
    public class EnemyStats
    {
        public float CurrentHealth;
        public float MaxHealth;
        public float Attack;
        public float Defense;
        public float Speed;
        public float Knockback;

        public EnemyStats(EnemyData data, float multiplier)
        {
            CurrentHealth = data.MaxHealth * multiplier;
            MaxHealth = data.MaxHealth * multiplier;
            Attack = data.Attack * multiplier;
            Defense = data.Defense * multiplier;
            Speed = data.Speed;
            Knockback = data.Knockback;
        }
    }
}
