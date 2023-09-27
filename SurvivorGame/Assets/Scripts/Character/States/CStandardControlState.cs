using SaitoGames.Utilities;
using TMPro.EditorUtilities;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Character
{

    public class CStandardControlState : State
    {
        private readonly Rigidbody _rb;
        private readonly Animator _animator;
        private readonly CharacterParameters _characterParams;

        private readonly int MovementHash = Animator.StringToHash("Movement");

        public CStandardControlState(
            StateMachine stateMachine,
            Rigidbody rb,
            Animator anim,
            CharacterParameters cParams
        ) : base(stateMachine)
        {
            _rb = rb;
            _animator = anim;
            _characterParams = cParams;
        }

        public override void StateFixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }

        public override void StateUpdate()
        {
            switch (_characterParams.CurrentAction)
            {
                case CharacterAction.Dodge:
                    break;
                case CharacterAction.Special1:
                    break;
                case CharacterAction.Special2:
                    break;
            }
            _characterParams.CurrentAction = CharacterAction.None;
        }

        public override void OnStateExit()
        {
            // Resets the animator when exiting the state
            _animator.SetFloat(MovementHash, 0f);
        }

        private void HandleMovement()
        {
            _animator.SetFloat(MovementHash, _characterParams.MoveDirection.magnitude);
            if (_characterParams.MoveDirection == Vector2.zero)
                return;

            var dir = _characterParams.MoveDirection.RemapToXZPlane(_rb.position.y);

            var delta = Time.fixedDeltaTime;
            var moveSpeed = _characterParams.MoveSpeed;

            // Moves character
            var newPos = _rb.position + (delta * moveSpeed * dir);
            //var newRot = Quaternion.Slerp(_rb.rotation, rotDestination, rotSpeed);
            _rb.MovePosition(newPos);
        }

        private void HandleRotation()
        {
            var dir = _characterParams.LookDirection;
            var delta = Time.fixedDeltaTime;
            var rotSpeed = _characterParams.RotationSpeed * delta;

            if (_characterParams.Strafe || dir != Vector2.zero)
            {
                TwinStickRotation(dir, rotSpeed);
            }
            else
            {
                ForwardRotation(rotSpeed);
            }

        }

        private void ForwardRotation(float rotSpeed)
        {
            var moveDir = _characterParams.MoveDirection;
            if (moveDir == Vector2.zero)
                return;

            TwinStickRotation(moveDir, rotSpeed);
        }

        private void TwinStickRotation(Vector2 dir, float rotSpeed)
        {
            if (dir == Vector2.zero)
                return;

            // Rotates the character 
            var rotDir = Camera.main.transform.TransformDirection(dir).With(y: 0f);
            var targetRot = Quaternion.LookRotation(rotDir.With(y: 0f), Vector3.up);
            var newRot = Quaternion.Slerp(_rb.rotation, targetRot, rotSpeed);
            _rb.MoveRotation(newRot);
        }
    }
}