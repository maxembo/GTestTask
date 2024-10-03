using Configs;
using UnityEngine;

namespace Player
{
    public class Movement
    {
        private readonly PlayerConfig _config;
        public Movement(PlayerConfig config) => _config = config;

        public void Move(Rigidbody2D rigidbody, Vector2 direction) => rigidbody.AddForce(direction * (_config.Speed * Time.deltaTime), ForceMode2D.Impulse);

        public Quaternion GetRotation(Vector2 direction) => (direction.x < 0) ? GetRotateRight() : GetRotateLeft();

        public bool IsDirectionZero(Vector2 direction) => direction == Vector2.zero;
        private Quaternion GetRotateLeft() => Quaternion.Euler(0, 0, 0);
        private Quaternion GetRotateRight() => Quaternion.Euler(0, 180, 0);
    }
}
