using SaitoGames.SurvivorGame.Character;
using SaitoGames.Utilities;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    public abstract class Enemy : StateMachine, IDamagable, IPooledObject
    {
        public ReturnPoolEventHandler ReturnEvent { get; set; }

        public GameObject GameObject => gameObject;

        [SerializeField] private FloatVariableAsset _level;
        [SerializeField] private GameEvent _damagedEvent;
        [SerializeField] private EnemyData _data;
        [SerializeField] protected GameEvent _defeatedEvent;
        [SerializeField] protected EnemyStats _stats;
        [SerializeField] protected Collider _collider;

        protected Animator _anim;
        private GameObject _mesh;

        public virtual void TakeDamage(float damage)
        {
            if (_stats == null)
                return;

            var dmg = damage - _stats.Defense;
            // Ensure damage is at least 1 so that we don't heal the enemy
            dmg = (dmg >= 1) ? dmg : 1;
            dmg = Mathf.Floor(dmg);

            _stats.CurrentHealth -= dmg;
            _damagedEvent.Raise(new object[] { transform.position, dmg });
        }

        public void SetEnemyData(EnemyData data)
        {
            _data = data;
        }

        public float GetAttack() => _stats.Attack;

        public EnemyType GetEnemyType() => _data.EnemyType;

        protected virtual void OnEnable()
        {
            if (_data != null)
            {
                var levelValue = (_level.Value - 1) * 0.2f;
                var multiplier = 1 + levelValue;

                _stats = new EnemyStats(_data, multiplier);
                _mesh = Instantiate(_data.Prefab, transform);
                _anim = _mesh.GetComponent<Animator>();
            }
        }

        protected virtual void OnDisable()
        {
            ReturnEvent?.Invoke(gameObject);
            Destroy(_mesh);
            _mesh = null;
        }
    }
}
