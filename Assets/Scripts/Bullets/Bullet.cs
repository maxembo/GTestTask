using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float _speed;
        [SerializeField, Range(0, 10)] private float _radius;

        public SpriteRenderer Renderer;

        private Vector2 _direction;
        private Vector3 _position => transform.position;

        private void Start() => Destroy(gameObject, 5f);

        private void Update() => Move();

        public void SetDirection(Vector3 direction) => _direction = direction;

        public void SetScale(Vector3 localScale) => transform.localScale = localScale;

        private void Move() => transform.Translate(_direction.normalized * (_speed * Time.deltaTime));

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" || collision.tag == "Bullet") return;

            var damageHits = Physics2D.OverlapCircleAll(_position, _radius);

            foreach (var hit in damageHits)
                if (hit) Destroy(hit.gameObject);
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_position, _radius);
        }
    }
}
