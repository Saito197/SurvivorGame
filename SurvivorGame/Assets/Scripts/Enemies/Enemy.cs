using SaitoGames.SurvivorGame.Character;
using SaitoGames.Utilities;

namespace SaitoGames.SurvivorGame.Enemies
{
    public abstract class Enemy : StateMachine, IDamagable
    {
        public abstract void TakeDamage();
    }
}
