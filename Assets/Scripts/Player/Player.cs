using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] public float speed;
        [SerializeField] private PlayerView view;

        private Movement _movement;
        private PlayerInput _input;
        private Rigidbody2D _rigidbody;
        private Vector2 _direction;

        public Vector2 Direction => _direction;
        public Rigidbody2D Rigidbody => _rigidbody;
        public float Speed => speed;
        private void Awake()
        {
            _input = new PlayerInput();
            view.Initialize();
            _rigidbody = GetComponent<Rigidbody2D>();
            _movement = new Movement(this);
        }

        private void FixedUpdate()
        {
            _movement.Move();
            _movement.Flip();

            UpdateMovementType();
        }

        private void UpdateMovementType()
            => view.SetMovementType(_movement.IsInputZero() ? MovementType.Idling : MovementType.Moving);

        private void OnMoveInput(InputAction.CallbackContext context)
            => _direction = context.ReadValue<Vector2>();

        private void OnEnable()
        {
            _input.Enable();
            _input.Gameplay.Move.performed += OnMoveInput;
            _input.Gameplay.Move.canceled += OnMoveInput;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Gameplay.Move.performed -= OnMoveInput;
            _input.Gameplay.Move.canceled -= OnMoveInput;
        }
    }
}
