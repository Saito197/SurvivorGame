using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.GameState
{
    public class GGamePausedState : State
    {
        public GGamePausedState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void OnStateEnter()
        {
            Time.timeScale = 0f;
        }

        public override void OnStateExit()
        {
            Time.timeScale = 1f;
        }
    }
}