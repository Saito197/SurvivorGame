using UnityEngine;

namespace SaitoGames.SurvivorGame.Character
{
    public abstract class Controller : MonoBehaviour
    {
        public GameObject ControlTargetObject;
        protected IControlable _targetObject;

        protected virtual void Awake()
        {
            _targetObject = ControlTargetObject.GetComponent<IControlable>();
        }
    }
}