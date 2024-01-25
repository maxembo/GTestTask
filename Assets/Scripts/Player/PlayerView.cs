using UnityEngine;

namespace Player
{
    public enum MovementType
    {
        Idling,
        Moving
    }

    [RequireComponent(typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        private Animator _animator;
        private const string Movement = "Movement";

        public void Initialize() => _animator = GetComponent<Animator>();

        public void SetMovementType(MovementType type) => _animator.SetInteger(Movement, (int)type);
    }
}
