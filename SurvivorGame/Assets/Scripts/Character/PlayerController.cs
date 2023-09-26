using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using static UnityEngine.Rendering.DebugUI;

namespace SaitoGames.SurvivorGame.Character
{
    public class PlayerController : Controller
    {
        public void OnMove(InputValue value)
        {
            var dir = value.Get<Vector2>();
            _targetObject.MovementCommand(dir);
        }

        public void OnLook(InputValue value)
        {
            var dir = value.Get<Vector2>();
            _targetObject.LookDirectionCommand(dir);
        }

        public void OnStrafe(InputValue value)
        {
            _targetObject.ValueUpdateCommand(ControllerValue.Strafe, value.isPressed);
        }

        public void OnDodge() 
        {
            _targetObject.ActionCommand(CharacterAction.Dodge);
        }
    }
}