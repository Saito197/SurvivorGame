using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.GameState
{
    public class DropItemSpawner : MonoBehaviour
    {
        [SerializeField] private DefaultPooledOjbect _expDropPrefab;
        [SerializeField] private GameEvent _enemyDefeatedEvent;
        
        private ObjectPooler<DefaultPooledOjbect> _expDrops;

        private void Awake()
        {
            _enemyDefeatedEvent.Response += EnemyDefeated;
            // Drop item pools 
            _expDrops = new ObjectPooler<DefaultPooledOjbect>();
            _expDrops.InitPooler(_expDropPrefab, transform, 100);
        }

        private void OnDestroy()
        {
            _enemyDefeatedEvent.Response -= EnemyDefeated;
        }

        private void EnemyDefeated(object[] args)
        {
            if (args == null || args.Length == 0)
                return;

            var pos = (Vector3)args[0];
            var exp = _expDrops.GetNextObject();
            exp.transform.position = pos;
        }

    }
}