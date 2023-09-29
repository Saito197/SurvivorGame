using SaitoGames.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Weapon
{
    [CreateAssetMenu(menuName = "Weapons/Projectile")]
    public class Projectile : Weapons
    {
        [SerializeField] private List<ProjectileParameter> _parameters;

        public override void InitializeWeapon()
        {
            base.InitializeWeapon();
            _cooldown = _parameters[0].Cooldown;
        }

        public override void WeaponUpdate(Transform user, float delta)
        {
            _cooldown -= delta;
            if (_cooldown > 0) return;

            var parameters = _parameters[CurrentLevel];
            _cooldown = parameters.Cooldown;
            
            foreach (var b in parameters.BulletDirection)
            {
                var bulletDir = user.TransformDirection(b);
                var bullet = _bulletPool.GetNextObject(false);
                bullet.Damage = parameters.Damage;
                bullet.Speed = parameters.Speed;
                bullet.LifeSpan = parameters.BulletLifeSpan;
                bullet.HitDelay = parameters.HitDelay;
                bullet.transform.position = user.position.Add(y: 1f);
                bullet.transform.rotation = Quaternion.LookRotation(bulletDir, Vector3.up);
                bullet.gameObject.SetActive(true);
            }
        }
    }

}
