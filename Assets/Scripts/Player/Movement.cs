using UnityEngine;

namespace Player
{
    public class Movement
    {
        private Player _player;
        public Movement(Player player) => _player = player;
        private Rigidbody2D Rigidbody => _player.Rigidbody;
        private Vector2 Direcition => _player.Direction;
        private float Speed => _player.Speed;
        private Transform Transform => Rigidbody.transform;

        public void Move() => Rigidbody.AddForce(Direcition * (Speed * Time.deltaTime), ForceMode2D.Impulse);

        public void Flip()
        => Transform.rotation =
            (Direcition.x < 0) ? GetRotateRight() :
            (Direcition.x > 0) ? GetRotateLeft() :
            Transform.rotation;

        public bool IsInputZero() => Direcition == Vector2.zero;
        private Quaternion GetRotateLeft() => Quaternion.Euler(0, 0, 0);
        private Quaternion GetRotateRight() => Quaternion.Euler(0, 180, 0);
    }
}
