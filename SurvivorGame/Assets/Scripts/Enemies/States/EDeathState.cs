using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Enemies
{
    public class EDeathState : State
    {
        private float _despawn;
        private GameEvent _defeatedEvent;

        public EDeathState(
            StateMachine stateMachine,
            GameEvent defeatedEvent
        ) : base(stateMachine)
        {
            _defeatedEvent = defeatedEvent;
        }

        public override void OnStateEnter()
        {
            _defeatedEvent.Raise(new object[] { _stateMachine.transform.position });
            _despawn = 1f;
        }

        public override void StateUpdate()
        {
            _despawn -= Time.deltaTime;

            if (_despawn < 0f)
            {
                _stateMachine.gameObject.SetActive(false);
            }
        }
    }
}
