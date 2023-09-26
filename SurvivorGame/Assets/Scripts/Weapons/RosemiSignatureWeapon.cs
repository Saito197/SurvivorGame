using SaitoGames.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Weapon
{
    [Serializable]
    public struct RosemiWeaponParameters 
    {
        public Vector3[] BulletDirection;
        public float Speed;
        public float Damage;
        public float Cooldown;
        public float BulletLifeSpan;
    }

    [CreateAssetMenu(menuName = "Weapons/Rosemi")]
    public class RosemiSignatureWeapon : Weapons
    {
        [SerializeField] private List<RosemiWeaponParameters> _parameters;

        [SerializeField] private int _currentLevel;

        public override void InitializeWeapon()
        {
            base.InitializeWeapon();
            _currentLevel = 0;
            _cooldown = _parameters[0].Cooldown;
        }

        public override void WeaponUpdate(Transform user, float delta)
        {
            _cooldown -= delta;
            if (_cooldown > 0) return;

            var parameters = _parameters[_currentLevel];
            _cooldown = parameters.Cooldown;
            
            foreach (var b in parameters.BulletDirection)
            {
                var bulletDir = user.TransformDirection(b);
                var bullet = _bulletPool.GetNextObject();
                bullet.Damage = parameters.Damage;
                bullet.Speed = parameters.Speed;
                bullet.LifeSpan = parameters.BulletLifeSpan;
                bullet.transform.position = user.position.Add(y: 2f);
                bullet.transform.rotation = Quaternion.LookRotation(bulletDir, Vector3.up);
                bullet.gameObject.SetActive(true);
            }
        }
    }
}
