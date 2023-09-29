using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Weapon
{
    [CreateAssetMenu(menuName = "Weapons/LeafShield")]
    public class LeafShield : Weapons
    {
        [SerializeField] private List<ProjectileParameter> _parameters;
        private List<OrbittingBullet> _bullets;

        public override void InitializeWeapon()
        {
            base.InitializeWeapon();
            _bullets = new List<OrbittingBullet>();
        }

        public override void WeaponUpdate(Transform user, float delta)
        {
            var parameters = _parameters[CurrentLevel];

            if (_bullets.Count < parameters.BulletDirection.Length)
            {
                var bullet = (OrbittingBullet)_bulletPool.GetNextObject(false);
                bullet.SetTarget(user);
                _bullets.Add(bullet);
                bullet.gameObject.SetActive(true);

                for (int i = 0; i < _bullets.Count; i++)
                {
                    _bullets[i].Damage = parameters.Damage;
                    _bullets[i].Speed = parameters.Speed;
                    _bullets[i].HitDelay = parameters.HitDelay;
                    _bullets[i].OrbitRadius = parameters.BulletDirection[0].x;
                    _bullets[i].CurrentAngle = i * 360 / _bullets.Count;
                }
            }
        }
    }

}
