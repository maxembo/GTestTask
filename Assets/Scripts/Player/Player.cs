using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;
        [SerializeField] private PlayerView view;

        private PlayerInput _input;
        private Rigidbody2D _rigidbody;
        private Vector2 _direction;

        private void Awake()
        {
            _input = new PlayerInput();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rigidbody.AddForce(_direction * (config.Speed * Time.deltaTime), ForceMode2D.Impulse);
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
        }

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
