using SaitoGames.Utilities;
using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    [Serializable]
    public class MovementParameters
    {
        public Vector2 Direction;
        public float MoveSpeed;
        public float RotationSpeed;
    }

    [Serializable]
    public class ActionParameters
    {

    }

    public class CStandardControlState : State
    {
        private readonly Rigidbody _rb;
        private readonly Animator _animator;
        private readonly MovementParameters _moveParams;
        private readonly ActionParameters _actionParams;

        private readonly int MovementHash = Animator.StringToHash("Movement");
        private readonly int AttackHash = Animator.StringToHash("Attack");

        public CStandardControlState(
            StateMachine stateMachine,
            Rigidbody rb,
            Animator animator,
            MovementParameters moveParams,
            ActionParameters actionParams
        ) : base(stateMachine)
        {
            _rb = rb;
            _animator = animator;
            _moveParams = moveParams;
            _actionParams = actionParams;
        }

        public override void StateFixedUpdate()
        {
            var dir = _moveParams.Direction;
            _animator.SetFloat(MovementHash, dir.magnitude);

            if (dir == Vector2.zero)
                return;


            var delta = Time.fixedDeltaTime;
            var moveSpeed = _moveParams.MoveSpeed * delta;
            var rotSpeed = _moveParams.RotationSpeed * delta;

            // Get move direction based on X-Z plane
            var moveDir = dir.RemapToXZPlane(_stateMachine.transform.position.y);
            moveDir = Camera.main.transform.TransformDirection(moveDir).With(y: 0f);
            // Find rotation direction
            var rotDestination = Quaternion.LookRotation(moveDir.With(y: 0f), Vector3.up);

            // Moves character forward and rotates the character based on input
            var newPos = _rb.position + (moveSpeed * _stateMachine.transform.forward);
            var newRot = Quaternion.Slerp(_rb.rotation, rotDestination, rotSpeed);
            _rb.Move(newPos, newRot);

        }

        public override void OnStateExit()
        {
            // Resets the animator when exiting the state
            _animator.SetFloat(MovementHash, 0f);
        }

        
    }
}