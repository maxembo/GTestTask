using UnityEngine;

namespace Player
{
    public class PlayerView
    {
        private Animator _animator;
        private const string Movement = "Movement";

        public PlayerView(Animator animator) => _animator = animator;

        public void SetMovementType(MovementType type) => _animator.SetInteger(Movement, (int)type);
    }
}
