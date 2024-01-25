using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _weapons = new List<GameObject>();

        private PlayerInput _input;
        private GameObject _currentWeapon;
        private int _currentWeaponIndex = 1;

        private void Awake() => _input = new PlayerInput();

        private void SwitchWeapon()
        {
            _currentWeaponIndex = (_currentWeaponIndex + 1) % _weapons.Count;

            if (_currentWeapon)
                _currentWeapon.SetActive(false);

            _currentWeapon = _weapons[_currentWeaponIndex];
            _currentWeapon.SetActive(true);
        }

        private void OnEnable()
        {
            _input.Gameplay.SwitchWeapon.performed += _ => SwitchWeapon();
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Gameplay.SwitchWeapon.performed -= _ => SwitchWeapon();
            _input.Disable();
        }
    }
}
