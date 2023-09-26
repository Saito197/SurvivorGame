using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Weapon
{
    public class Bullet : PooledObject
    {
        public float Damage { get; set; }
        public float Speed { get; set; }
        public float LifeSpan { get; set; }

        private void Update()
        {
            LifeSpan -= Time.deltaTime;
            if (LifeSpan < 0)
            {
                gameObject.SetActive(false);
                return;
            }
        }

        private void FixedUpdate()
        {
            UpdatePosition(Time.fixedDeltaTime);
        }

        private void UpdatePosition(float delta)
        {
            var nextPos = delta * Speed * transform.forward;
            transform.position += nextPos;
        }
    }
}
