using SaitoGames.SurvivorGame.Enemies;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.GameState
{
    [CreateAssetMenu]
    public class EnemyWave : ScriptableObject
    {
        [Serializable]
        public struct WaveData
        {
            public EnemyData Enemy;
            public int MinSpawn;
        }

        public List<WaveData> WaveEnemies;
        public float SpawnInterval;
    }
}