using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.GameState
{
    public class GGameActiveState : State
    {
        private FloatVariableAsset _playtime;

        public GGameActiveState(
            StateMachine stateMachine,
            FloatVariableAsset playtime
        ) : base(stateMachine)
        {
            _playtime = playtime;
        }

        public override void StateUpdate()
        {
            _playtime.Value += Time.deltaTime;
        }
    }
}