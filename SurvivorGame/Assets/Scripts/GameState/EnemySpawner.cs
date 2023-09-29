using SaitoGames.SurvivorGame.Enemies;
using SaitoGames.Utilities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace SaitoGames.SurvivorGame.GameState
{
    public class EnemySpawner : MonoBehaviour
    {
        public int WaveIndex { get; private set; }
        [SerializeField] private FloatVariableAsset _cameraDistance;
        [SerializeField] private float _spawnDistance = 20f;

        [SerializeField] private List<EnemyWave> _waves;
        [SerializeField] private Enemy _stalkerTypePrefab;
        [SerializeField] private Enemy _fixedDirectionPrefab;

        private EnemyController _stalkerController;

        private Dictionary<EnemyType, Tuple<ObjectPooler<Enemy>, EnemyController>> _enemyTypeTable;

        [SerializeField] private float _spawnInterval = 10f;

        private bool __init = false;

        public void InitializeSpawner()
        {

            // Enemy pools 
            _stalkerController = FindObjectOfType<StalkerTypeController>();
            var stalkerTypePool = new ObjectPooler<Enemy>();
            stalkerTypePool.InitPooler(_stalkerTypePrefab, _stalkerController.transform, 100);

            //var fixedDirTypePool = new ObjectPooler<Enemy>();

            var stalkerTuple = new Tuple<ObjectPooler<Enemy>, EnemyController>(
                stalkerTypePool,
                _stalkerController
            );

            _enemyTypeTable = new Dictionary<EnemyType, Tuple<ObjectPooler<Enemy>, EnemyController>>
            {
                { EnemyType.Stalker, stalkerTuple }
            };

            __init = true;
        }

        public void SpawnIntervalUpdate(float delta)
        {
            if (!__init) return;

            _spawnInterval -= delta;
            if ( _spawnInterval <= 0f )
            {
                var wave = (WaveIndex < _waves.Count) ? _waves[WaveIndex] : _waves[_waves.Count - 1];
                _spawnInterval = wave.SpawnInterval;

                foreach (var e in wave.WaveEnemies)
                {
                    Spawn(e.Enemy);
                }
            }
        }

        public void SpawnNewWave(int wave)
        {
            if (!__init) return;
            WaveIndex = wave;
            
            var currentWave = (WaveIndex < _waves.Count) ? _waves[WaveIndex] : _waves[_waves.Count - 1];

            foreach (var enemy in currentWave.WaveEnemies)
            {
                Spawn(enemy.Enemy, enemy.MinSpawn);
            }
            _spawnInterval = currentWave.SpawnInterval;
        }

        private void Spawn(EnemyData enemy, int count = 1)
        {
            if (!__init) return;
            var cam = Camera.main.transform;
            var dir = cam.forward;
            var camPos = cam.position;
            var target = camPos + (dir * _cameraDistance.Value);

            var pool = _enemyTypeTable[enemy.EnemyType].Item1;
            var controller = _enemyTypeTable[enemy.EnemyType].Item2;
            for (int i = 0; i < count; i++)
            {
                // Generate randomized spawn position
                var xCoord = target.x + (MathUtilities.CoinFlip() ? -_spawnDistance : _spawnDistance);
                var zCoord = UnityEngine.Random.Range(target.z - 10f, target.z + 10f);
                
                xCoord = Mathf.Clamp(xCoord, -95f, 95f);
                zCoord = Mathf.Clamp(zCoord, -20f, 20f);
                var pos = new Vector3(xCoord, 0f, zCoord);

                // Spawn object
                var e = pool.GetNextObject(false);
                e.transform.position = pos;
                e.SetEnemyData(enemy);
                e.ReturnEvent += EnemyDespawned;
                e.gameObject.SetActive(true);

                controller.AddEnemy(e);
            }
        }

        private void EnemyDespawned(GameObject enemy)
        {
            if (!__init) return;
            if (enemy.TryGetComponent<Enemy>(out var e))
            {
                e.ReturnEvent -= EnemyDespawned;
                var eType = e.GetEnemyType();
                var controller = _enemyTypeTable[eType].Item2;
                controller.RemoveEnemy(e);
            }
        }
    }
}