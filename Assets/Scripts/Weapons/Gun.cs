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
            //currentSize = bulletPrefab.transform.localScale.y;
            currentSize = config.GunConfig.BulletPrefab.transform.localScale.y;
        }

        private void Update()
        {
            transform.rotation = GetRotation();
            transform.localScale = GetScale();
        }

        protected override void Shoot()
        {
            if (!canShoot) return;

            //animator.SetTrigger(Attack);
            config.GunConfig.Animator.SetTrigger(Attack);
            SpawnBullet();
        }

        protected override async void Recharge()
        {
            canShoot = false;
            //await Task.Delay((int)(reload * Second));
            await Task.Delay((int)(config.GunConfig.Reload * Second));
            canShoot = true;
        }

        private void SpawnBullet()
        {
            //var bulletGameObject = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            var bulletGameObject = Instantiate(config.GunConfig.BulletPrefab, config.GunConfig.SpawnPoint.position, config.GunConfig.SpawnPoint.rotation);
            var bullet = bulletGameObject.GetComponent<Bullet>();

            bullet.SetScale(new Vector3(1, CheckCursorPosition() ? -currentSize : currentSize, 1));
            bullet.SetDirection(CheckCursorPosition() ? Vector3.down : Vector3.up);

            Recharge();
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
