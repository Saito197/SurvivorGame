using SaitoGames.SurvivorGame.GameState;
using SaitoGames.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SaitoGames.SurvivorGame.Character
{
    public class PlayerController : Controller
    {
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _cameraSmoothing;
        [SerializeField] private FloatVariableAsset _cameraDistance;

        private Vector3 _cameraVelocity;

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

        private void OnValidate()
        {
            _cameraDistance.Value = _cameraOffset.magnitude;
        }

        private void FixedUpdate()
        {
            var cam = Camera.main.transform;
            var target = ControlTargetObject.transform.position;
            var startPos = cam.position;
            var targetPos =  target + _cameraOffset;

            cam.position = Vector3.SmoothDamp(startPos, targetPos, ref _cameraVelocity, _cameraSmoothing);
            _healthBar.transform.position = target;
        }
    }
}