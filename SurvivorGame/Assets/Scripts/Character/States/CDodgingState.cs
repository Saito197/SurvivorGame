using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Character
{
    public class CDodgingState : State
    {
        private Animator _anim;

        public CDodgingState(StateMachine stateMachine, Animator anim) : base(stateMachine)
        {
            _anim = anim;
        }
    }
}