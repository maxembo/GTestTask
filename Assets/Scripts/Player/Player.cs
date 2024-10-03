using Configs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private Animator _animator;

        private Rigidbody2D _rigidbody;
        private Vector2 _direction;

        private PlayerView _view;
        private Movement _movement;
        private PlayerInput _input;

        private InputAction InputMove => _input.Gameplay.Move;

        private void Awake()
        {
            _input = new PlayerInput();
            _rigidbody = GetComponent<Rigidbody2D>();
            _view = new PlayerView(_animator);
            _movement = new Movement(_config);
        }

        private void Start() => _view.SetMovementType(MovementType.Idling);

        private void FixedUpdate()
        {
            _movement.Move(_rigidbody, _direction);
            transform.rotation = _movement.GetRotation(_direction);

            UpdateMovementType();
        }

        private void UpdateMovementType()
            => _view.SetMovementType(_movement.IsDirectionZero(_direction) ? MovementType.Idling : MovementType.Moving);

        private void OnMoveInput(InputAction.CallbackContext context)
            => _direction = context.ReadValue<Vector2>();

        private void OnEnable()
        {
            _input.Enable();
            InputMove.performed += OnMoveInput;
            InputMove.canceled += OnMoveInput;
        }

        private void OnDisable()
        {
            _input.Disable();
            InputMove.performed -= OnMoveInput;
            InputMove.canceled -= OnMoveInput;
        }
    }
}
