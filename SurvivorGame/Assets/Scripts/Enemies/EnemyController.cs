using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    public abstract class EnemyController : MonoBehaviour
    {
        public abstract void AddEnemy(Enemy enemy);

        public abstract void RemoveEnemy(Enemy enemy);
    }
}
