using Autodesk.Fbx;
using SaitoGames.Utilities;
using System;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Weapon
{
    [Serializable]
    public struct ProjectileParameter
    {
        public Vector3[] BulletDirection;
        public float Speed;
        public float Damage;
        public float Cooldown;
        public float BulletLifeSpan;
        public float HitDelay;
    }

    public abstract class Weapons : ScriptableObject
    {
        public int CurrentLevel;

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _maxBulletCount;

        protected ObjectPooler<Bullet> _bulletPool;
        protected float _cooldown;

        public virtual void InitializeWeapon()
        {
            CurrentLevel = 0;
            var poolObjectContainer = new GameObject($"{name} bullet pool");
            _bulletPool = new ObjectPooler<Bullet>();
            _bulletPool.InitPooler(_bulletPrefab, poolObjectContainer.transform, _maxBulletCount);
        }

        public abstract void WeaponUpdate(Transform user, float delta);
    }
}
