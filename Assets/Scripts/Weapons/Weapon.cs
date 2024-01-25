using System.Threading.Tasks;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected Transform spawnPoint;
        [SerializeField, Range(0, 20)] protected float reload;
        [SerializeField] protected Animator animator;

        protected const string Attack = "Attack";
        protected const int Second = 1000;

        protected bool canShoot = true;
        protected Vector3 localScale = Vector3.one;

        protected Camera main;
        protected PlayerInput input;

        protected float angle;
        protected float currentSize;

        protected abstract void Shoot();

        protected abstract Task Recharge();

        protected abstract Quaternion GetRotation();

        protected abstract Vector3 GetScale();

        protected bool CheckCursorPosition() { return angle is > 90 or < -90; }

    }
}
