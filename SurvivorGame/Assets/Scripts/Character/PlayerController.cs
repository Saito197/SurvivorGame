using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace SaitoGames.SmasherGame.Character
{
    public class PlayerController : Controller
    {
        public void MovementControl(CallbackContext context)
        {
            var dir = context.ReadValue<Vector2>();
            _targetObject.DirectionCommand(dir);
        }

        public void AttackCommand(CallbackContext context)
        {
            _targetObject.ActionCommand(CharacterAction.Attack);
        }

        public void DodgeCommand(CallbackContext context) 
        {
            _targetObject.ActionCommand(CharacterAction.Dodge);
        }
    }
}