using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Weapon
{
    public class OrbittingBullet : Bullet
    {
        public float OrbitRadius;
        public float CurrentAngle;
        private Transform _target;

        public void SetTarget(Transform target) => _target = target;

        protected override void LifespanCheck(float delta)
        {
        }

        protected override void UpdatePosition(float delta)
        {
            CurrentAngle += Speed * delta;
            CurrentAngle = Mathf.Repeat(CurrentAngle, 360f);

            var offsetPos = _target.position + Vector3.right * OrbitRadius;
            var pos = MathUtilities.GetAngledVector(_target.position, offsetPos, CurrentAngle);

            transform.position = pos;
        }
    }
}
