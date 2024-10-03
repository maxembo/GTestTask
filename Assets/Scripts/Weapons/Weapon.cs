using Configs;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        //[SerializeField] protected GameObject bulletPrefab;
        //[SerializeField] protected Transform spawnPoint;
        //[SerializeField, Range(0, 20)] protected float reload;
        //[SerializeField] protected Animator animator;

        [SerializeField] protected WeaponConfig config;

        protected const string Attack = "Attack";
        protected const int Second = 1000;

        protected bool canShoot = true;
        protected Vector3 localScale = Vector3.one;

        protected Camera main;
        protected PlayerInput input;

        protected float angle;
        protected float currentSize;

        protected abstract void Shoot();

        protected abstract void Recharge();

        protected Quaternion GetRotation()
        {
            var targetDirection = GetMousePosition() - (Vector2)transform.position;

            angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0f, 0f, angle);
        }

        protected Vector2 GetMousePosition()
        {
            Vector2 mousePosition = input.Gameplay.FireDirection.ReadValue<Vector2>();
            Vector2 cameraScreenPoint = main.ScreenToWorldPoint(mousePosition);
            return cameraScreenPoint;
        }

        protected Vector3 GetScale()
        {
            localScale.y = CheckCursorPosition() ? -1f : 1f;
            return localScale;
        }

        protected bool CheckCursorPosition() => angle > 90 || angle < -90;

    }
}
