using Bullets;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class Shotgun : Weapon
    {
        private InputAction InputFire => input.Gameplay.Fire;

        private void Awake() => input = new PlayerInput();


        private void Start()
        {
            main = Camera.main;
            //currentSize = bulletPrefab.transform.localScale.y;
            currentSize = config.ShotgunConfig.BulletPrefab.transform.localScale.y;
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
            config.ShotgunConfig.Animator.SetTrigger(Attack);
            SpawnBullet();
        }

        protected override async void Recharge()
        {
            canShoot = false;
            //await Task.Delay((int)(reload * Second));
            await Task.Delay((int)(config.ShotgunConfig.Reload * Second));
            canShoot = true;
        }

        private void SpawnBullet()
        {
            for (int currentBullet = -1; currentBullet < 2; currentBullet++)
            {
                //var bulletGameObject = Instantiate(bulletPrefab, spawnPoint.position, GetBulletRotation(currentBullet));
                var bulletGameObject = Instantiate(config.ShotgunConfig.BulletPrefab, config.ShotgunConfig.SpawnPoint.position, GetBulletRotation(currentBullet));

                var bullet = bulletGameObject.GetComponent<Bullet>();

                bullet.SetScale(new Vector2(1, CheckCursorPosition() ? -currentSize : currentSize));
                bullet.SetDirection(CheckCursorPosition() ? Vector2.down : Vector2.up);
            }

            Recharge();
        }

        //private Quaternion GetBulletRotation(int currentBullet) => spawnPoint.rotation * Quaternion.Euler(0f, 0f, 45f * currentBullet);
        private Quaternion GetBulletRotation(int currentBullet) => config.ShotgunConfig.SpawnPoint.rotation * Quaternion.Euler(0f, 0f, 45f * currentBullet);

        private void OnEnable()
        {
            InputFire.performed += _ => Shoot();
            input.Enable();
        }

        private void OnDisable()
        {
            InputFire.performed -= _ => Shoot();
            input.Disable();

        }
    }
}
