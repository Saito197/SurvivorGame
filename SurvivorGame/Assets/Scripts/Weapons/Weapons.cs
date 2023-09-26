using Autodesk.Fbx;
using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Weapon
{
    public abstract class Weapons : ScriptableObject
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _maxBulletCount;

        protected ObjectPooler<Bullet> _bulletPool;
        protected float _cooldown;

        public virtual void InitializeWeapon()
        {
            var poolObjectContainer = new GameObject($"{name} bullet pool");
            _bulletPool = new ObjectPooler<Bullet>();
            _bulletPool.InitPooler(_bulletPrefab, poolObjectContainer.transform, _maxBulletCount);
        }

        public abstract void WeaponUpdate(Transform user, float delta);
    }
}
