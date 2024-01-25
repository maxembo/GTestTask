using Bullets;
using System.Threading.Tasks;
using UnityEngine;

namespace Weapons
{
    public class Gun : Weapon
    {
        private void Awake() => input = new PlayerInput();

        private void Start()
        {
            main = Camera.main;
            currentSize = bulletPrefab.transform.localScale.y;
        }

        private void Update()
        {
            transform.rotation = GetRotation();
            transform.localScale = GetScale();
        }

        protected override void Shoot()
        {
            if (!canShoot) return;

            animator.SetTrigger(Attack);
            SpawnBullet();
        }

        protected override async Task Recharge()
        {
            canShoot = false;
            await Task.Delay((int)(reload * Second));
            canShoot = true;
        }

        protected override Quaternion GetRotation()
        {
            var mousePosition = input.Gameplay.FireDirection.ReadValue<Vector2>();
            var cameraScreenPoint = main.ScreenToWorldPoint(mousePosition);
            var targetDirection = cameraScreenPoint - transform.position;

            angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0f, 0f, angle);
        }

        protected override Vector3 GetScale()
        {
            localScale.y = CheckCursorPosition() ? -1f : 1f;

            return localScale;
        }

        private async void SpawnBullet()
        {
            var bulletGameObject = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            var bullet = bulletGameObject.GetComponent<Bullet>();

            bullet.SetScale(new Vector3(1, CheckCursorPosition() ? -currentSize : currentSize, 1));
            bullet.SetDirection(CheckCursorPosition() ? Vector3.down : Vector3.up);

            await Recharge();
        }

        private void OnEnable()
        {
            input.Gameplay.Fire.performed += _ => Shoot();
            input.Enable();
        }

        private void OnDisable()
        {
            input.Gameplay.Fire.performed -= _ => Shoot();
            input.Disable();
        }
    }
}
