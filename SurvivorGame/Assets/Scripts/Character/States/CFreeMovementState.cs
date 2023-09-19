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

    public class CFreeMovementState : State
    {
        private readonly Rigidbody _rb;
        private readonly MovementParameters _moveParams;

        public CFreeMovementState(
            StateMachine stateMachine,
            Rigidbody rb,
            MovementParameters moveParams
        ) : base(stateMachine)
        {
            _rb = rb;
            _moveParams = moveParams;
        }

        public override void StateFixedUpdate()
        {
            //_rb.velocity = Vector3.zero.With(y: _rb.velocity.y);
            var dir = _moveParams.Direction;
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
    }
}