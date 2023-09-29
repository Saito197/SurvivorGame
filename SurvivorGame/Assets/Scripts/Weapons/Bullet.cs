using SaitoGames.SurvivorGame.Character;
using SaitoGames.SurvivorGame.Enemies;
using SaitoGames.Utilities;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;

namespace SaitoGames.SurvivorGame.Weapon
{
    public class Bullet : MonoBehaviour, IPooledObject
    {
        public float Damage { get; set; }
        public float Speed { get; set; }
        public float LifeSpan { get; set; }
        public float HitDelay { get; set; }

        public ReturnPoolEventHandler ReturnEvent { get; set; }

        public GameObject GameObject => gameObject;

        [SerializeField] protected float _hitDelay;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _enemyLayer;

        protected virtual void Update()
        {
            var delta = Time.deltaTime;

            if (_hitDelay > 0)
                _hitDelay -= delta;

            LifespanCheck(delta);
        }

        protected virtual void FixedUpdate()
        {
            DamageCheck();
            UpdatePosition(Time.fixedDeltaTime);
        }

        protected virtual void UpdatePosition(float delta)
        {
            var nextPos = delta * Speed * transform.forward;
            transform.position += nextPos;
        }

        protected virtual void DamageCheck()
        {
            if (_hitDelay > 0)
                return;

            var pos = transform.position;
            var dir = transform.forward;
            var distance = Time.fixedDeltaTime * Speed;

            var hits = Physics.SphereCastAll(pos, _radius, dir, distance, _enemyLayer);

            if (hits.Length == 0)
                return;

            foreach (var hit in hits)
            {
                var enemy = hit.collider.GetComponent<IDamagable>();
                enemy.TakeDamage(Damage);
            }

            _hitDelay = HitDelay;
        }

        protected virtual void LifespanCheck(float delta)
        {
            LifeSpan -= delta;
            if (LifeSpan < 0)
            {
                gameObject.SetActive(false);
                return;
            }
        }

        private void OnDisable()
        {
            ReturnEvent?.Invoke(gameObject);
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
#endif
    }
}
